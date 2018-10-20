namespace IRunes.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Domain;
    using SIS.Framework.Services.Contracts;

    public class UserService : IUserService
    {
        protected IRunesDbContext Context { get; set; }

        protected IHashService HashService { get; set; }

        protected IUserCookieService UserCookieService { get; set; }

        public UserService(IHashService hashService, IUserCookieService userCookieService)
        {
            this.HashService = hashService;
            this.UserCookieService = userCookieService;
            this.Context = new IRunesDbContext();
        }

        public User GetUser(User user)
        {
            string passwordHashed = this.HashService.Hash(user.Password);
            User userFromDb = null;
            using (Context)
            {
                userFromDb = Context.Users
                    .FirstOrDefault(u => u.Username == user.Username || u.Email == user.Email && u.Password == passwordHashed);
            }

            return userFromDb;
        }

        public bool RegisterUser(User user)
        {
            string password = this.HashService.Hash(user.Password);
            string confirmPassword = this.HashService.Hash(user.ConfirmPassword);

            if (password != confirmPassword)
            {
                return false;
            }

            if (this.CheckUserExist(user))
            {
                return false;
            }

            try
            {
                Context.Users.Add(user);
                Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("Something went wrong with db request to save user");
            }
        }

        private bool CheckUserExist(User user)
        {
            return this.Context.Users.Any(u => u.Username == user.Username || u.Email == user.Email && u.Password == user.Password);
        }
    }
}