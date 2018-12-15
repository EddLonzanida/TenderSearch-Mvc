using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForCreateEdit&lt;T, TRepository, TLayoutContentsCreateEditViewModel&gt;" />
    public abstract class CrudControllerForCreateEditWithParent<T, TRepository, TLayoutContentsCreateEditViewModel>
        : CrudControllerForCreateEdit<T, TRepository, TLayoutContentsCreateEditViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerForCreateEditWithParent(TRepository repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditWithParent(IMediator mediator, TRepository repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }

    /// <inheritdoc />
    public abstract class CrudControllerForCreateEditWithParent<T, TLayoutContentsCreateEditViewModel>
        : CrudControllerForCreateEditWithParent<T, IDataRepositoryBase<int, T>, TLayoutContentsCreateEditViewModel>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
    {
        protected CrudControllerForCreateEditWithParent(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditWithParent(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}