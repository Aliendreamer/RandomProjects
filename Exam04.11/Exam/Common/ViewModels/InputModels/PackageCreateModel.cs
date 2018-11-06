namespace Panda.Infrastructure.ViewModels.InputModels
{
    public class PackageCreateModel
    {
        public string Description { get; set; }

        public string Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recipient { get; set; }
    }
}