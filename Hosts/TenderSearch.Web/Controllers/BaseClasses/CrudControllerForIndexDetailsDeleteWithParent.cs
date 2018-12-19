using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForIndexDetailsDelete&lt;T, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForIndexDetailsDeleteWithParent<T, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerForIndexDetailsDelete<T, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerForIndexDetailsDeleteWithParent(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForIndexDetailsDeleteWithParent(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}