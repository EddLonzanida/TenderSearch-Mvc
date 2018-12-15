using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eml.ControllerBase.Mvc.Infrastructures;
using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using TenderSearch.Contracts.Infrastructure;

namespace TenderSearch.Web.Areas.Users.ViewModels
{
    public class DependentLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<int, Dependent>
    {
        public Func<IEnumerable<SelectListItem>> GetDependentTypes { get; set; }
        public Func<IEnumerable<SelectListItem>> GetGenders { get; set; }
        public Func<IEnumerable<SelectListItem>> GetMaritalStatus { get; set; }
        public PersonSuggestion InitialPersonSuggestion { get; set; }
        public string ActionName { get;  }

        public bool CanShowEmployee()
        {
            if (HasParent)
            {
                return false;
            }

            return ActionName == ActionNames.CreateWithParent || ActionName == ActionNames.Create;
        }

        public DependentLayoutContentsCreateEditViewModel()
        {
        }

        public DependentLayoutContentsCreateEditViewModel(Dependent item, string title1, string title2, string title3, int pageSize, string actionName, int labelClassColumnCount = 4, int parentId = default)
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
            ActionName = actionName;
        }
    }
}