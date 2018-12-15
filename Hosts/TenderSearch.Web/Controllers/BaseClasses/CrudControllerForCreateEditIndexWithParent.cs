using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForCreateEditIndex&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel&gt;" />
    public abstract class CrudControllerForCreateEditIndexWithParent<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        : CrudControllerForCreateEditIndex<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerForCreateEditIndexWithParent(TRepository repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditIndexWithParent(IMediator mediator, TRepository repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }

    /// <inheritdoc cref="CrudControllerForCreateEditIndexWithParent&lt;T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel&gt;" />
    public abstract class CrudControllerForCreateEditIndexWithParent<T, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>
        : CrudControllerForCreateEditIndexWithParent<T, IDataRepositoryBase<int, T>, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        protected CrudControllerForCreateEditIndexWithParent(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditIndexWithParent(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}