using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;

namespace TenderSearch.Web.ViewModels.AccountViewModels
{
    public class RegisterViewModel : LayoutViewModelBase
    {
        public RegisterModel RegisterModel { get; set; }

        public RegisterViewModel()
        {
        }

        public RegisterViewModel(string applicationName, string applicationVersion, string mvcActionName, string title1, 
            string area, Func<MenuItem> getMainMenus, string userName = "", IEnumerable<string> roles = null,
                                string email = "", string password = "", string confirmPassword = "") 
            : base(applicationName, applicationVersion, mvcActionName, title1, area, getMainMenus, roles)
        {
            RegisterModel = new RegisterModel { UserName = userName, Email = email, Password = password, ConfirmPassword = confirmPassword };
        }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

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
    }
}