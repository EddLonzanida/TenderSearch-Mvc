using TenderSearch.Web.Controllers.BaseClasses;
using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses
{
    public abstract class UserManagersWithContentControllerBase<T, TLayoutContentsCreateEditViewModel> 
        : UserManagerWithContentBase<T, TLayoutContentsCreateEditViewModel>
        where T : class, IEntityBase<string>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditWithEntityViewModel<string, T>
    {
        protected UserManagersWithContentControllerBase(ILogger logger)
            : base(logger)
        {
        }

        protected UserManagersWithContentControllerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }
    }
}