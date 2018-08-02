namespace ProductShopApp.Dtos.export
{   
    using System;
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UserDtoUserInfoOnly
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public UsersandProductsProductDto SoldProduct { get; set; }
    }
}

