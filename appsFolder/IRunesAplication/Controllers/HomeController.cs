namespace IRunesAplication.Controllers
{
    using IRunes.Services;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;

    public class HomeController : BaseController
    {
        //Get
        public IHttpResponse Index(IHttpRequest request)
        {
            if (this.IsAuthenticated(request))
            {
                string username = request.Session.GetParameter(IRunesConstants.formUsername).ToString();
                this.ViewBag[IRunesConstants.formUsername] = username;
                return this.View($"HomeUser");
            }
            return this.View();
        }
    }
}