using System.Xml.Serialization;

namespace CardPay.model.response
{
    /// <summary>
    /// Report order info
    /// </summary>
    public class ReportOrderInfo
    {
        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        /// <value>
        /// The Order's ID.
        /// </value>
        [XmlAttribute("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// Merchant's order ID
        /// </value>
        [XmlAttribute("orderu_number")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// Current status of the order
        /// </value>
        [XmlAttribute("status_name")]
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the date in.
        /// </summary>
        /// <value>
        /// Date in UTC when the order was received
        /// </value>
        [XmlAttribute("date_in")]
        public string DateIn { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// Order amount in the currency of processing
        /// </value>
        [XmlAttribute("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the hold number.
        /// </summary>
        /// <value>
        /// Approval code received from the bank
        /// </value>
        [XmlAttribute("hold_number")]
        public string HoldNumber { get; set; }

        /// <summary>
        /// Gets or sets the e mail.
        /// </summary>
        /// <value>
        /// Email of customer
        /// </value>
        [XmlAttribute("email")]
        public string EMail { get; set; }
    }
}
