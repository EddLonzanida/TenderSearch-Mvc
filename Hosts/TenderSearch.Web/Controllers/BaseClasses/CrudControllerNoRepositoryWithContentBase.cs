using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.Logger;
using Eml.Mediator.Contracts;
using X.PagedList;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    //public class CrudControllerNoRepositoryWithContentBase
    //{
    //}
    public abstract class CrudControllerNoRepositoryWithContentBase<TKey, T>
        : CrudControllerNoRepositoryBase<TKey, T, LayoutContentsCreateEditViewModel<TKey, T>, LayoutContentsIndexViewModel<TKey, T>, LayoutContentsDetailsDeleteViewModel<TKey, T>>
        where T : class, IEntityBase<TKey>, new()
    {
        protected CrudControllerNoRepositoryWithContentBase(ILogger logger) : base(logger)
        {
        }

        protected CrudControllerNoRepositoryWithContentBase(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        protected override LayoutContentsCreateEditViewModel<TKey, T> GetLayoutContentsViewModelForCreateEdit(T item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, TKey parentId, string param)
        {
            var contentsVm = new LayoutContentsCreateEditViewModel<TKey, T>(item, title1, title2, title3, pageSize, parentId: parentId);

            return contentsVm;
        }

        protected override LayoutContentsIndexViewModel<TKey, T> GetLayoutContentsViewModelForIndex(IPagedList<T> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, TKey parentId, string param)
        {
            var contentsVm = new LayoutContentsIndexViewModel<TKey, T>(pagedList, title1, title2, title3, search, targetTableBody, page, parentId)
            {
                Param = param
            };

            return contentsVm;
        }

        protected override LayoutContentsDetailsDeleteViewModel<TKey, T> GetLayoutContentsViewModelForDetailsDelete(T item, string title1, string title2, string title3)
        {
            var contentsVm = new LayoutContentsDetailsDeleteViewModel<TKey, T>(item, title1, title2, title3);

            return contentsVm;
        }

        //protected override LayoutContentsIndexViewModel<int, T> GetLayoutContentsViewModelForIndex(IPagedList<T> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        //{
        //}

        //protected override LayoutContentsDetailsDeleteViewModel<int, T> GetLayoutContentsViewModelForDetailsDelete(T item, string title1, string title2, string title3)
        //{
        //}
    }
}