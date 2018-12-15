using System;
using System.Collections.Generic;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TenderSearch.Web.ViewModels.ManageViewModels
{
    public class ManageLoginsViewModel : LayoutViewModelBase
    {
        public string StatusMessage { get; set; }

        public bool HasLoginProviders { get; set; }
        public bool CanShowRemoveButton { get; set; }
        public ManageLoginsModel ManageLoginsModel { get; set; }

        public ManageLoginsViewModel()
        {
        }

        public ManageLoginsViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ManageLoginsModel = new ManageLoginsModel();
        }

    }

    public class ManageLoginsModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}