using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForIndex&lt;T, TLayoutContentsIndexViewModel&gt;" />
    public abstract class CrudControllerForIndexWithParent<T, TLayoutContentsIndexViewModel>
        : CrudControllerForIndex<T, TLayoutContentsIndexViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerForIndexWithParent(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForIndexWithParent(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}