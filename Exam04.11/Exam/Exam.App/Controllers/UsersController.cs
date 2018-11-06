namespace Exam.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Enums;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;
    using ViewModels.InputModels;

    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LogingInputViewModel model)
        {
            var user = this.Context.Users.FirstOrDefault(x =>
                x.Username == model.Username && x.Password == model.Password);

            if (user == null)
            {
                return this.View();
            }

            SignIn(new IdentityUser
            {
                Username = model.Username,
                Email = user.Email,
                Password = user.Password,
                Roles = new List<string> { user.Role.ToString() },
            });

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterInputViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,            
                Role = this.Context.Users.Any() ? UserRole.User : UserRole.Admin
            };

            SignIn(new IdentityUser
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Roles = new List<string> { user.Role.ToString() },
            });
            this.Context.Users.Add(user);
            this.Context.SaveChanges();

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