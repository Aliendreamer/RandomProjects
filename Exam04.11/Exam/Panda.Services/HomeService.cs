namespace Panda.Services
{
    using Interfaces;
    using System.Linq;
    using Domain.Enums;
    using Domain.Models;
    using Infrastructure.Constants;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Infrastructure.ViewModels.OutputModels;

    public class HomeService : BaseService, IHomeService
    {
        public IEnumerable<HomeIndexViewModel> GetHomeViewModels(string username)
        {
            var packageRowsList = new List<HomeIndexViewModel>();

            var allPackages = new List<Package>();

            var user = this.Db.Users.First(x => x.Username == username);

            if (user.Role.ToString() == nameof(UserRole.Admin))
            {
                allPackages = this.Db.Packages.Include(x => x.Recipient).ToList();
            }
            else
            {
                allPackages = this.Db.Packages.Include(x => x.Recipient)
                    .Where(x => x.Recipient.Username == username).ToList();
            }
            var pending = new HomeIndexViewModel
            {
                PackageType = nameof(PackageStatus.Pending),

                Packages = allPackages.Where(x => x.Status.ToString() == nameof(PackageStatus.Pending)).Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = GlobalConstants.Display.DisplayBlock,
                    Acquire = GlobalConstants.Display.DisplayNone
                }).ToList()
            };
            var shipped = new HomeIndexViewModel
            {
                PackageType = nameof(PackageStatus.Shipped),
                Packages = allPackages.Where(x => x.Status.ToString() == nameof(PackageStatus.Shipped)).Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = GlobalConstants.Display.DisplayBlock,
                    Acquire = GlobalConstants.Display.DisplayNone
                }).ToList()
            };
            var delivered = new HomeIndexViewModel
            {
                PackageType = nameof(PackageStatus.Delivered),
                Packages = allPackages.Where(x => x.Status.ToString() == nameof(PackageStatus.Delivered) && x.Recipient.Username == username).Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = GlobalConstants.Display.DisplayNone,
                    Acquire = GlobalConstants.Display.DisplayBlock
                }).ToList()
            };
            packageRowsList.Add(pending);
            packageRowsList.Add(shipped);
            packageRowsList.Add(delivered);

            return packageRowsList;
        }
    }
}