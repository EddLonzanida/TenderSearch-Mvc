using Eml.Contracts.Entities;
using Eml.Logger;
using Eml.Mediator.Contracts;
using TenderSearch.Web.Controllers.BaseClasses;

namespace TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses
{
    public abstract class UserManagersControllerBase<T> : UserManagerBase<T>
        where T : class, IEntityBase<string>, new()
    {
        protected UserManagersControllerBase(ILogger logger)
            : base(logger)
        {
        }

        protected UserManagersControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }
    }
}