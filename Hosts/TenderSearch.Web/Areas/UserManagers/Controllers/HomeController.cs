using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.ViewModels.HomeViewModels;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses;

namespace TenderSearch.Web.Areas.UserManagers.Controllers
{
    [Authorize(Roles = Authorize.UserManagers)]
    [RouteArea(MvcArea.UserManagers)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : UserManagersHomeControllerBase
    {
        [ImportingConstructor]
        public HomeController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected override async Task<HomeIndexViewModel> GetHomeIndexViewModelAsync(string title1, List<string> rolesForUser)
        {
            const string title = "Users Home Page";

            return await base.GetHomeIndexViewModelAsync(title, rolesForUser);
        }

        public override async Task<ActionResult> Index()
        {
            await Task.Delay(1);

            return RedirectToAction<AspNetUserController>(c => c.Index(null, null, null, null, null, null), MvcArea.UserManagers);
        }
    }
}