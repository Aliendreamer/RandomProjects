namespace Panda.Infrastructure.ViewModels.OutputModels
{
    public class PackageViewModel
    {
        public int Number { get; set; }
        public int Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }
    }
}