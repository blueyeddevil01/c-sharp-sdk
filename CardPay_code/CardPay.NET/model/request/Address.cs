using System.Xml.Serialization;

namespace CardPay.model.request
{
    /// <summary>
    /// Address description
    /// </summary>
	public class Address {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// ISO 3166-1 code of delivery country.
        /// </value>
        [XmlAttribute("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// Delivery state or province.
        /// </value>
        [XmlAttribute("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// Delivery city.
        /// </value>
        [XmlAttribute("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// Delivery post code.
        /// </value>
        [XmlAttribute("zip")]
        public string ZIP { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>
        /// Delivery street address.
        /// </value>
        [XmlAttribute("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// Customer phone number.
        /// </value>
        [XmlAttribute("phone")]
        public string Phone { get; set; }
	}
}

