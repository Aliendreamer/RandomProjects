namespace CarDealerClient.Dtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using CarDealer.Models;

    [XmlType("car")]
    public class ExportCarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string  Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long Distance { get; set; }

        [XmlArray("parts")]
        public List<ExportPartDto> Parts { get; set; }
    }
}
