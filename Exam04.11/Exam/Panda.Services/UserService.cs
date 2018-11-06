namespace Panda.Services
{
    using Interfaces;
    using AutoMapper;
    using System.Linq;
    using Domain.Models;
    using System.Collections.Generic;
    using Infrastructure.ViewModels.InputModels;

    using SIS.Framework.Security;

    public class UserService : BaseService, IUserService
    {
        public IdentityUser Login(LogingInputViewModel model)
        {
            var userFromDb =
                this.Db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            var result = CurrentUser(userFromDb);

            return result;
        }

        public IdentityUser Register(RegisterInputViewModel model)
        {
            var user = Mapper.Map<User>(model);

            this.Db.Users.Add(user);
            this.Db.SaveChanges();

            var result = CurrentUser(user);

            return result;
        }

        private IdentityUser CurrentUser(User user)
        {
            if (user == null)
            {
                return null;
            }

            var identity = new IdentityUser
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Roles = new List<string> { user.Role.ToString() },
            };

            return identity;
        }
    }
}