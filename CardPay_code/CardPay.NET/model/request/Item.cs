using System.ComponentModel;
using System.Xml.Serialization;

namespace CardPay.model.request
{
    /// <summary>
    /// Order item
    /// </summary>
	public class Item
	{
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <value>
        /// The name of product / service, provided to the customer.
        /// </value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// Description of product / service, provided to the customer.
        /// </value>
        [XmlAttribute("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// Product / service quantity.
        /// </value>
        [DefaultValue(1), XmlAttribute("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// Price of product / service.
        /// </value>
        [DefaultValue(0), XmlAttribute("price")]
        public decimal Price { get; set; }
	}
}