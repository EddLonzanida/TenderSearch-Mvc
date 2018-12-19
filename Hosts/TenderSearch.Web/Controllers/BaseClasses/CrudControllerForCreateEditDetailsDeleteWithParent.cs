using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudControllerForCreateEditDetailsDelete&lt;T, TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel&gt;" />
    public abstract class CrudControllerForCreateEditDetailsDeleteWithParent<T, TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerForCreateEditDetailsDelete<T,  TLayoutContentsCreateEditViewModel, TLayoutContentsDetailsDeleteViewModel>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);
        public abstract override Task BeforeCreateSave(T item);
        public abstract override Task BeforeEditSave(T item);
        public abstract override Task<string> GetParentName(int parentId);
        public abstract override int GetParentId(T item);

        protected CrudControllerForCreateEditDetailsDeleteWithParent(IDataRepositoryBase<int, T> repository, ILogger logger) 
            : base(repository, logger)
        {
        }

        protected CrudControllerForCreateEditDetailsDeleteWithParent(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }
    }
}