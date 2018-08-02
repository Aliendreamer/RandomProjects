namespace ProductShopApp.Dtos.export
{
    using System.Xml.Serialization;


    [XmlType("user")]
    public class UserDtoSoldItems
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public SoldProductsDto[] SoldProducts { get; set; }
    }
}
