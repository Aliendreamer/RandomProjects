namespace Panda.App.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Domain.Enums;
    using Infrastructure.Constants;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Controllers;

    public class BaseController : Controller
    {
        protected override IViewable View([CallerMemberName]string actionName = "")
        {
            SetAccessButtons();

            return base.View(actionName);
        }

        protected void SetAccessButtons()
        {
            if (this.Identity == null)
            {
                this.Model.Data[GlobalConstants.ViewSetup.NotLogged] = GlobalConstants.Display.DisplayBlock;
                this.Model.Data[GlobalConstants.ViewSetup.IsLogged] = GlobalConstants.Display.DisplayNone;
                this.Model.Data[GlobalConstants.ViewSetup.IsAdmin] = GlobalConstants.Display.DisplayNone;
            }
            else if (this.Identity.Roles.Contains(nameof(UserRole.Admin)))
            {
                this.Model.Data[GlobalConstants.ViewSetup.NotLogged] = GlobalConstants.Display.DisplayNone;
                this.Model.Data[GlobalConstants.ViewSetup.IsLogged] = GlobalConstants.Display.DisplayBlock;
                this.Model.Data[GlobalConstants.ViewSetup.IsAdmin] = GlobalConstants.Display.DisplayBlock;
                this.Model.Data[GlobalConstants.ViewSetup.NotAdmin] = GlobalConstants.Display.DisplayNone;
            }
            else
            {
                this.Model.Data[GlobalConstants.ViewSetup.NotLogged] = GlobalConstants.Display.DisplayNone;
                this.Model.Data[GlobalConstants.ViewSetup.IsLogged] = GlobalConstants.Display.DisplayBlock;
                this.Model.Data[GlobalConstants.ViewSetup.IsAdmin] = GlobalConstants.Display.DisplayNone;
                this.Model.Data[GlobalConstants.ViewSetup.NotAdmin] = GlobalConstants.Display.DisplayBlock;
            }
        }
    }
}