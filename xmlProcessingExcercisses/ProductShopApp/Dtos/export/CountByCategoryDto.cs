namespace ProductShopApp.Dtos.export
{   
   
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using productShopDatabase.Models;

    [XmlType("category")]
    public class CountByCategoryDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlIgnore]
        public HashSet<CategoryProduct> Products { get; set; }

        [XmlElement("products-count")]
        public int ProductCount { get; set; }

        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }

        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}
