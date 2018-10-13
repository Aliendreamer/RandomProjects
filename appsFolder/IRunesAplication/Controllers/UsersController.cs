namespace IRunesAplication.Controllers
{ 
    using IRunes.Domain;
    using IRunes.Services;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;


    public class UsersController:BaseController
    {

        //GET
        public IHttpResponse Register()
        {
            return this.View();           
        }
        
        //Post
        public IHttpResponse Register(IHttpRequest request)
        {

            if (!this.UserService.RegisterUser(request))
            {
                return this.Register();
            }

            string username = request.FormData[IRunesConstants.formUsername].ToString();

            var response=  new RedirectResult("/home/index");

            this.SignInUSer(username,request,response);

            return response;

        }
        //get login
        public IHttpResponse Login()
        {
            return this.View();
        }

        //post login

        public  IHttpResponse Login(IHttpRequest request)
        {
            string loginInput = request.FormData[IRunesConstants.loginInputPlaceholder].ToString();
            string password = request.FormData[IRunesConstants.formPassword].ToString();

            User user = this.UserService.GetUser(loginInput, password);

            if (user == null)
            {
                return new RedirectResult("/users/register");
            }
            
            var response=  new RedirectResult("/home/index");
            SignInUSer(user.Username,request,response);

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
           request.Session.ClearParameters(); 

            return  new RedirectResult("/");
        }
    }
}
