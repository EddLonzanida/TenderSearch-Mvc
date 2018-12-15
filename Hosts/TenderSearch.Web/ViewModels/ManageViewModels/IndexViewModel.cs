using System;
using System.Collections.Generic;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;
using Microsoft.AspNet.Identity;

namespace TenderSearch.Web.ViewModels.ManageViewModels
{
    public class IndexViewModel : LayoutViewModelBase
    {
        public string StatusMessage { get; set; }

        public string UserId { get; set; }

        public IndexModel IndexModel { get; set; }
        
        public IndexViewModel()
        {
        }

        public IndexViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            IndexModel = new IndexModel();
        }
    }

    public class IndexModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }
}