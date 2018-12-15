using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.IdentityConfig;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Areas.Admins.Controllers.BaseClasses
{
    public abstract class AdminHomeControllerBase : HomeControllerBase
    {
        protected AdminHomeControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected AdminHomeControllerBase(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }
    }
}