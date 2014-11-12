using System;
using System.Xml.Serialization;

namespace CardPay.model.request
{
	public class Reccuring
	{
        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// Period in days of extension of service
        /// </value>
        [XmlAttribute("period")]
        public int Period { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// Cost of extension of service
        /// </value>
        [XmlAttribute("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the begin.
        /// </summary>
        /// <value>
        /// Date from which recurring payments begin
        /// </value>
        [XmlAttribute("begin")]
        public DateTime Begin { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// Number of recurring payments
        /// </value>
        [XmlAttribute("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the begin identifier.
        /// </summary>
        /// <value>
        /// Can be used instead of all other attributes to continue payment manually in Gateway Mode. Must contain ID of the initial transaction.
        /// </value>
        [XmlAttribute("begin_id")]
        public int BeginId { get; set; }

        public Reccuring() {
            Begin = DateTime.Now;
        }
	}
}