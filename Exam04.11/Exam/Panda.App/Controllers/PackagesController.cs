namespace Panda.App.Controllers
{
    using Domain.Enums;
    using Services.Interfaces;
    using Infrastructure.Constants;
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
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Create()
        {
            var users = this.PackageService.GetRecipientNames();

            this.Model.Data["Recipients"] = users;

            return this.View();
        }

        [HttpPost]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Create(PackageCreateModel model)
        {
            this.PackageService.CreatePackage(model);
            return this.Create();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin), nameof(UserRole.User))]
        public IActionResult Details(int id)
        {
            var package = this.PackageService.PackageDetailsById(id);
            this.Model.Data["Package"] = package;

            return this.View();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Pending()
        {
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(PackageStatus.Pending));

            this.Model.Data["PendingPackages"] = shipments;
            this.Model.Data["Details"] = GlobalConstants.Display.DisplayNone;
            this.Model.Data["Ship"] = GlobalConstants.Display.DisplayBlock;
            this.Model.Data["Deliver"] = GlobalConstants.Display.DisplayNone;

            return this.View();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Shipped()
        {
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(PackageStatus.Shipped));

            this.Model.Data["ShippedPackages"] = shipments;
            this.Model.Data["Details"] = GlobalConstants.Display.DisplayNone;
            this.Model.Data["Ship"] = GlobalConstants.Display.DisplayNone;
            this.Model.Data["Deliver"] = GlobalConstants.Display.DisplayBlock;
            return this.View();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Delivered()
        {
            var shipments = this.PackageService.GetPackagesByShipmentStatus(nameof(PackageStatus.Delivered));

            this.Model.Data["DeliveredPackages"] = shipments;
            this.Model.Data["Ship"] = GlobalConstants.Display.DisplayNone;
            this.Model.Data["Deliver"] = GlobalConstants.Display.DisplayNone;
            this.Model.Data["Details"] = GlobalConstants.Display.DisplayBlock;
            return this.View();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Ship(int id)
        {
            this.PackageService.Ship(id);
            return this.Pending();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin))]
        public IActionResult Deliver(int id)
        {
            this.PackageService.Deliver(id);
            return this.Shipped();
        }

        [HttpGet]
        [Authorize(nameof(UserRole.Admin), nameof(UserRole.User))]
        public IActionResult Acquire(int id)
        {
            this.PackageService.Acquire(id, this.Identity.Username);
            return RedirectToAction("/");
        }
    }
}