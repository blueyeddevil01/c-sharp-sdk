using System.Xml.Serialization;

namespace CardPay.model.response
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum TransactionStatus
    {
        [XmlEnum(Name = "void")]
        VOID,
        [XmlEnum(Name = "capture")]
        CAPTURE,
        [XmlEnum(Name = "refund")]
        REFUND,
    }

    /// <summary>
    /// Change order info
    /// </summary>
    public class ChangeOrderInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// Requested order's ID
        /// </value>
        [XmlAttribute("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status to.
        /// </summary>
        /// <value>
        /// Requested status of the order
        /// </value>
        [XmlAttribute("status_to")]
        public TransactionStatus StatusTo { get; set; }

        /// <summary>
        /// Gets or sets the refund amount.
        /// </summary>
        /// <value>
        /// Refunded amount in the currency of processing
        /// </value>
        [XmlAttribute("refund_amount")]
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// Currency of refunded amount
        /// </value>
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the remaining amount.
        /// </summary>
        /// <value>
        /// Amount left after refund was made
        /// </value>
        [XmlAttribute("remaining_amount")]
        public decimal RemainingAmount { get; set; }
    }
}
