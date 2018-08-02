namespace CarDealerClient.Dtos.importDtos
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using CarDealer.Models;

    [XmlType("car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long Distance { get; set; }        

        [XmlIgnore]
        public ICollection<PartCar> Parts { get; set; }
    }
}
