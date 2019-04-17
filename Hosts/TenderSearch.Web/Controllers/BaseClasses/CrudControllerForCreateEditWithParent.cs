using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;
using TenderSearch.Data;
using TenderSearch.Data.Contracts;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForCreateEdit&lt;T, TLayoutContentsCreateEditViewModel&gt;" />
    public abstract class CrudControllerForCreateEditWithParent<T, TParent, TLayoutContentsCreateEditViewModel>
        : CrudControllerForCreateEdit<T, TLayoutContentsCreateEditViewModel>, IControllerWithParent<int, T, TenderSearchDb>
        where T : class, IEntityBase<int>, new()
        where TParent : class, IEntityBase<int>, IEntitySoftdeletableBase, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditWithEntityViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);

        public abstract override Task BeforeCreateSave(TenderSearchDb db, T item);

        public abstract override Task BeforeEditSave(TenderSearchDb db, T item);

        public abstract override Task<string> GetParentName(int parentId);

        public abstract override int GetParentId(T item);

        protected readonly IDataRepositorySoftDeleteInt<TParent> parentRepository;

        protected CrudControllerForCreateEditWithParent(IDataRepositoryBase<int, T, TenderSearchDb> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : this(null, repository, parentRepository, logger)
        {
        }

        protected CrudControllerForCreateEditWithParent(IMediator mediator
            , IDataRepositoryBase<int, T, TenderSearchDb> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : base(mediator, repository, logger)
        {
            this.parentRepository = parentRepository;
        }
    }
}