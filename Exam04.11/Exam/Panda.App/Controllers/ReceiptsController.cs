﻿namespace Panda.App.Controllers
{
    using Services.Interfaces;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ReceiptsController : BaseController
    {
        public ReceiptsController(IReceiptsService receiptsService)
        {
            ReceiptsService = receiptsService;
        }

        private IReceiptsService ReceiptsService { get; }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Index()
        {
            var displayModels = this.ReceiptsService.IndexModels(this.Identity.Username);
            this.Model.Data["Receipts"] = displayModels;
            return this.View();
        }

        [HttpGet]
        [Authorize("Admin", "User")]
        public IActionResult Details(int id)
        {
            var receipt = this.ReceiptsService.GetReceiptById(id);

            this.Model.Data["Receipt"] = receipt;

            return this.View();
        }
    }
}