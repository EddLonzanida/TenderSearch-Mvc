using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using TenderSearch.Web.Controllers.BaseClasses;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Controllers
{
    [Route("Home")]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : HomeControllerBase
    {
        [ImportingConstructor]
        protected HomeController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        //protected virtual async Task<HomeIndexViewModel> GetHomeIndexViewModelAsync()
        //{
        //    const string title1 = "Home";

        //    var applicationName = GetApplicationName();
        //    var applicationVersion = GetApplicationVersion();
        //    var area = GetAreaName();
        //    var mvcActionName = GetActionName();
        //    var vm = new HomeIndexViewModel(applicationName, applicationVersion, mvcActionName, title1, area);

        //    return await Task.FromResult(vm);
        //}

        //[HttpGet]
        //[Route("")]
        //public async Task<ActionResult> Index()
        //{
        //    var vm = await GetHomeIndexViewModelAsync();

        //    return View(vm);
        //}

        [HttpGet]
        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        [Route("Contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return RedirectToAction<AccountController>(c => c.Register());
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return RedirectToAction<AccountController>(c => c.ForgotPassword());
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
        }
        protected override string GetApplicationName()
        {
            return MvcApplication.ApplicationName;
        }

        protected override string GetApplicationVersion()
        {
            return MvcApplication.ApplicationVersion;
        }

    }
}