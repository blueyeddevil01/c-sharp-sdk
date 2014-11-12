using System.Collections.Generic;
using System.Xml.Serialization;

namespace CardPay.model.response
{
    /// <summary>
    /// Report response
    /// </summary>
    [XmlRoot("response")]
    public class ReportResponse
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
        /// Gets or sets the order qty.
        /// </summary>
        /// <value>
        /// Quantity of orders in response, returned in case of successful request
        /// </value>
        [XmlAttribute("orders_qty")]
        public int OrderQty { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The reason why request was insuccessful
        /// </value>
        [XmlAttribute("details")]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the orders information.
        /// </summary>
        /// <value>
        /// The orders information.
        /// </value>
        [XmlArray("orders")]
        [XmlArrayItem("orderu", typeof(ReportOrderInfo))]
        public List<ReportOrderInfo> OrdersInfo { get; set; }
    }
}
