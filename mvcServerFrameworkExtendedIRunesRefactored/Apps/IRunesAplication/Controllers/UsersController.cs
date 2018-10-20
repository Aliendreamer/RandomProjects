namespace IRunesApplication.Controllers
{
    using AutoMapper;
    using IRunes.Domain;
    using IRunes.Services.Contracts;
    using SIS.Framework.ActionsResults;
    using SIS.Framework.ActionsResults.Base;
    using SIS.Framework.Attributes.Methods;
    using SIS.Framework.Services.Contracts;
    using ViewModels;

    public class UsersController : BaseController
    {
        //GET
        public UsersController(IUserService userService, IUserCookieService userCookieService) : base(userCookieService)
        {
            this.UserService = userService;
        }

        protected IUserService UserService { get; }

        [HttpGet]
        public IActionResult Register()
        {
            this.SettingViewsBasedOnAccess();
            return this.View();
        }

        //Post
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = Mapper.Map<User>(model);
            var success = this.UserService.RegisterUser(user);
            if (success)
            {
                string username = user.Username ?? user.Email;
                var result = new RedirectResult("/");
                this.SignInUSer(username, this.Request);
                return result;
            }

            return this.Register();
        }

        //get login
        [HttpGet]
        public IActionResult Login()
        {
            this.SettingViewsBasedOnAccess();
            return this.View();
        }

        //post login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = Mapper.Map<User>(model);
            var userFromDb = this.UserService.GetUser(user);
            if (userFromDb != null)
            {
                this.SignInUSer(userFromDb.Username, this.Request);
                return this.RedirectToAction("/");
            }

            this.Error = "User credentials are not correct!";

            this.SettingViewsBasedOnAccess();
            return this.Login();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.Request.Session.ClearParameters();

            return this.RedirectToAction("/");
        }
    }
}