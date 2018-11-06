namespace Exam.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;
    using ViewModels.OutputModels;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (this.Identity == null)
            {
                return this.View();
            }

            var packageRowsList = new List<HomeIndexViewModel>();

            var allPackages = this.Context.Packages
                .Where(x => x.Recipient.Username == this.Identity.Username).ToList();

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
                Packages = allPackages.Where(x => x.Status.ToString() == "Delivered").Select(x => new PackageHomeModel
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

            this.Model.Data["PackageRows"] = packageRowsList;
            this.Model.Data["Username"] = this.Identity.Username;

            return this.View($"LoggedIn");
        }
    }
}