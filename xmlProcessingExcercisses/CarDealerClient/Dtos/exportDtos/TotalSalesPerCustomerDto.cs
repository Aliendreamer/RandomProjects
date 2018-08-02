namespace CarDealerClient.Dtos
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class TotalSalesPerCustomerDto
    {
        [XmlAttribute("full-name")]
        public string Name { get; set; }

        [XmlAttribute("bought-cars")]
        public int CarDeals { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SumPaid { get; set; }
    }
}
