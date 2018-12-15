using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel : LayoutViewModelBase
    {
        public ForgotPasswordModel ForgotPasswordModel { get; set; }

        public ForgotPasswordViewModel()
        {
        }

        public ForgotPasswordViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null, string email = "")
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ForgotPasswordModel = new ForgotPasswordModel { Email = email };
        }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
