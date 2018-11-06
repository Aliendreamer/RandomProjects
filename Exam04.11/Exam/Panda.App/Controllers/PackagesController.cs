namespace Panda.App.Controllers
{
    using Services.Interfaces;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using Infrastructure.ViewModels.InputModels;

    public class PackagesController : BaseController
    {
        public PackagesController(IPackageService packageService)
        {
            PackageService = packageService;
        }

        private IPackageService PackageService { get; }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Create()
        {
            var users = this.PackageService.GetRecipientNames();

            this.Model.Data["Recipients"] = users;

            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(PackageCreateModel model)
        {
            this.PackageService.CreatePackage(model);
            return this.Create();
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Details(int id)
        {
            var package = this.PackageService.PackageDetailsById(id);
            this.Model.Data["Package"] = package;

            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Pending()
        {
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(this.Pending));

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
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(this.Shipped));

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
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(this.Delivered));

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
            this.PackageService.Ship(id);
            return this.Pending();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Deliver(int id)
        {
            this.PackageService.Deliver(id);
            return this.Shipped();
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Acquire(int id)
        {
            this.PackageService.Acquire(id, this.Identity.Username);
            return RedirectToAction("/");
        }
    }
}