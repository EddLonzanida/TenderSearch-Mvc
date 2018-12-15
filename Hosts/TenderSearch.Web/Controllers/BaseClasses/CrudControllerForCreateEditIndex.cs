using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerBaseInt&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForCreateEditIndex<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        : CrudControllerBaseInt<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, LayoutContentsDetailsDeleteViewModel<int, T>>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>, ILabelClassCount
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        protected CrudControllerForCreateEditIndex(TRepository repository, ILogger logger)
            : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditIndex(IMediator mediator, TRepository repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }

        protected override LayoutContentsDetailsDeleteViewModel<int, T> GetLayoutContentsViewModelForDetailsDelete(T item, string title1, string title2, string title3)
        {
            var contentsVm = new LayoutContentsDetailsDeleteViewModel<int, T>(item, title1, title2, title3);

            return contentsVm;
        }
    }


    /// <inheritdoc />
    public abstract class CrudControllerForCreateEditIndex<T, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        : CrudControllerForCreateEditIndex<T, IDataRepositoryBase<int, T>, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>, ILabelClassCount
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        protected CrudControllerForCreateEditIndex(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditIndex(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}