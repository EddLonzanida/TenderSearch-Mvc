using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using TenderSearch.Data;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerBaseInt&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForCreateEditIndex<T, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        : CrudControllerBaseInt<T, IDataRepositoryBase<int, T, TenderSearchDb>, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, LayoutContentsDetailsDeleteViewModel<int, T>>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>, ILabelClassCount
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        protected CrudControllerForCreateEditIndex(IDataRepositoryBase<int, T, TenderSearchDb> repository, ILogger logger)
            : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditIndex(IMediator mediator, IDataRepositoryBase<int, T, TenderSearchDb> repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }

        protected override LayoutContentsDetailsDeleteViewModel<int, T> GetLayoutContentsViewModelForDetailsDelete(T item, string title1, string title2, string title3)
        {
            var contentsVm = new LayoutContentsDetailsDeleteViewModel<int, T>(item, title1, title2, title3);

            return contentsVm;
        }
    }
}