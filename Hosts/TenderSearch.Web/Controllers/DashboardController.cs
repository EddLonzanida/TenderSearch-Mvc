using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.ViewModels.DashboardViewModels;
using Eml.Extensions;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Controllers
{
    [Authorize]
    [Route("Dashboard")]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardController : ControllerOwinBase
    {
        [ImportingConstructor]
        protected DashboardController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected virtual async Task<DashboardIndexViewModel> GetDashboardIndexViewModelAsync(List<string> rolesForUser)
        {
            const string title1 = "Select your destination";

            var vm = await GetLayoutViewModelBase<DashboardIndexViewModel>(title1);

            rolesForUser.Sort();

            vm.DasboardItems = rolesForUser;
            vm.UserName = User.Identity.Name.ToProperCase();

            return await Task.FromResult(vm);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var rolesForUser = await GetRolesForUserAsync(User.Identity.Name);

            //no need to  display dashboard if the user have 1 role. applicable only to users with multiple roles
            if (rolesForUser.Count == 1) return Goto(rolesForUser.First());

            var vm = await GetDashboardIndexViewModelAsync(rolesForUser);

            //  var model = rolesForUser.ToList();

            return View(vm);
        }

        [HttpGet]
        [Route("Jump")]
        public ActionResult Jump(string jump)
        {
            var eRole = (eUserRoles)Enum.Parse(typeof(eUserRoles), jump);

            switch (eRole)
            {
                case eUserRoles.UserManagers:

                    return RedirectToAction<Areas.UserManagers.Controllers.HomeController>(c => c.Index(), jump);

                case eUserRoles.Users:

                    return RedirectToAction<Areas.Users.Controllers.HomeController>(c => c.Index(), jump);

                case eUserRoles.Admins:

                    return RedirectToAction<Areas.Admins.Controllers.HomeController>(c => c.Index(), jump);

                default:
                    return RedirectToAction<AccountController>(c => c.Register());
            }
        }
    }
}