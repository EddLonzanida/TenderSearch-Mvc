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
    /// <inheritdoc cref="CrudControllerForIndex&lt;T, TLayoutContentsIndexViewModel&gt;" />
    public abstract class CrudControllerForIndexWithParent<T, TParent, TLayoutContentsIndexViewModel>
        : CrudControllerForIndex<T, TLayoutContentsIndexViewModel>, IControllerWithParent<int, T, TenderSearchDb>
        where T : class, IEntityBase<int>, new()
        where TParent : class, IEntityBase<int>, IEntitySoftdeletableBase, new()
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);

        public abstract override Task BeforeCreateSave(TenderSearchDb db, T item);

        public abstract override Task BeforeEditSave(TenderSearchDb db, T item);

        public abstract override Task<string> GetParentName(int parentId);

        public abstract override int GetParentId(T item);

        protected readonly IDataRepositorySoftDeleteInt<TParent> parentRepository;

        protected CrudControllerForIndexWithParent(IDataRepositoryBase<int, T, TenderSearchDb> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : this(null, repository, parentRepository, logger)
        {
        }

        protected CrudControllerForIndexWithParent(IMediator mediator
            , IDataRepositoryBase<int, T, TenderSearchDb> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : base(mediator, repository, logger)
        {
            this.parentRepository = parentRepository;
        }
    }
}