namespace Exam.App.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using ViewModels.InputModels;
    using ViewModels.OutputModels;

    public class PackagesController : BaseController
    {
        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Create()
        {
            var users = this.Context.Users.Select(x => new CreateDropDownMenuModel
            {
                Username = x.Username
            }).ToList();

            this.Model.Data["Recipients"] = users;

            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(PackageCreateModel model)
        {
            var user = this.Context.Users.FirstOrDefault(x => x.Username == model.Recipient);
            var package = new Package
            {
                Weight = double.Parse(model.Weight),
                ShippingAddress = model.ShippingAddress,
                Status = PackageStatus.Pending,
                EstimatedDeliveryDate = null,
                Recipient = user,
                Description = model.Description
            };
            this.Context.Packages.Add(package);
            this.Context.SaveChanges();
            return this.Create();
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Details(int id)
        {
            var package = this.Context.Packages.Include(x => x.Recipient).FirstOrDefault(x => x.Id == id);

            string deliveryDate = string.Empty;
            if (package.Status.ToString() == "Pending")
            {
                deliveryDate = "N/A";
            }
            else if (package.Status.ToString() == "Delivered")
            {
                deliveryDate = "Delivered";
            }
            else
            {
                deliveryDate = package.EstimatedDeliveryDate.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            }

            this.Model.Data["Recipient"] = package.Recipient.Username;
            this.Model.Data["Description"] = package.Description;
            this.Model.Data["Weight"] = package.Weight;
            this.Model.Data["DeliveryDate"] = deliveryDate;
            this.Model.Data["Status"] = package.Status.ToString();
            this.Model.Data["Address"] = package.ShippingAddress;

            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Pending()
        {
            var shipments = this.Context.Packages
                .Where(x => x.Status.ToString() == "Pending")
                .Select(ax => new PackageViewModel
                {
                    Id = ax.Id,
                    Description = ax.Description,
                    Recipient = ax.Recipient.Username,
                    ShippingAddress = ax.ShippingAddress,
                    Weight = ax.Weight
                }).ToArray();

            int count = 1;
            foreach (var s in shipments)
            {
                s.Number = count;
                count++;
            }

            this.Model.Data["PendingPackages"] = shipments;
            this.Model.Data["Details"] = "none";
            this.Model.Data["Ship"] = "block";
            this.Model.Data["Deliver"] = "none";

            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Shipped()
        {
            var shipments = this.Context.Packages
                .Where(x => x.Status.ToString() == "Shipped")
                .Select(ax => new PackageViewModel
                {
                    Id = ax.Id,
                    Description = ax.Description,
                    Recipient = ax.Recipient.Username,
                    ShippingAddress = ax.ShippingAddress,
                    Weight = ax.Weight
                }).ToArray();

            int count = 1;
            foreach (var s in shipments)
            {
                s.Number = count;
                count++;
            }

            this.Model.Data["ShippedPackages"] = shipments;
            this.Model.Data["Details"] = "none";
            this.Model.Data["Ship"] = "none";
            this.Model.Data["Deliver"] = "block";
            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Delivered()
        {
            var shipments = this.Context.Packages
                .Where(x => x.Status.ToString() == "Delivered")
                .Select(ax => new PackageViewModel
                {
                    Id = ax.Id,
                    Description = ax.Description,
                    Recipient = ax.Recipient.Username,
                    ShippingAddress = ax.ShippingAddress,
                    Weight = ax.Weight
                }).ToArray();

            int count = 1;
            foreach (var s in shipments)
            {
                s.Number = count;
                count++;
            }
            this.Model.Data["DeliveredPackages"] = shipments;
            this.Model.Data["Ship"] = "none";
            this.Model.Data["Deliver"] = "none";
            this.Model.Data["Details"] = "block";
            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Ship(int id)
        {
            var package = this.Context.Packages.Find(id);
            var rng = new Random();
            var days = rng.Next(19, 41);
            package.EstimatedDeliveryDate = DateTime.UtcNow;
            package.EstimatedDeliveryDate.Value.AddDays(days).ToShortDateString();
            package.Status = PackageStatus.Shipped;
            this.Context.Packages.Update(package);
            this.Context.SaveChanges();
            return RedirectToAction("/");
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Deliver(int id)
        {
            var package = this.Context.Packages.Find(id);
            package.Status = PackageStatus.Delivered;
            this.Context.Packages.Update(package);
            this.Context.SaveChanges();

            return RedirectToAction("/");
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Acquire(int id)
        {
            var user = this.Context.Users.FirstOrDefault(x => x.Username == this.Identity.Username);
            var package = this.Context.Packages.Find(id);
            decimal feePackage = (decimal)package.Weight * 2.67m;
            var receipt = new Receipt
            {
                IssuedOn = DateTime.UtcNow,
                Fee = feePackage,
                Package = package,
                Recipient = user
            };
            package.Status = PackageStatus.Acquired;
            this.Context.Packages.Update(package);
            this.Context.Receipts.Add(receipt);
            this.Context.SaveChanges();
            return RedirectToAction("/");
        }
    }
}