using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;

namespace TenderSearch.Web.Areas.Admins.ViewModels
{
    public class HasCustomDropDownLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<int, HasCustomDropDown>
    {
        public Func<IEnumerable<SelectListItem>> GetCustomDropDown { get; set; }

        public HasCustomDropDownLayoutContentsCreateEditViewModel()
        {
        }

        public HasCustomDropDownLayoutContentsCreateEditViewModel(HasCustomDropDown item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4, int parentId = default, string param = "")
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
        }
    }
}