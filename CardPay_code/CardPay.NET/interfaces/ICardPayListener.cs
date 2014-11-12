using CardPay.model.request;
using CardPay.model.response;

namespace CardPay.interfaces
{
    /// <summary>
    /// API listener
    /// </summary>
    public interface ICardPayListener {
        /// <summary>
        /// Called when order request completed.
        /// </summary>
        /// <param name="orderResult">The order result.</param>
        void OnPayCompleted(OrderResponse orderResult);
        /// <summary>
        /// Called when print form ready.
        /// </summary>
        /// <param name="form">The print form.</param>
        void OnPrintFormCompleted(PrintForm form);
        /// <summary>
        /// Called when error occured.
        /// </summary>
        /// <param name="errorDescription">The error description.</param>
        void OnError(string errorDescription);
        /// <summary>
        /// Called when report request completed.
        /// </summary>
        /// <param name="reportResponse">The report response.</param>
        void OnReportRequestCompleted(ReportResponse reportResponse);
        /// <summary>
        /// Called when change request completed.
        /// </summary>
        /// <param name="changeResponse">The change response.</param>
        void OnChangeRequestCompleted(ChangeResponse changeResponse);
    }
}
