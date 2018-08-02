namespace ProductShopApp.Dtos.export
{   
    using System;
    using System.Xml.Serialization;

    [XmlType("product")]
    public class ProductDtoFourthProblem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
