namespace Exam.App.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Data;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Controllers;

    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Context = new ExamAppDb();
        }

        public ExamAppDb Context { get; set; }

        protected override IViewable View([CallerMemberName]string actionName = "")
        {
            SetAccessButtons();

            return base.View(actionName);
        }

        protected void SetAccessButtons()
        {
            if (this.Identity == null)
            {
                this.Model.Data["NotLogged"] = "block";
                this.Model.Data["IsLogged"] = "none";
                this.Model.Data["IsAdmin"] = "none";
            }
            else if (this.Identity.Roles.Contains("Admin"))
            {
                this.Model.Data["NotLogged"] = "none";
                this.Model.Data["IsLogged"] = "block";
                this.Model.Data["IsAdmin"] = "block";
                this.Model.Data["NotAdmin"] = "none";
            }
            else
            {
                this.Model.Data["NotLogged"] = "none";
                this.Model.Data["IsLogged"] = "block";
                this.Model.Data["IsAdmin"] = "none";
                this.Model.Data["NotAdmin"] = "block";
            }
        }
    }
}