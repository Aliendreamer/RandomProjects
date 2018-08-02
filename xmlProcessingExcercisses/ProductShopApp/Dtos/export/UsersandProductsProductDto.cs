namespace ProductShopApp.Dtos.export
{

    using System.Xml.Serialization;


    [XmlType("sold-products")]
    public class UsersandProductsProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDtoFourthProblem[] SoldProductsDtos { get; set; }
    }


}
