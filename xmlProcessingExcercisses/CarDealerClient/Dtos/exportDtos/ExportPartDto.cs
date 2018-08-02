namespace CarDealerClient.Dtos
{
    using System.Xml.Serialization;

    [XmlType("product")]
   public class ExportPartDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
