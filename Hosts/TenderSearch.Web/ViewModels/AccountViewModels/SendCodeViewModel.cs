using System;
using System.Collections.Generic;
using System.Linq;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;
using Eml.Extensions;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class SendCodeViewModel : LayoutViewModelBase
    {
        public SendCodeModel SendCodeModel { get; set; }

        public SendCodeViewModel()
        {
        }

        public SendCodeViewModel(bool rememberMe, IEnumerable<SelectListItem> providers,
            string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            var mvcSelectListItem = providers.ToList().ConvertAll(x => new System.Web.Mvc.SelectListItem { Text = x.Text, Selected = x.Selected, Value = x.Value });

            SendCodeModel = new SendCodeModel { ReturnUrl = ReturnUrl, RememberMe = rememberMe, Providers = mvcSelectListItem };
        }
    }

    public class SendCodeModel
    {
        public string SelectedProvider { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}