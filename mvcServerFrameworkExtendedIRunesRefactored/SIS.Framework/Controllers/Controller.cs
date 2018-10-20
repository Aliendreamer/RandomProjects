namespace SIS.Framework.Controllers
{
    using System.Runtime.CompilerServices;
    using ActionsResults;
    using ActionsResults.Contracts;
    using HTTP.Requests;
    using Models;
    using Utilities;
    using Views;

    public abstract class Controller
    {
        protected Controller()
        {
            this.ViewModel = new ViewModel();
        }

        public Model ModelState { get; } = new Model();

        public IHttpRequest Request { get; set; }

        public ViewModel ViewModel { get; set; }

        protected IViewable View([CallerMemberName] string viewName = "")
        {
            var controllerName = ControllerUtilities.GetControllerName(this);

            var viewFullyQualifiedName = ControllerUtilities
                .GetViewFullyQualifiedName(controllerName, viewName);

            var view = new View(viewFullyQualifiedName, this.ViewModel.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
            => new RedirectResult(redirectUrl);
    }
}
