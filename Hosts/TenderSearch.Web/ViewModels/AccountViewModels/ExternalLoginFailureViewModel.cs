using System;
using Eml.ControllerBase.Mvc.BaseClasses;
using System.Collections.Generic;
using Eml.ControllerBase.Mvc;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ExternalLoginFailureViewModel : LayoutViewModelBase
    {
        public ExternalLoginFailureViewModel()
        {
        }

        public ExternalLoginFailureViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
        }
    }
}