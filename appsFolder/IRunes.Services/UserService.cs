namespace IRunes.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Domain;
    using IRunesAplication.Services;
    using SIS.HTTP.Requests;

    public class UserService:IUserService
    {
        protected IRunesDbContext Context { get; set; }

        protected  IHashService HashService { get; set; }

        protected  IUserCookieService UserCookieService { get; set; }
        public UserService()
        {
            this.HashService=new HashService();
            this.Context=new IRunesDbContext();
            this.UserCookieService=new UserCookieService();
        }


        public User GetUser(string input,string password)
        {

            string passwordhashed = this.HashService.Hash(password);
            User userFromDb = null;
            using (Context)
            {

                userFromDb = Context.Users
                    .FirstOrDefault(u=> u.Username ==input || u.Email==input && u.Password == passwordhashed);
            }

            return userFromDb;
        }

        public bool RegisterUser(IHttpRequest request)
        {
            string username = request.FormData[IRunesConstants.formUsername].ToString();
            string email = request.FormData[IRunesConstants.formEmail].ToString();
            string password = this.HashService.Hash(request.FormData[IRunesConstants.formPassword].ToString());
            string confirmpassword = this.HashService.Hash(request.FormData[IRunesConstants.formConfirmPassword].ToString());

            if (password != confirmpassword)
            {
                return false;
            }

            User user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            if (this.CheckUserExist(user))
            {
                return false;
            }


            Context.Users.Add(user);
            Context.SaveChangesAsync();           
            return true;
        }

        public bool CheckUserExist(User user)
        {
           return  this.Context.Users.Any(u =>u.Username == user.Username || u.Email==user.Email && u.Password == user.Password);
        }

        
    }
}
