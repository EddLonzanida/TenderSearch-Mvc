using TenderSearch.Data.Security;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TenderSearch.Web.Areas.UserManagers.ViewModels
{
    public class AspNetUserRoleLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<string, AspNetUserRole>
    {
        public Func<string, IEnumerable<SelectListItem>> GetRoles { get; set; }

        public AspNetUserRoleLayoutContentsCreateEditViewModel()
        {
        }

        public AspNetUserRoleLayoutContentsCreateEditViewModel(AspNetUserRole item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4, string parentId = null)
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
        }
    }
}