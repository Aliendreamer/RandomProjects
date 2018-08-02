namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Part
    {

        public Part()
        {
            Cars=new HashSet<PartCar>();
        }

        public int PartId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<PartCar> Cars { get; set; }

    }
}
