namespace IRunesAplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using IRunes.Services;
    using IRunes.Services.Contracts;
    using Services;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;

    public abstract class BaseController
    {

        protected  IDictionary<string,string> ViewBag { get; set; }

        protected string GetCurrentControllerName() =>
            this.GetType().Name.Replace(IRunesConstants.ControllerDefaultName, string.Empty);

        protected IUserCookieService UserCookieService { get; set; }

        protected IAlbumService AlbumService { get; set; }

        protected IUserService UserService { get; set; }

        protected ITrackService TrackService { get; set; }

        protected BaseController()
        {
            this.UserService = new UserService();
            this.UserCookieService = new UserCookieService();
            this.AlbumService=new AlbumService();
            this.TrackService=new TrackService();
            this.ViewBag=new Dictionary<string, string>();
        }

        //TODO: cookie is disappearing
        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(IRunesConstants.authenticatingCookie))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(IRunesConstants.authenticatingCookie);
            var cookieContent = cookie.Value;
            var userName = this.UserCookieService.GetUserData(cookieContent);
            return userName;

        }
        protected string GetRelativePath()
        {
            string appPath = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(appPath);
            var root = parentDirectory.FullName;

            var parts = root.Split('\\', StringSplitOptions.RemoveEmptyEntries);
            var usefulParts = parts.Take(parts.Length - 2);

            var result = string.Join("\\", usefulParts) + "\\";
            return result;
        }

        protected bool IsAuthenticated(IHttpRequest request)
        {
            return request.Session.ContainsParameter(IRunesConstants.formUsername);
        }

        protected void SignInUSer(string username, IHttpRequest request, IHttpResponse response)
        {
            var userCookie = new HttpCookie(IRunesConstants.authenticatingCookie,
                this.UserCookieService.GetUserCookie(username));

            request.Session.AddParameter(IRunesConstants.formUsername, username);
            response.Cookies.Add(userCookie);
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
           
            string filePath = GetRelativePath() +
                              IRunesConstants.ViewsFolderName +
                              IRunesConstants.DelimiterChars +
                              this.GetCurrentControllerName() +
                              IRunesConstants.DelimiterChars +
                              viewName +
                              IRunesConstants.HtmlExtension;


            if (!File.Exists(filePath))
            {
                return new HtmlResult("Path is not valid", HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllText(filePath);

            foreach (var viewBagKey in ViewBag.Keys)
            {
                var dynamicPlaceHolder = $"{{{{{{{viewBagKey}}}}}}}";


                if (fileContent.Contains(dynamicPlaceHolder))
                {
                  fileContent = fileContent.Replace(dynamicPlaceHolder,
                                                this.ViewBag[viewBagKey]);
                }

            }


            var response = new HtmlResult(fileContent, HttpResponseStatusCode.Ok);

            return response;
        }
    }
}
