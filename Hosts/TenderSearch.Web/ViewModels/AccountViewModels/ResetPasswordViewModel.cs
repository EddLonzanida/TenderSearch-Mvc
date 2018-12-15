using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel : LayoutViewModelBase
    {
        public ResetPasswordModel ResetPasswordModel { get; set; }

        public ResetPasswordViewModel()
        {
        }

        public ResetPasswordViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, string area, Func<MenuItem> getMainMenus, IEnumerable<string> roles = null,
            string email = "", string password = "", string confirmPassword = "", string code = "")
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            ResetPasswordModel = new ResetPasswordModel { Email = email, Password = password, ConfirmPassword = confirmPassword, Code = code };
        }
    }

    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}