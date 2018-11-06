namespace Panda.Services.Interfaces
{
    using System.Collections.Generic;
    using Infrastructure.ViewModels.OutputModels;

    public interface IReceiptsService
    {
        IEnumerable<ReceiptDisplayModel> IndexModels(string userName);

        ReceiptDetailsViewModel GetReceiptById(int id);
    }
}