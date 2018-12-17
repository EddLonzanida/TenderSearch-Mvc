using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Admins.Controllers.BaseClasses;
using TenderSearch.Web.ViewModels.HomeViewModels;

namespace TenderSearch.Web.Areas.Admins.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Admins)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : AdminHomeControllerBase
    {
        [ImportingConstructor]
        protected HomeController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected override async Task<HomeIndexViewModel> GetHomeIndexViewModelAsync(string title1, List<string> rolesForUser)
        {
            const string title = "Admin Home Page";

            return await base.GetHomeIndexViewModelAsync(title, rolesForUser);
        }
    }
}