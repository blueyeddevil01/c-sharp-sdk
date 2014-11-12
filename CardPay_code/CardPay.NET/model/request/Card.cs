using System.Xml.Serialization;

namespace CardPay.model.request
{
    /// <summary>
    /// Customer's card.
    /// </summary>
	public class Card
	{
        /// <summary>
        /// Gets or sets the card number (PAN).
        /// </summary>
        /// <value>
        /// Customer's card number (PAN).
        /// </value>
        [XmlAttribute("num")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the cardholder name.
        /// </summary>
        /// <value>
        /// Customer's cardholder name.
        /// </value>
        [XmlAttribute("holder")]
        public string Holder { get; set; }

        /// <summary>
        /// Gets or sets the CVV.
        /// </summary>
        /// <value>
        /// Customer's CVV2 / CVC2.
        /// </value>
        [XmlAttribute("cvv")]
        public string CVV { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// Customer's card expiration date.
        /// </value>
        [XmlAttribute("expires")]
        public string Expires { get; set; }
	}
}