using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using TenderSearch.Contracts.Infrastructure;

namespace TenderSearch.Web.Areas.Users.ViewModels
{
    public class AddressLayoutContentsCreateEditViewModel : LayoutContentsCreateEditViewModel<int, Address>
    {
        public Func<IEnumerable<SelectListItem>> GetAddressTypes { get; set; }
        public Func<IEnumerable<SelectListItem>> GetOwnerTypes { get; set; }
        public BarangaySuggestion InitialBarangaySuggestion { get; set; }
        public PersonSuggestion InitialPersonSuggestion { get; set; }
        public string InitialStreetAddress { get; set; }
        public string ActionName { get; }

        public bool CanShowOwner()
        {
            if (HasParent)
            {
                return false;
            }

            return ActionName == ActionNames.CreateWithParent || ActionName == ActionNames.Create;
        }

        public AddressLayoutContentsCreateEditViewModel()
        {
        }

        public AddressLayoutContentsCreateEditViewModel(Address item, string title1, string title2, string title3, int pageSize, string actionName, int labelClassColumnCount = 4, int parentId = default)
            : base(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
        {
            ActionName = actionName;
        }
    }
}