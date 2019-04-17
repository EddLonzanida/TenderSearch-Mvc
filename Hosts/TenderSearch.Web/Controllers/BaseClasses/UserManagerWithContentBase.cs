using System;
using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.BaseClasses;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.Infrastructures;
using Eml.DataRepository;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;
using TenderSearch.Data;
using ILogger = Eml.Logger.ILogger;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc />
    /// <summary>
    /// Uses Generic TKey id. Uses generic TLayoutContentsCreateEditViewModel
    /// </summary>
    public abstract class UserManagerWithContentBase<T, TLayoutContentsCreateEditViewModel>
        : CrudControllerNoRepositoryWithContentBase<string, T>
        where T : class, IEntityBase<string>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditWithEntityViewModel<string, T>
    {
        protected UserManagerWithContentBase(ILogger logger)
            : base(logger)
        {
        }

        protected UserManagerWithContentBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected abstract override Task FinalizeDelete(TenderSearchDb db, T itemFromDb, string newDeletionReason, DateTime timeStamp, string returnUrl);

        protected abstract TLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(T item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4, string parentId = default);

        protected override async Task<ILayoutViewModelBase> GetViewModelForCreateEditAsync(T item, string returnUrl, eAction action, string parentId, string param)
        {
            var pageSizeConfig = new PageSizeConfig();
            var (title1, title2, title3) = await GetTitle123Async(item, parentId, action);
            var pageSize = pageSizeConfig.Value;
            var contentsVm = GetLayoutContentsViewModelForCreateEdit(item, title1, title2, title3, pageSize, parentId: parentId);
            var vm = await GetLayoutViewModelBase<LayoutWithContentViewModelBase<TLayoutContentsCreateEditViewModel>>(title1);

            vm.AllowKnockoutJs = GetAllowKnockoutJs(param);
            vm.ReturnUrl = returnUrl;
            vm.ContentViewModel = contentsVm;

            return vm;
        }
    }
}