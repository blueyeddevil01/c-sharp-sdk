using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CardPay.model.request
{
    public class PrintForm
    {
        public string OrderXML { get; set; }

        public string SHA512 { get; set; }
    }

    /// <summary>
    /// Transaction request
    /// </summary>
    [XmlRoot("order")]
	public class OrderRequest
	{

#region Обязательные аттрибуты

        /// <summary>
        /// Gets or sets the Unique merchant's ID.
        /// </summary>
        /// <value>
        /// Unique merchant's ID used by the CardPay payment system
        /// </value>
	    [XmlAttribute("wallet_id")]
	    public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>
        /// Unique order ID used by the merchant’s shopping cart.
        /// </value>
        [XmlAttribute("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the order amount.
        /// </summary>
        /// <value>
        /// The total order amount in your account’s selected currency.
        /// </value>
        [XmlAttribute("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the customer’s e-mail.
        /// </summary>
        /// <value>
        /// Customer’s e-mail address.
        /// </value>
        [XmlAttribute("email")]
        public string EMail { get; set; }

#endregion

#region Необязательные аттрибуты

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// Description of product/service being sold.
        /// </value>
        [XmlAttribute("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is two phase.
        /// </summary>
        /// <value>
        /// If <c>true</c> the amount will not be captured but only blocked.
        /// </value>
        [XmlAttribute("is_two_phase")]
        public bool IsTwoPhase { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// ISO 4217 currency code.
        /// </value>
        /// <exception cref="CardPayException"></exception>
        [XmlAttribute("currency")]
        public string Currency {
            get { return _currency; }
            set {
                if (value.Length != 3)
                    throw new CardPayException(string.Format("Wrong currency format: {0}", value));

                _currency = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gateway Mode.
        /// </summary>
        /// <value>
        /// If <c>true</c> the Gateway Mode will be used.
        /// </value>
        [XmlAttribute("is_gateway")]
        public bool IsGateway { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// Customer’s IPv4.
        /// </value>
        [XmlAttribute("ip")]
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        /// <value>
        /// Preferred locale for the payment page. The default locale (en) will be applied if the selected locale is not supported.
        /// </value>
        [DefaultValue("en"), XmlAttribute("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the order items.
        /// </summary>
        /// <value>
        /// Order items.
        /// </value>
        [XmlElement(ElementName = "order_item")]
        public List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// Shipping Address
        /// </value>
        [XmlElement(ElementName = "shipping")]
        public Address Shipping { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address
        /// </value>
        [XmlElement(ElementName = "billing")]
        public Address Billing { get; set; }

        /// <summary>
        /// Gets or sets the customer's card
        /// </summary>
        /// <value>
        /// The Customer's card.
        /// </value>
        [XmlElement(ElementName = "card")]
        public Card Card { get; set; }

#endregion

        private XmlSerializer _serializer;
        private string _currency;

        public OrderRequest() {
            _serializer = new XmlSerializer(GetType());
        }

        /// <summary>
        /// Get XML-string from order object.
        /// </summary>
        /// <returns></returns>
        public string toString() {
            using (var stream = new StringWriter()) {
                using (var writer = XmlWriter.Create(stream)) {
                    _serializer.Serialize(writer, this);
                    return stream.ToString();
                }
            }
        }

        /// <summary>
        /// Get base64-string from order object.
        /// </summary>
        /// <returns></returns>
        public string toBase64() {
            var data = Encoding.UTF8.GetBytes(toString());
            return Convert.ToBase64String(data, 0, data.Length);
        }

        /// <summary>
        /// Get sha512-string from order object.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        public string toSHA512(string secret) {
            var digest = String.Format("{0}{1}", toString(), secret);
            var data = Encoding.UTF8.GetBytes(digest);

            return toHex(new SHA512Managed().ComputeHash(data));
        }

        private String toHex(byte[] buf) {
            return toHex(buf, 0, buf.Length);
        }

        private char[] _hexchars = new char[] { '0', '1', '2', '3', '4', '5',
			'6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private String toHex(byte[] buf, int ofs, int len) {
            var sb = new StringBuilder();
            var j = ofs + len;
            for (var i = ofs; i < j; i++) {
                if (i < buf.Length) {
                    sb.Append(_hexchars[(buf[i] & 0xF0) >> 4]);
                    sb.Append(_hexchars[buf[i] & 0x0F]);
                }
            }
            return sb.ToString();
        }
    }
}