namespace Panda.App.Controllers
{
    using Services.Interfaces;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;

    public class HomeController : BaseController
    {
        public HomeController(IHomeService homeService)
        : base()
        {
            this.HomeService = homeService;
        }

        private IHomeService HomeService { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.Identity == null)
            {
                return this.View();
            }

            var packageRowsList = this.HomeService.GetHomeViewModels(this.Identity.Username);

            this.Model.Data["PackageRows"] = packageRowsList;
            this.Model.Data["Username"] = this.Identity.Username;

            return this.View($"LoggedIn");
        }
    }
}