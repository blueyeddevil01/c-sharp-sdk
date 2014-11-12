using System.Xml.Serialization;

namespace CardPay.model.response
{
    [XmlRoot("response")]
    public class ChangeResponse
    {
        /// <summary>
        /// Gets or sets the is executed.
        /// </summary>
        /// <value>
        /// Indicates was the request successful or not
        /// </value>
        [XmlAttribute("is_executed")]
        public string IsExecuted { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The reason why request was insuccessful
        /// </value>
        [XmlAttribute("details")]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the order information.
        /// </summary>
        /// <value>
        /// The order information.
        /// </value>
        [XmlElement("order")]
        public ChangeOrderInfo OrderInfo { get; set; }
    }
}
