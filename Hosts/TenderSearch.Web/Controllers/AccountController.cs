using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data.Security;
using TenderSearch.Web.Configurations;
using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.IdentityConfig;
using TenderSearch.Web.Utils;
using TenderSearch.Web.ViewModels.AccountViewModels;
using Eml.ControllerBase.Mvc.Configurations;
using Eml.Extensions;
using Eml.Logger;
using Eml.Mediator.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LogLevel = NLog.LogLevel;

namespace TenderSearch.Web.Controllers
{
    [Authorize]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Route("Account")]
    public class AccountController : ControllerOwinBase
    {
        [ImportingConstructor]
        public AccountController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        public AccountController(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(mediator, logger, userManager, signInManager)
        {
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public ActionResult Login(string returnUrl)
        {
            var vm = GetLoginViewModel(returnUrl);

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {

            var model = vm.LoginModel;
            var vm2 = GetLoginViewModel(returnUrl);

            vm2.LoginModel = vm.LoginModel;

            if (!ModelState.IsValid)
            {
                return View(vm2);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    var rolesForUser = await GetRolesForUserAsync(model.UserName);

                    if (rolesForUser.Count == 0)
                    {
                        ModelState.AddModelError("", "No roles assigned. Please contact your Administrator.");

                        return View(vm2);
                    }

                    if (!string.IsNullOrWhiteSpace(returnUrl)) return RedirectToLocal(returnUrl);

                    if (rolesForUser.Count > 1)
                    {
                        return RedirectToAction<DashboardController>(c => c.Index(), string.Empty);
                    }

                    foreach (var role in rolesForUser)
                    {

                        var eRole = (eUserRoles)Enum.Parse(typeof(eUserRoles), role);

                        switch (eRole)
                        {
                            case eUserRoles.Users:
                            case eUserRoles.Admins:
                            case eUserRoles.UserManagers:
                                return RedirectToAction<HomeController>(c => c.Index(), role);
                            default:
                                return RedirectToAction<AccountController>(c => c.Register());
                        }
                    }

                    break;
                case SignInStatus.LockedOut:

                    vm2.Title1 = "Locked Out";

                    return View("Lockout", vm2);

                case SignInStatus.RequiresVerification:

                    return RedirectToAction<AccountController>(c => c.SendCode(returnUrl, model.RememberMe));

                case SignInStatus.Failure:
                default:

                    ModelState.AddModelError("", "Invalid login attempt.");
                    
                    return View(vm2);
            }

            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("VerifyCode")]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync()) return PartialView("Error");

            var vm = GetVerifyCodeViewModel(provider, returnUrl, rememberMe);

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("VerifyCode")]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(vm.VerifyCodeModel.Provider, vm.VerifyCodeModel.Code, isPersistent: vm.VerifyCodeModel.RememberMe, rememberBrowser: vm.VerifyCodeModel.RememberBrowser);

            switch (result)
            {
                case SignInStatus.Success:

                    return RedirectToLocal(vm.ReturnUrl);

                case SignInStatus.LockedOut:

                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                case SignInStatus.Failure:
                default:

                    ModelState.AddModelError("", "Invalid code.");

                    return View(vm);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            var vm = GetRegisterViewModel();

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            var vm2 = GetRegisterViewModel();

            if (!ModelState.IsValid)
            {
                vm2.RegisterModel = vm.RegisterModel;

                return View(vm2);
            }

            var userName = vm.RegisterModel.UserName.ToProperCase();
            var user = new ApplicationUser { UserName = userName, Email = vm.RegisterModel.Email };
            var result = await UserManager.CreateAsync(user, vm.RegisterModel.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                //TODO Send email notification to UserManagers
                NotifyUserManagers(userName);

                var confirmRegistrationVm = GetConfirmEmailViewModel();

                return View("ConfirmRegistration", confirmRegistrationVm);
            }

            AddErrors(result);

            return View(vm2);
        }

        [HttpPost]
        [Route("NotifyUserManagers")]
        private void NotifyUserManagers(string userName)
        {
            const eArea currentStep = eArea.Registration;
            const string lineBreak = "<br>";
            const string qoute = "\"";

            var applicationNameConfig = new ApplicationNameConfig();
            var baseAddress = $"{Request.Url?.Scheme}://{Request.Url?.Authority}{Url.Content("~")}";
            var urlLink = Mailer.GetUrlLink(currentStep, baseAddress, userName);
            var emailStyleConfig = new EmailStyleConfig();
            var style = emailStyleConfig.Value;
            var messageBody = $"<p style={qoute}{style}{qoute}>" + $"Dear UserManagers,{lineBreak}{lineBreak}" +
                              $"<b>{userName}</b> is now registered.{lineBreak}{lineBreak}" +
                              $"Click <a href='{urlLink}'>here</a> to grant roles.{lineBreak}{lineBreak}" +
                              "This is an automated message. Do not reply." + "</p>";
            var message = new MailMessage
            {
                Subject = $"{applicationNameConfig.Value} {eArea.Registration} - {userName}",
                Body = messageBody
            };

            Mailer.SendEmail(currentStep, message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    const string errorMessage = "userId == null || code == null has return true.";

                    var vm1 = await GetErrorViewModel(null, errorMessage);

                    return PartialView("Error", vm1);
                }

                var result = await UserManager.ConfirmEmailAsync(userId, code);

                if (result.Succeeded)
                {
                    var vm = GetConfirmEmailViewModel();

                    return View("ConfirmEmail", vm);
                }
                else
                {
                    var vm3 = await GetErrorViewModel(null, "");

                    return View("Error", vm3);
                }
            }
            catch (InvalidOperationException ex)
            {
                var vm2 = await GetErrorViewModel(ex, ex.Message);

                return View("Error", vm2);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            var vm = GetForgotPasswordViewModel("ForgotPassword");

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            var user = await UserManager.FindByEmailAsync(vm.ForgotPasswordModel.Email);

            try
            {
                if (!ModelState.IsValid) return View(vm);

                if (user == null)
                {
                    vm.Title1 = "Forgot Password Confirmation";
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation", vm);
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Url?.Scheme);
                //  await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                Mailer.SendEmail(callbackUrl, user.UserName, vm.ForgotPasswordModel.Email);

                return RedirectToAction<AccountController>(c => c.ForgotPasswordConfirmation(), string.Empty);
                // If we got this far, something failed, redisplay form
            }
            catch (Exception ex)
            {
                var methodNamestring =
                    $"{MethodBase.GetCurrentMethod().DeclaringType?.FullName}.{MethodBase.GetCurrentMethod().Name}";

                return await GetExceptionErrors(ex, methodNamestring, user?.UserName);
            }
        }

        protected async Task<ActionResult> GetExceptionErrors(Exception ex, string methodName, string userName)
        {
            if (!Request.IsAjaxRequest())
            {
                var vm = await GetErrorViewModel(ex, "");

                return View("Error", vm);
            }

            var error = string.Format("<strong>Method:</strong> {1}<br>{0}<br><strong>Inner Exception:</strong> {2}", ex.Message, methodName, ex.InnerException);

            logger.Log.Log(LogLevel.Error, new Exception(error));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return Content(error, MediaTypeNames.Text.Html);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPasswordConfirmation")]
        public ActionResult ForgotPasswordConfirmation()
        {
            var vm = GetForgotPasswordViewModel("ForgotPasswordConfirmation");

            return View(vm);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(string code)
        {
            const string mvcActionName = "ResetPassword";

            if (string.IsNullOrWhiteSpace(code))
            {
                var errorViewModel = await GetErrorViewModel( null, "Code is empty.");

                return View("Error", errorViewModel);
            }

            var vm = GetResetPasswordViewModel(mvcActionName);

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpGet]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await UserManager.FindByEmailAsync(vm.ResetPasswordModel.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction<AccountController>(c => c.ResetPasswordConfirmation());
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, vm.ResetPasswordModel.Code, vm.ResetPasswordModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction<AccountController>(c => c.ResetPasswordConfirmation());
            }

            AddErrors(result);

            return View(vm);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPasswordConfirmation")]
        public ActionResult ResetPasswordConfirmation()
        {
            const string mvcActionName = "ResetPasswordConfirmation";

            var vm = GetResetPasswordViewModel(mvcActionName);

            vm.Title1 = "Reset password confirmation";

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ExternalLogin")]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SendCode")]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            const string mvcActionName = "SendCode";

            var userId = await SignInManager.GetVerifiedUserIdAsync();

            if (userId == null)
            {
                var errorViewModel = await GetErrorViewModel(null, "User is not verified.");

                return PartialView("Error", errorViewModel);
            }

            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new Eml.Extensions.SelectListItem { Text = purpose, Value = purpose });
            var vm = GetSendCodeViewModel(factorOptions, mvcActionName, returnUrl, rememberMe);

            return View(vm);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("SendCode")]
        public async Task<ActionResult> SendCode(SendCodeViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (await SignInManager.SendTwoFactorCodeAsync(vm.SendCodeModel.SelectedProvider))
            {
                return RedirectToAction<AccountController>(c => c.VerifyCode(vm.SendCodeModel.SelectedProvider, vm.ReturnUrl, vm.SendCodeModel.RememberMe));
            }

            var errorViewModel = await GetErrorViewModel(null, "_signInManager.SendTwoFactorCodeAsync returned false.");

            return PartialView("Error", errorViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ExternalLoginCallback")]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            const string mvcActionName = "ExternalLoginCallback";

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                // return RedirectToAction("Login");
                return RedirectToAction<AccountController>(c => c.Login(returnUrl));
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:

                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:

                    return View("Lockout");

                case SignInStatus.RequiresVerification:

                    return RedirectToAction<AccountController>(c => c.SendCode(returnUrl, false));

                case SignInStatus.Failure:
                default:

                    var loginProvider = loginInfo.Login.LoginProvider;
                    var vm = GetExternalLoginConfirmationViewModel(mvcActionName, returnUrl, loginInfo.Email, loginProvider);

                    return View("ExternalLoginConfirmation", vm);
            }
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ExternalLoginConfirmation")]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel vm, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction<ManageController>(c => c.Index(null));
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure", vm);
                }

                var user = new ApplicationUser { UserName = vm.ExternalLoginConfirmationModel.Email, Email = vm.ExternalLoginConfirmationModel.Email };
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);

                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("LogOff")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction<HomeController>(c => c.Index());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ExternalLoginFailure")]
        public ActionResult ExternalLoginFailure()
        {
            const string mvcActionName = "ExternalLoginCallback";

            var vm = GetExternalLoginFailure(mvcActionName);

            return View(vm);
        }

        #region Helpers
        private ConfirmEmailViewModel GetConfirmEmailViewModel()
        {
            const string title = "Confirm Email";
            const string returnUrl = "";
            const string mvcActionName = "ConfirmEmail";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new ConfirmEmailViewModel(applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private LoginViewModel GetLoginViewModel(string returnUrl)
        {
            const bool rememberMe = false;
            const string title1 = "Login";

            var area = GetAreaName();
            var mvcActionName = GetActionName();
            var userName = User.Identity.Name.ToProperCase();
            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;

            var vm = new LoginViewModel(userName, rememberMe, applicationName, applicationVersion, mvcActionName, title1, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private VerifyCodeViewModel GetVerifyCodeViewModel(string provider, string returnUrl, bool rememberMe)
        {
            const string code = "";
            const string errorMessage = "SignInManager.HasBeenVerifiedAsync has return false.";
            const bool rememberBrowser = false;
            const string mvcActionName = "VerifyCode";
            const string title = "Verify";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new VerifyCodeViewModel(provider, code, rememberBrowser, rememberMe, errorMessage, applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private RegisterViewModel GetRegisterViewModel()
        {
            const string returnUrl = "";
            const string mvcActionName = "Register";
            const string title = "Register";

            var userName = User.Identity.Name.ToProperCase();
            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new RegisterViewModel(applicationName, applicationVersion, mvcActionName, title, userName, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private ForgotPasswordViewModel GetForgotPasswordViewModel(string mvcActionName)
        {
            const string returnUrl = "";
            const string title = "Forgot your password?";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new ForgotPasswordViewModel(applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private ResetPasswordViewModel GetResetPasswordViewModel(string mvcActionName, string email = "", string password = "", string confirmPassword = "", string code = "")
        {
            const string returnUrl = "";
            const string title = "Reset password";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new ResetPasswordViewModel(applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area), null, email, password, confirmPassword, code)
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private SendCodeViewModel GetSendCodeViewModel(IEnumerable<Eml.Extensions.SelectListItem> providers, string mvcActionName, string returnUrl, bool rememberMe)
        {
            const string title = "Send";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new SendCodeViewModel(rememberMe, providers, applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private ExternalLoginConfirmationViewModel GetExternalLoginConfirmationViewModel(string mvcActionName, string returnUrl, string email, string loginProvider)
        {
            const string title = "Register";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new ExternalLoginConfirmationViewModel(loginProvider, email, applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area))
            {
                ReturnUrl = returnUrl
            };

            return vm;
        }

        private ExternalLoginFailureViewModel GetExternalLoginFailure(string mvcActionName)
        {
            const string title = "Login Failure";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var area = GetAreaName();
            var vm = new ExternalLoginFailureViewModel(applicationName, applicationVersion, mvcActionName, title, area, () => GetMainMenus(area));

            return vm;
        }

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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction<HomeController>(c => c.Index());
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public ChallengeResult(string provider, string redirectUri, string userId = null)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };

                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}