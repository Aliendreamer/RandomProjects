namespace Panda.Services.Interfaces
{
    using System.Collections.Generic;
    using Infrastructure.ViewModels.OutputModels;

    public interface IHomeService
    {
        IEnumerable<HomeIndexViewModel> GetHomeViewModels(string username);
    }
}