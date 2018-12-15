using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.ManageViewModels
{
    public class ChangePasswordViewModel : LayoutViewModelBase
    {
        public ChangePasswordModel ChangePasswordModel { get; set; }

        public ChangePasswordViewModel()
        {
        }

        public ChangePasswordViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null)
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ChangePasswordModel = new ChangePasswordModel();
        }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}