using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Web.IdentityConfig;
using TenderSearch.Web.ViewModels.HomeViewModels;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    public abstract class HomeControllerBase : ControllerOwinBase
    {
        protected HomeControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected HomeControllerBase(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }

        protected virtual async Task<HomeIndexViewModel> GetHomeIndexViewModelAsync(string title1, List<string> rolesForUser)
        {
            var vm = await GetLayoutViewModelBase<HomeIndexViewModel>(title1);

            return await Task.FromResult(vm);
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult> Index()
        {
            return await DoGetIndex();
        }

        protected virtual async Task<ActionResult> DoGetIndex()
        {
            var rolesForUser = new List<string>();

            if (!string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                rolesForUser = await GetRolesForUserAsync(User.Identity.Name);
            }

            var vm = await GetHomeIndexViewModelAsync("Home", rolesForUser);

            return View(vm);
        }
    }
}