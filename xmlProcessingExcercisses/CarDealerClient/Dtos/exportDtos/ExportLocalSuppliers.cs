namespace CarDealerClient.Dtos
{
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class ExportLocalSuppliers
    {
        [XmlAttribute("id")]
        public int SupplierId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartCount { get; set; }
    }
}
