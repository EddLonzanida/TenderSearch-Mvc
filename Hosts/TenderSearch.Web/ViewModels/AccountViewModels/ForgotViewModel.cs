using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ForgotViewModel : LayoutViewModelBase
    {
        public ForgotModel ForgotModel { get; set; }

        public ForgotViewModel()
        {
        }

        public ForgotViewModel(string email, string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ForgotModel = new ForgotModel { Email = email };
        }
    }

    public class ForgotModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}