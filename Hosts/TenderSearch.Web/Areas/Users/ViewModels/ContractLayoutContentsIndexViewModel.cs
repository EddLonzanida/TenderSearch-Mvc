using System;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using TenderSearch.Business.Common.Entities;
using X.PagedList;

namespace TenderSearch.Web.Areas.Users.ViewModels
{
    public class ContractLayoutContentsIndexViewModel : LayoutContentsIndexViewModel<int, Contract>
    {
        public Func<Contract, int?> GetExpirationCountDown { get; set; }

        public ContractLayoutContentsIndexViewModel()
        {
        }

        public ContractLayoutContentsIndexViewModel(IPagedList<Contract> pagedList, string title1, string title2, string title3, string searchTerm, string targetTableBody, int pageNumber, int parentId = default, string param = "")
            : base(pagedList, title1, title2, title3, searchTerm, targetTableBody, pageNumber, parentId)
        {
        }
    }
}