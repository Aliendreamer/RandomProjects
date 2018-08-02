namespace ProductShopApp.Dtos.import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using productShopDatabase.Models;

    [XmlType("product")]
    public class ProductDto
    {
        [XmlElement("name")]
        [MinLength(3)]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlIgnore]
        public int? ByerId { get; set; }
        public User Byer { get; set; }

        [XmlIgnore]
        public int SellerId { get; set;}
        public User Seller { get; set;}
    }
}
