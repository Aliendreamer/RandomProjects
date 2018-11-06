namespace Panda.Services.Interfaces
{
    using System.Collections.Generic;
    using Infrastructure.ViewModels.InputModels;
    using Infrastructure.ViewModels.OutputModels;

    public interface IPackageService
    {
        IEnumerable<CreateDropDownMenuModel> GetRecipientNames();

        void CreatePackage(PackageCreateModel model);

        PackageDetailsViewModel PackageDetailsById(int id);

        void Acquire(int id, string username);

        void Deliver(int id);

        void Ship(int id);

        IEnumerable<PackageViewModel> GetPackagesByShipmentStatus(string type);
    }
}