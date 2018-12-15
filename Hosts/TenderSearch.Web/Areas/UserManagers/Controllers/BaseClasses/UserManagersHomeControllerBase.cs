using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.IdentityConfig;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses
{
    public abstract class UserManagersHomeControllerBase : HomeControllerBase
    {
        protected UserManagersHomeControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected UserManagersHomeControllerBase(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }
    }
}