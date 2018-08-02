namespace CarDealerClient.Dtos.exportDtos
{
    using System;
    using System.Xml.Serialization;

    [XmlType("sale")]
    public class ExportSaleDto
    {
        [XmlIgnore]
        public decimal Pricewithdiscount;
        [XmlElement("car")]
        public ExportFerariCars ExportFerariCars { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public double Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        // i needed to make PriceWithDiscount this way or xmlserializer couldn't see it
        // strange but true and it worked
        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount
        {
            get => Math.Round(this.Price -= this.Price * (decimal)this.Discount, 2);
            set => Pricewithdiscount = value;
        }
    }
}
