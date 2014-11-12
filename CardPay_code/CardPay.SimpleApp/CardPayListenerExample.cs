using System;
using CardPay.interfaces;
using CardPay.model.request;
using CardPay.model.response;

namespace CardPay.SimpleApp
{
    public class CardPayListenerExample : ICardPayListener
    {
        public void OnPayCompleted(OrderResponse result) {
            var payResult = result;

            Console.WriteLine(payResult);
        }

        public void OnPrintFormCompleted(PrintForm form) {
            var printForm = form;

            Console.WriteLine("OrderXML : {0}; SHA512 : {1}", printForm.OrderXML, printForm.SHA512);
        }

        public void OnError(string errorDescription) {
            Console.WriteLine("CardPayAPI error: {0}", errorDescription);
        }

        public void OnReportRequestCompleted(ReportResponse report) {
            var reportResponse = report;

            Console.WriteLine(reportResponse);
        }

        public void OnChangeRequestCompleted(ChangeResponse change) {
            var changeResponse = change;

            Console.WriteLine(changeResponse);
        }
    }
}
