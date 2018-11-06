namespace Panda.Infrastructure.ViewModels.OutputModels
{
    public class ReceiptDisplayModel
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public string IssuedOn { get; set; }

        public string Recipient { get; set; }
    }
}