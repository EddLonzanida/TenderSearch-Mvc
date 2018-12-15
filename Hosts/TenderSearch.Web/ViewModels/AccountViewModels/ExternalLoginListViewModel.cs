using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;
using Microsoft.Owin.Security;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ExternalLoginListViewModel : LayoutViewModelBase
    {
        public ExternalLoginListModel ExternalLoginListModel { get; set; }

        public ExternalLoginListViewModel()
        {
        }

        public ExternalLoginListViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string returnUrl, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ExternalLoginListModel = new ExternalLoginListModel { ReturnUrl = returnUrl };
        }

        public List<AuthenticationDescription> GetloginProviders(HttpContextBase context)
        {
            var loginProviders = context.GetOwinContext().Authentication.GetExternalAuthenticationTypes();

            return loginProviders.ToList();
        }
    }

    public class ExternalLoginListModel
    {
        public string ReturnUrl { get; set; }
    }
}