namespace Exam.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using ViewModels.OutputModels;

    public class ReceiptsController:BaseController
    {
        [HttpGet]
        [Authorize("Admin","User")]
        public IActionResult Index()
        {
           
            if (this.Identity.Roles.Contains("Admin"))
            { 
             var receipts = this.Context.Receipts.Select(x=>new ReceiptDisplayModel
             {
                 Fee = x.Fee,
                 Id = x.Id,
                 IssuedOn = x.IssuedOn.ToShortDateString(),
                 Recipient = x.Recipient.Username
             }).ToList();
             this.Model.Data["Receipts"] = receipts;
            }
            else
            {
                var userReceipts = this.Context.Receipts.Where(x=>x.Recipient.Username==this.Identity.Username)
                    .Select(x => new ReceiptDisplayModel
                {
                    Fee = x.Fee,
                    Id = x.Id,
                    IssuedOn = x.IssuedOn.ToShortDateString(),
                    Recipient = x.Recipient.Username
                }).ToList();
                this.Model.Data["Receipts"] = userReceipts;
            }
             return this.View();
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Details(int id)
        {
            var receipt = this.Context.Receipts.Include(x=>x.Package)
                .Include(x=>x.Recipient)
                .FirstOrDefault(x=>x.Id==id);

            this.Model.Data["Number"] = receipt.Id;
            this.Model.Data["IssuedOn"] = receipt.IssuedOn.ToShortDateString();
            this.Model.Data["DeliveryAddress"] = receipt.Package.ShippingAddress;
            this.Model.Data["Weight"] = receipt.Package.Weight;
            this.Model.Data["Description"] = receipt.Package.Description;
            this.Model.Data["Recipient"] = receipt.Recipient.Username;
            this.Model.Data["Fee"] = receipt.Fee;

            return this.View();
        }
    }
}
