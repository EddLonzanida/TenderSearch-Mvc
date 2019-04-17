using Eml.Contracts.Entities;
using Eml.Mediator.Contracts;
using System;
using System.Threading.Tasks;
using TenderSearch.Data;
using ILogger = Eml.Logger.ILogger;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc />
    /// <summary>
    /// Uses Generic TKey id. Uses non-generic LayoutContentsCreateEditViewModel
    /// </summary>
    public abstract class UserManagerBase<T> : CrudControllerNoRepositoryWithContentBase<string, T>
        where T : class, IEntityBase<string>, new()
    {
        protected UserManagerBase(ILogger logger)
            : base(logger)
        {
        }

        protected UserManagerBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected abstract override Task FinalizeDelete(TenderSearchDb db, T itemFromDb, string newDeletionReason, DateTime timeStamp, string returnUrl);

        #region NOT NEEDED
        protected sealed override void SetModified(TenderSearchDb db, T item)
        {
        }

        protected sealed override async Task SaveChangesAsync(TenderSearchDb db)
        {
            await Task.Delay(1);
        }

        protected sealed override void SetUnchanged(TenderSearchDb db, T item)
        {
        }

        protected sealed override void DiscardChanges()
        {
        }
        #endregion // NOT NEEDED
    }
}
