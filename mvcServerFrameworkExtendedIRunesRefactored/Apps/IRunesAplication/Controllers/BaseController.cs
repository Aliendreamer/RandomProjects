namespace IRunesApplication.Controllers
{
    using IRunes.Data;
    using IRunes.Services;
    using SIS.Framework.Controllers;
    using SIS.Framework.Services.Contracts;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Requests;

    public abstract class BaseController : Controller
    {
        protected IUserCookieService UserCookieService { get; }

        protected string Error { get; set; }

        protected BaseController(IUserCookieService userCookieService)
        : base()
        {
            this.UserCookieService = userCookieService;
            this.Db = new IRunesDbContext();
        }

        protected IRunesDbContext Db { get; }

        protected bool IsAuthenticated()
        {
            return this.Request.Session.ContainsParameter(IRunesConstants.formUsername);
        }

        protected void SignInUSer(string username, IHttpRequest request)
        {
            var userCookie = new HttpCookie(IRunesConstants.authenticatingCookie,
                this.UserCookieService.GetUserCookie(username));

            request.Session.AddParameter(IRunesConstants.formUsername, username);
            request.Cookies.Add(userCookie);
        }

        protected void SettingViewsBasedOnAccess()
        {
            if (!IsAuthenticated())
            {
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderError] = "none";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderErrorMessage] = "none";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderAuthentication] = "none";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderNotAuthenticated] = "inline";
            }
            else
            {
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderError] = "none";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderErrorMessage] = "none";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderAuthentication] = "inline";
                this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderNotAuthenticated] = "none";
            }

            if (this.Error == null) return;
            this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderError] = "inline";
            this.ViewModel.Data[IRunesConstants.LayoutPlaceHolderErrorMessage] = this.Error;
        }
    }
}