namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;


    public class Car
    {

        public Car()
        {
            Parts=new HashSet<PartCar>();
            Sales=new HashSet<Sale>();
        }

        public int CarId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long Distance { get; set; }


        public decimal Price => this.Parts.Sum(x => x.Part.Price * x.Part.Quantity);
        

        public ICollection<PartCar> Parts { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
