namespace ProductShopApp.Dtos.import
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using productShopDatabase.Models;

    [XmlType("user")]
    public class UserDto
    {
            public int Id { get; set; }

            [XmlAttribute("firstName")]
            public string FirstName { get; set; }

            [XmlAttribute("lastName")]
            [MinLength(3)]
            public string LastName { get; set; }

            [XmlAttribute("age")]
            public string Age { get; set; }

            public HashSet<Product> BoughtProducts { get; set; }

            public HashSet<Product> SoldProducts { get; set; }
        }
}
