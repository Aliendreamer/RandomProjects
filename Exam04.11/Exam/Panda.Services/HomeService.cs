namespace Panda.Services
{
    using Interfaces;
    using System.Linq;
    using Domain.Models;
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

            if (user.Role.ToString() == "Admin")
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
                PackageType = "Pending",

                Packages = allPackages.Where(x => x.Status.ToString() == "Pending").Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = "block",
                    Acquire = "none"
                }).ToList()
            };
            var shipped = new HomeIndexViewModel
            {
                PackageType = "Shipped",
                Packages = allPackages.Where(x => x.Status.ToString() == "Shipped").Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = "block",
                    Acquire = "none"
                }).ToList()
            };
            var delivered = new HomeIndexViewModel
            {
                PackageType = "Delivered",
                Packages = allPackages.Where(x => x.Status.ToString() == "Delivered" && x.Recipient.Username == username).Select(x => new PackageHomeModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = "none",
                    Acquire = "block"
                }).ToList()
            };
            packageRowsList.Add(pending);
            packageRowsList.Add(shipped);
            packageRowsList.Add(delivered);

            return packageRowsList;
        }
    }
}