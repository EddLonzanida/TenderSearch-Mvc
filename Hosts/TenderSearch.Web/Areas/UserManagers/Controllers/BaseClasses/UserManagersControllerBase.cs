using TenderSearch.Web.Controllers.BaseClasses;
using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;

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

    public abstract class UserManagersControllerBase<T, TLayoutContentsCreateEditViewModel> 
        : UserManagerBase<T, TLayoutContentsCreateEditViewModel>
        where T : class, IEntityBase<string>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<string, T>
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