using System;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using CardPay.interfaces;
using CardPay.model.request;
using CardPay.model.response;

namespace CardPay
{
	public class CardPayAPI
	{
		private const string cardpaymentUrl = "https://cardpay.com/MI/cardpayment.html";
        private const string orderReportUrl = "https://cardpay.com/MI/service/order-report";
        private const string orderChangeStatusUrl = "https://cardpay.com/MI/service/order-change-status";

		private string _secret;
        private int _walletId;
	    private ICardPayListener _listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardPayAPI"/> class.
        /// </summary>
        /// <param name="secret">The secret key.</param>
        /// <param name="walletId">The wallet identifier.</param>
        /// <param name="listener">The listener for response.</param>
        public CardPayAPI(String secret, int walletId, ICardPayListener listener) {
			_secret = secret;
            _walletId = walletId;
            _listener = listener;
        }

#region Публичные методы

        /// <summary>
        /// This method allows you to send a transaction to the CardPay server for processing
        /// </summary>
        /// <param name="order">Order request</param>
		public void Pay(OrderRequest order){
            order.WalletID = _walletId;

            var base64OrderXML = order.toBase64();
            var sha512 = order.toSHA512(_secret);

            var parameters = new NameValueCollection { { "orderXML", base64OrderXML }, { "sha512", sha512 } };
            sendOrderRequest(parameters);
		}

        /// <summary>
        /// This method allows you to get orderXML and sha512 from order
        /// </summary>
        /// <param name="order">Order to process</param>
        public void PrintForm(OrderRequest order) {
            order.WalletID = _walletId;
            
            var base64OrderXML = order.toBase64();
            var sha512 = order.toSHA512(_secret);

            _listener.OnPrintFormCompleted(new PrintForm {
                OrderXML = base64OrderXML,
                SHA512 = sha512
            });
        }

        /// <summary>
        /// This method allows you to receive transaction details
        /// </summary>
        /// <param name="clientLogin">User login. It is the same as for Payment Manager</param>
        /// <param name="clientPassword">User password. It is the same as for Payment Manager</param>
        /// <param name="dateBegin">Optional. Date from which you want to receive last 10 orders</param>
        /// <param name="dateEnd">Optional. Date before which you want to receive last 10 orders</param>
        /// <param name="currency">Optional. Limit result with single currency of order</param>
        /// <param name="orderNumber">Optional. Order number sent with order</param>
        public void GetTransactionsInfo(string clientLogin, string clientPassword, string dateBegin = "", string dateEnd = "", string currency = "", string orderNumber = "") {
            var parameters = new NameValueCollection {
                {"client_login", clientLogin},
                {"client_password", clientPassword},
                {"wallet_id", _walletId.ToString()}
            };

            if(!String.IsNullOrWhiteSpace(dateBegin))
                parameters.Add("date_begin", dateBegin);

            if(!String.IsNullOrWhiteSpace(dateEnd))
                parameters.Add("date_end", dateEnd);

            if(!String.IsNullOrWhiteSpace(currency))
                parameters.Add("currency", currency);

            if(!String.IsNullOrWhiteSpace(orderNumber))
                parameters.Add("order_number", orderNumber);

            sendReportRequest(parameters);
		}

        /// <summary>
        /// This method allows you to receive transaction details
        /// </summary>
        /// <param name="clientLogin">User login. It is the same as for Payment Manager</param>
        /// <param name="clientPassword">User password. It is the same as for Payment Manager</param>
        /// <param name="id">ID of Order to be changed</param>
        /// <param name="statusTo">Status to be set (void, capture, refund)</param>
        /// <param name="amount">Optional. Only for refund. Amount to be refunded. Total refund when not sent</param>
        /// <param name="reason">Optional. Required for refund. Reason of refunding</param>
        public void ChangeTransactionStatus(string clientLogin, string clientPassword, int id, TransactionStatus statusTo, decimal amount = 0, string reason = "")
        {
            var parameters = new NameValueCollection {
                {"client_login", clientLogin},
                {"client_password", clientPassword},
                {"id", id.ToString()},
                {"status_to", statusTo.ToString().ToLower()}
            };

            if (amount != 0)
                parameters.Add("amount", amount.ToString());

            if (!String.IsNullOrWhiteSpace(reason))
                parameters.Add("reason", reason);

            sendChangeRequest(parameters);
        }

#endregion

#region Приватные методы

        private void sendOrderRequest(NameValueCollection parameters) {
	        var client = new WebClient();

	        client.UploadValuesCompleted += OnPayRequestCompleted;
            client.UploadValuesAsync(new Uri(cardpaymentUrl), "POST", parameters);
	    }
        
        private void sendReportRequest(NameValueCollection parameters) {
            var request = WebRequest.Create(orderReportUrl + parametersToString(parameters));
            request.Method = "GET";

            using (var response = request.GetResponse()) {
                using (var stream = response.GetResponseStream()) {
                    var serializer = new XmlSerializer(typeof(ReportResponse));
                    var reader = new StreamReader(stream);

                    ReportResponse reportResponse;
                    try {
                        reportResponse = (ReportResponse)serializer.Deserialize(reader);
                    } catch (Exception ex) {
                        _listener.OnError(String.Format("Error in requesting order report. Response: {0}. Error: {1}",
                                                        response, ex.Message));
                        return;
                    }

                    if (_listener != null)
                        _listener.OnReportRequestCompleted(reportResponse);
                }
            }
        }

        private void sendChangeRequest(NameValueCollection parameters) {
            var request = WebRequest.Create(orderChangeStatusUrl + parametersToString(parameters));
            request.Method = "GET";

            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new XmlSerializer(typeof(ChangeResponse));
                    var reader = new StreamReader(stream);

                    ChangeResponse changeResponse;
                    try {
                        changeResponse = (ChangeResponse)serializer.Deserialize(reader);
                    }
                    catch (Exception ex)
                    {
                        _listener.OnError(String.Format("Error in changing order status. Response: {0}. Error: {1}",
                                                        response, ex.Message));
                        return;
                    }

                    if (_listener != null)
                        _listener.OnChangeRequestCompleted(changeResponse);
                }
            }
        }

        private String parametersToString(NameValueCollection parameters) {
	        var builder = new StringBuilder();
	        bool isFirstArg = true;
	        foreach (String name in parameters.Keys) {
	            String prefix = "&";

	            if (isFirstArg) {
	                prefix = "?";
	                isFirstArg = false;
	            }
	            builder.Append(String.Format("{0}{1}={2}", prefix, name, Uri.EscapeDataString(parameters[name])));
	        }
	        return builder.ToString();
	    }

	    private void OnPayRequestCompleted(object sender, UploadValuesCompletedEventArgs e) {
            var response = Encoding.UTF8.GetString(e.Result);

            var serializer = new XmlSerializer(typeof(OrderResponse));
            using (var reader = XmlReader.Create(new StringReader(response))) {
                OrderResponse orderResult;
                try {
                    orderResult = (OrderResponse)serializer.Deserialize(reader);
                } catch (Exception ex) {
                    _listener.OnError(String.Format("Gateway mode is not supported for this wallet ID. Response: {0}. Error: {1}",
                                                    response, ex.Message));
                    return;
                }

                if (_listener != null)
                    _listener.OnPayCompleted(orderResult);
            }
	    }

#endregion

	}
}