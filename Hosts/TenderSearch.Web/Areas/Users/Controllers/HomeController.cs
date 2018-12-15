using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Users.Controllers.BaseClasses;
using TenderSearch.Web.ViewModels.HomeViewModels;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : UsersHomeControllerBase
    {
        [ImportingConstructor]
        protected HomeController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected override Task<HomeIndexViewModel> GetHomeIndexViewModelAsync(string title1, List<string> rolesForUser)
        {
            const string title = "Users Home Page";

            return base.GetHomeIndexViewModelAsync(title, rolesForUser);
        }
    }
}