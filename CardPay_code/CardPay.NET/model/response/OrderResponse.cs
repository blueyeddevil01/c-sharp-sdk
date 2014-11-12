using System.Xml.Serialization;

namespace CardPay.model.response
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum StatusType
    {
        [XmlEnum(Name = "APPROVED")]
        APPROVED,
        [XmlEnum(Name = "DECLINED")]
        DECLINED,
        [XmlEnum(Name = "PENDING")]
        PENDING,
        [XmlEnum(Name = "VOIDED")]
        VOIDED,
        [XmlEnum(Name = "REFUNDED")]
        REFUNDED,
        [XmlEnum(Name = "CHARGEBACK")]
        CHARGEBACK,
    }

    /// <summary>
    /// Order result response
    /// </summary>
    [XmlRoot("order")]
	public class OrderResponse
	{
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order's ID
        /// </value>
	    [XmlAttribute("id")]
	    public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>
        /// Unique order ID used by the merchant’s shopping cart.
        /// </value>
        [XmlAttribute("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status of order.
        /// </value>
        [XmlAttribute("status")]
        public StatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// Description of product/service being sold.
        /// </value>
        [XmlAttribute("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date of order.
        /// </value>
        [XmlAttribute("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the card bin.
        /// </summary>
        /// <value>
        /// The first 6 card number
        /// </value>
        [XmlAttribute("card_bin")]
        public int CardBin { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card part number (mask: 6…4 or …4).
        /// </value>
        [XmlAttribute("card_num")]
        public string CardNum { get; set; }

        /// <summary>
        /// Gets or sets the card holder.
        /// </summary>
        /// <value>
        /// The card holder.
        /// </value>
        [XmlAttribute("card_holder")]
        public string CardHolder { get; set; }

        /// <summary>
        /// Gets or sets the decline reason.
        /// </summary>
        /// <value>
        /// The decline reason.
        /// </value>
        [XmlAttribute("decline_reason")]
        public string DeclineReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is3 d].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is3 d]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute("is_3d")]
        public bool Is3D { get; set; }

        /// <summary>
        /// Gets or sets the approval code.
        /// </summary>
        /// <value>
        /// The approval code.
        /// </value>
        [XmlAttribute("approval_code")]
        public string ApprovalCode { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [XmlAttribute("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the refunded.
        /// </summary>
        /// <value>
        /// The refunded.
        /// </value>
        [XmlAttribute("refunded")]
        public decimal Refunded { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [XmlAttribute("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [recurring success].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [recurring success]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute("recurring_success")]
        public bool RecurringSuccess { get; set; }
	}
}