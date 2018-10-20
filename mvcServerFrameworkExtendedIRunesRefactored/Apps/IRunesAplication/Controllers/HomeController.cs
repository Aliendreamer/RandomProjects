namespace IRunesApplication.Controllers
{
    using IRunes.Services;
    using SIS.Framework.ActionsResults.Base;
    using SIS.Framework.Attributes.Methods;
    using SIS.Framework.Services.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(IUserCookieService userCookieService)
            : base(userCookieService)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            this.SettingViewsBasedOnAccess();
            if (IsAuthenticated())
            {
                this.ViewModel.Data[IRunesConstants.formUsername] =
                    this.Request.Session.GetParameter(IRunesConstants.formUsername).ToString();
            }
            return this.View();
        }
    }
}