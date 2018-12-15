using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel : LayoutViewModelBase
    {
        public ExternalLoginConfirmationModel ExternalLoginConfirmationModel { get; set; }

        public string LoginProvider { get; set; }

        public ExternalLoginConfirmationViewModel()
        {
        }

        public ExternalLoginConfirmationViewModel(string loginProvider, string email, string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ExternalLoginConfirmationModel = new ExternalLoginConfirmationModel { Email = email };
            LoginProvider = loginProvider;
        }
    }

    public class ExternalLoginConfirmationModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}