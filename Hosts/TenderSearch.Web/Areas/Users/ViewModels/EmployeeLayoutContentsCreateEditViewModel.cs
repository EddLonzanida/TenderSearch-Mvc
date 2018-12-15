using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;

namespace TenderSearch.Web.Areas.Users.ViewModels
{
    public class EmployeeLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<int, Employee>
    {
        public Func<IEnumerable<SelectListItem>> GetRankTypes { get; set; }
        public Func<IEnumerable<SelectListItem>> GetCategoryTypes { get; set; }
        public Func<IEnumerable<SelectListItem>> GetStatusTypes { get; set; }
        public Func<IEnumerable<SelectListItem>> GetGenders { get; set; }
        public Func<IEnumerable<SelectListItem>> GetMaritalStatus { get; set; }
        public Func<IEnumerable<SelectListItem>> GetDepartments { get; set; }
        
        public EmployeeLayoutContentsCreateEditViewModel()
        {
        }

        public EmployeeLayoutContentsCreateEditViewModel(Employee item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4, int parentId = default)
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
        }
    }
}