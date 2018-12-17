using System;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using X.PagedList;

namespace TenderSearch.Web.Areas.Admins.ViewModels
{
    public class HasCustomActionForIndexLayoutContentsIndexViewModel : LayoutContentsIndexViewModel<int, HasCustomActionForIndex>
    {
        public Func<HasCustomActionForIndex, int?> GetExpirationCountDown { get; set; }

        public HasCustomActionForIndexLayoutContentsIndexViewModel()
        {
        }

        public HasCustomActionForIndexLayoutContentsIndexViewModel(IPagedList<HasCustomActionForIndex> pagedList, string title1, string title2, string title3, string searchTerm, string targetTableBody, int pageNumber, int parentId = default, string param = "")
            : base(pagedList, title1, title2, title3, searchTerm, targetTableBody, pageNumber, parentId)
        {
        }
    }
}