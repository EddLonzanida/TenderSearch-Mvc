using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudController&lt;T&gt;" />
    public abstract class CrudControllerWithParent<T, TRepository> : CrudController<T>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerWithParent(TRepository repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerWithParent(IMediator mediator, TRepository repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}