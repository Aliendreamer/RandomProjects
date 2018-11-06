namespace Panda.App.Controllers
{
    using Services.Interfaces;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;
    using Infrastructure.ViewModels.InputModels;

    public class UsersController : BaseController
    {
        public UsersController(IUserService userService)
        : base()
        {
            this.UserService = userService;
        }

        private IUserService UserService { get; set; }

        [HttpGet]
        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LogingInputViewModel model)
        {
            var user = this.UserService.Login(model);

            if (user == null)
            {
                return this.View();
            }

            SignIn(user);

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterInputViewModel model)
        {
            var user = this.UserService.Register(model);
            this.SignIn(user);

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            SignOut();
            return RedirectToAction("/");
        }
    }
}