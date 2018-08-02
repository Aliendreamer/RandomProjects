namespace CarDealerClient.Dtos.exportDtos
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportFerariCars
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long Distance { get; set; }
    }
}
