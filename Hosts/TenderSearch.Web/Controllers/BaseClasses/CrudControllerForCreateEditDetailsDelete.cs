using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using X.PagedList;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerBaseInt&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForCreateEditDetailsDelete<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerBaseInt<T, TRepository, TLayoutContentsCreateEditViewModel, LayoutContentsIndexViewModel<int, T>, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>, ILabelClassCount
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        protected CrudControllerForCreateEditDetailsDelete(TRepository repository, ILogger logger)
            : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditDetailsDelete(IMediator mediator, TRepository repository, ILogger logger)
            : base(mediator, repository, logger)
        {
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

    /// <inheritdoc />
    public abstract class CrudControllerForCreateEditDetailsDelete<T, TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerForCreateEditDetailsDelete<T, IDataRepositoryBase<int, T>, TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>, ILabelClassCount
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        protected CrudControllerForCreateEditDetailsDelete(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditDetailsDelete(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}