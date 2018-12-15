using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;

namespace TenderSearch.Web.Areas.Users.ViewModels
{
    public class ContractLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<int, Contract>
    {
        public Func<IEnumerable<SelectListItem>> GetCompanies { get; set; }

        public Func<IEnumerable<SelectListItem>> GetContractTypes { get; set; }

        public ContractLayoutContentsCreateEditViewModel()
        {
        }

        public ContractLayoutContentsCreateEditViewModel(Contract item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4, int parentId = default)
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
        }
    }
}