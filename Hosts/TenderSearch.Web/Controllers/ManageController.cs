using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TenderSearch.Data.Security;
using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.IdentityConfig;
using TenderSearch.Web.ViewModels.ManageViewModels;
using Eml.Logger;
using Eml.Mediator.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TenderSearch.Web.Controllers
{
    [Authorize]
    [Route("Manage")]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageController : ControllerOwinBase
    {
        [ImportingConstructor]
        public ManageController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        public ManageController(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }

        private async Task<IndexViewModel> GetIndexViewModel(string statusMessage)
        {
            const string title1 = "Manage Account";

            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            var applicationName = GetApplicationName();
            var applicationVersion = GetApplicationVersion();
            var area = GetAreaName();
            var mvcActionName = GetActionName();
            var rolesForUser = await GetRolesForUserAsync(userName);
            var model = new IndexModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            var vm = new IndexViewModel(applicationName, applicationVersion, mvcActionName, title1, area, () => GetMainMenus(area), rolesForUser)
            {
                UserId = userId,
                StatusMessage = statusMessage,
                IndexModel = model
            };

            return vm;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index(eManageMessageId? message)
        {
            var statusMessage = message == eManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                                : message == eManageMessageId.SetPasswordSuccess ? "Your password has been set."
                                : message == eManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                                : message == eManageMessageId.Error ? "An error has occurred."
                                : message == eManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                                : message == eManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                                : "";

            var vm = await GetIndexViewModel(statusMessage);

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("RemoveLogin")]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            eManageMessageId? message;

            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                message = eManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = eManageMessageId.Error;
            }

            return RedirectToAction<ManageController>(c => c.ManageLogins(message));
        }

        [HttpGet]
        [Route("AddPhoneNumber")]
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("AddPhoneNumber")]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);

            if (UserManager.SmsService == null)
                return RedirectToAction<ManageController>(c => c.VerifyPhoneNumber(model.Number));

            var message = new IdentityMessage
            {
                Destination = model.Number,
                Body = "Your security code is: " + code
            };

            await UserManager.SmsService.SendAsync(message);

            return RedirectToAction<ManageController>(c => c.VerifyPhoneNumber(model.Number));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("EnableTwoFactorAuthentication")]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction<ManageController>(c => c.Index(null));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("DisableTwoFactorAuthentication")]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction<ManageController>(c => c.Index(null));
        }

        [HttpGet]
        [Route("VerifyPhoneNumber")]
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);

            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("VerifyPhoneNumber")]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction<ManageController>(c => c.Index(eManageMessageId.AddPhoneSuccess));
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("RemovePhoneNumber")]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);

            if (!result.Succeeded)
            {
                return RedirectToAction<ManageController>(c => c.Index(eManageMessageId.Error));
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction<ManageController>(c => c.Index(eManageMessageId.RemovePhoneSuccess));
        }
        
        private async Task<ChangePasswordViewModel> GetChangePasswordViewModel(string returnUrl)
        {
            const string title1 = "Login";

            var userName = User.Identity.Name;
            var area = GetAreaName();
            var mvcActionName = GetActionName();
            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var rolesForUser = await GetRolesForUserAsync(userName);

            var vm = new ChangePasswordViewModel(applicationName, applicationVersion, mvcActionName, title1, area, () => GetMainMenus(area), rolesForUser)
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword()
        {
            const string returnUrl = "";

            var vm = await GetChangePasswordViewModel(returnUrl);

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            const string returnUrl = "";

            var model = vm.ChangePasswordModel;
            var vm1 = await GetChangePasswordViewModel(returnUrl);

            vm1.ChangePasswordModel = model;

            if (!ModelState.IsValid)
            {
                return View(vm1);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction<ManageController>(c => c.Index(eManageMessageId.ChangePasswordSuccess));
            }

            AddErrors(result);

            return View(vm1);
        }

        [HttpGet]
        [Route("SetPassword")]
        public ActionResult SetPassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("SetPassword")]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction<ManageController>(c => c.Index(eManageMessageId.SetPasswordSuccess));
            }

            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private async Task<ManageLoginsViewModel> GetManageLoginsViewModel(string statusMessage, ApplicationUser user)
        {
            const string title1 = "Manage your external logins";

            var userId = user.Id; // User.Identity.GetUserId();
            var applicationName = GetApplicationName();
            var applicationVersion = GetApplicationVersion();
            var area = GetAreaName();
            var mvcActionName = GetActionName();
            var loginProviders = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
            var userLogins = await UserManager.GetLoginsAsync(userId);
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            var rolesForUser = await GetRolesForUserAsync(User.Identity.Name);
            var model = new ManageLoginsModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            };
            var vm = new ManageLoginsViewModel(applicationName, applicationVersion, mvcActionName, title1, area, () => GetMainMenus(area), rolesForUser)
            {
                StatusMessage = statusMessage,
                HasLoginProviders = loginProviders.Any(),
                CanShowRemoveButton = user.PasswordHash != null || userLogins.Any(),
                ManageLoginsModel = model
            };

            return vm;
        }

        [HttpGet]
        [Route("ManageLogins")]
        public async Task<ActionResult> ManageLogins(eManageMessageId? message)
        {
            var statusMessage =
                message == eManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == eManageMessageId.Error ? "An error has occurred."
                : "";

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                var errorVm = await GetErrorViewModel(null, statusMessage);

                return View("Error", errorVm);
            }

            var vm = await GetManageLoginsViewModel(statusMessage, user);

            vm.Title1 = message.ToString();

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("LinkLogin")]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        [HttpGet]
        [Route("LinkLoginCallback")]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());

            if (loginInfo == null)
            {
                return RedirectToAction<ManageController>(c => c.ManageLogins(eManageMessageId.Error));
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);

            return result.Succeeded
                ? RedirectToAction<ManageController>(c => c.ManageLogins(null))
                : RedirectToAction<ManageController>(c => c.ManageLogins(eManageMessageId.Error));
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            return user?.PasswordHash != null;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            return user?.PhoneNumber != null;
        }

        public enum eManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
        #endregion
    }
}