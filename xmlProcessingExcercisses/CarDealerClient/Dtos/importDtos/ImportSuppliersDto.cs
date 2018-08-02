namespace CarDealerClient.Dtos.importDtos
{   
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class ImportSuppliersDto
    {
        public int SupplierId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }

    }
}
