using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.IdentityConfig;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Areas.Users.Controllers.BaseClasses
{
    public abstract class UsersHomeControllerBase : HomeControllerBase
    {
        protected UsersHomeControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected UsersHomeControllerBase(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }
    }
}