using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using TenderSearch.Data;
using X.PagedList;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc />
    public abstract class CrudControllerForDetailsDelete<T, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerBaseInt<T, IDataRepositoryBase<int, T, TenderSearchDb>, LayoutContentsCreateEditViewModel<int, T>, LayoutContentsIndexViewModel<int, T>, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteWithEntityViewModel<int, T>
    {
        protected CrudControllerForDetailsDelete(IDataRepositoryBase<int, T, TenderSearchDb> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForDetailsDelete(IMediator mediator, IDataRepositoryBase<int, T, TenderSearchDb> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }

        protected override LayoutContentsCreateEditViewModel<int, T> GetLayoutContentsViewModelForCreateEdit(T item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var contentsVm = new LayoutContentsCreateEditViewModel<int, T>(item, title1, title2, title3, pageSize, parentId: parentId);

            return contentsVm;
        }

        protected override LayoutContentsIndexViewModel<int, T> GetLayoutContentsViewModelForIndex(IPagedList<T> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            var contentsVm = new LayoutContentsIndexViewModel<int, T>(pagedList, title1, title2, title3, search, targetTableBody, page, parentId)
            {
                Param = param
            };

            return contentsVm;
        }
    }
}