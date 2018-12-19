using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using X.PagedList;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerBaseInt&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudController<T>
        : CrudControllerBaseInt<T, IDataRepositoryBase<int, T>, LayoutContentsCreateEditViewModel<int, T>, LayoutContentsIndexViewModel<int, T>, LayoutContentsDetailsDeleteViewModel<int, T>>
        where T : class, IEntityBase<int>, new()
    {
        protected CrudController(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudController(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
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

        protected override LayoutContentsDetailsDeleteViewModel<int, T> GetLayoutContentsViewModelForDetailsDelete(T item, string title1, string title2, string title3)
        {
            var contentsVm = new LayoutContentsDetailsDeleteViewModel<int, T>(item, title1, title2, title3);

            return contentsVm;
        }
    }
}