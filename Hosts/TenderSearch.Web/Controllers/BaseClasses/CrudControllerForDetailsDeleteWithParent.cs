using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForDetailsDelete&lt;T, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForDetailsDeleteWithParent<T, TParent, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerForDetailsDelete<T, TLayoutContentsDetailsDeleteViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TParent : class, IEntityBase<int>, IEntitySoftdeletableBase, new()
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);

        public abstract override Task BeforeCreateSave(T item);

        public abstract override Task BeforeEditSave(T item);

        public abstract override Task<string> GetParentName(int parentId);

        public abstract override int GetParentId(T item);

        protected readonly IDataRepositorySoftDeleteInt<TParent> parentRepository;

        protected CrudControllerForDetailsDeleteWithParent(IDataRepositoryBase<int, T> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : this(null, repository, parentRepository, logger)
        {
        }

        protected CrudControllerForDetailsDeleteWithParent(IMediator mediator
            , IDataRepositoryBase<int, T> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : base(mediator, repository, logger)
        {
            this.parentRepository = parentRepository;
        }
    }
}