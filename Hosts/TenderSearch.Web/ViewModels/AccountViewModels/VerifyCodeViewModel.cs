using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class VerifyCodeViewModel : LayoutViewModelBase
    {
        public VerifyCodeModel VerifyCodeModel { get; set; }

        public string ErrorMessage { get; set; }

        public VerifyCodeViewModel()
        {
        }

        public VerifyCodeViewModel(string provider, string code, bool rememberBrowser, bool rememberMe, string errorMessage, string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            VerifyCodeModel = new VerifyCodeModel { Provider = provider, Code = code, RememberBrowser = rememberBrowser, RememberMe = rememberMe };
            ErrorMessage = errorMessage;
        }
    }

    public class VerifyCodeModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}