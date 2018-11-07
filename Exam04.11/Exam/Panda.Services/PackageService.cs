namespace Panda.Services
{
    using System;
    using AutoMapper;
    using Interfaces;
    using System.Linq;
    using Domain.Enums;
    using Domain.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;
    using Infrastructure.Constants;
    using Infrastructure.ViewModels.InputModels;
    using Infrastructure.ViewModels.OutputModels;

    public class PackageService : BaseService, IPackageService
    {
        public IEnumerable<CreateDropDownMenuModel> GetRecipientNames()
        {
            var names = this.Db.Users.ProjectTo<CreateDropDownMenuModel>().ToList();

            return names;
        }

        public void Acquire(int id, string username)
        {
            var user = this.Db.Users.First(x => x.Username == username);
            var package = this.Db.Packages.Find(id);
            CreateReceipt(user, package);
        }

        public void Ship(int id)
        {
            var package = this.Db.Packages.Find(id);
            var rng = new Random();
            var days = rng.Next(19, 41);
            package.EstimatedDeliveryDate = DateTime.UtcNow;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            package.EstimatedDeliveryDate.Value.AddDays(days);
            package.Status = PackageStatus.Shipped;
            this.Db.Packages.Update(package);
            this.Db.SaveChanges();
        }

        public IEnumerable<PackageViewModel> GetPackagesByShipmentStatus(string type)
        {
            var shipments = this.Db.Packages
                .Where(x => x.Status.ToString() == type)
                .ProjectTo<PackageViewModel>().ToArray();

            int count = 1;
            foreach (var s in shipments)
            {
                s.Number = count;
                count++;
            }

            return shipments;
        }

        public void Deliver(int id)
        {
            var package = this.Db.Packages.Find(id);
            package.Status = PackageStatus.Delivered;
            this.Db.Packages.Update(package);
            this.Db.SaveChanges();
        }

        public void CreatePackage(PackageCreateModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Recipient);
            var package = Mapper.Map<Package>(model);
            package.Recipient = user;
            this.Db.Packages.Add(package);
            this.Db.SaveChanges();
        }

        public PackageDetailsViewModel PackageDetailsById(int id)
        {
            var packageFromDb = this.Db.Packages.Include(x => x.Recipient).FirstOrDefault(x => x.Id == id);
            var packageDto = Mapper.Map<PackageDetailsViewModel>(packageFromDb);

            return packageDto;
        }

        private void CreateReceipt(User user, Package package)
        {
            decimal feePackage = (decimal)package.Weight * GlobalConstants.Constants.MultiplierConst;
            var receipt = new Receipt
            {
                IssuedOn = DateTime.UtcNow,
                Fee = feePackage,
                Package = package,
                Recipient = user
            };
            package.Status = PackageStatus.Acquired;
            this.Db.Packages.Update(package);
            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();
        }
    }
}