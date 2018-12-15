using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using TenderSearch.Web.IdentityConfig;
using TenderSearch.Web.Utils;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.ViewModels;
using Eml.Logger;
using Eml.Mediator.Contracts;
using Microsoft.AspNet.Identity.Owin;
using TenderSearch.Contracts.Infrastructure;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    public abstract class ControllerOwinBase : ControllerMvcBase
    {
        private ApplicationUserManager _userManager;

        private ApplicationSignInManager _signInManager;

        protected ControllerOwinBase(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected ControllerOwinBase(IMediator mediator, ILogger logger, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : this(mediator, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        protected ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        protected async Task<List<string>> GetRolesForUserAsync(string userName = "")
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = User.Identity.Name;
            }

            var user = await UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new List<string>();
            }

            var rolesForUser = await UserManager.GetRolesAsync(user.Id);

            return (List<string>)rolesForUser;
        }

        protected override string GetApplicationName()
        {
            return MvcApplication.ApplicationName;
        }

        protected override string GetApplicationVersion()
        {
            return MvcApplication.ApplicationVersion;
        }

        protected override async Task<ErrorViewModel> GetErrorViewModel(Exception exception, string errorMessage)
        {
            const string returnUrl = "";

            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var mvcActionName = GetActionName();
            var controllerName = GetControllerName();
            var area = GetAreaName();
            var rolesForUser = await GetRolesForUserAsync(User.Identity.Name);

            var vm = GetErrorViewModel(applicationName, applicationVersion, area, controllerName, mvcActionName, exception, errorMessage, returnUrl, () => GetMainMenus(area), rolesForUser);

            return vm;
        }

        public MenuItem GetMainMenus(string area)
        {
            switch (area)
            {
                case MvcArea.Users:

                    return Users.GetMainMenus();

                case MvcArea.Admins:

                    return Admins.GetMainMenus();

                case MvcArea.UserManagers:

                    return UserManagers.GetMainMenus();

                default:
                    throw new NotImplementedException($"{area} is not yet implemented.");
            }
        }

        protected override async Task<TLayoutViewModelBase> GetLayoutViewModelBase<TLayoutViewModelBase>(string title1)
        {
            var applicationName = MvcApplication.ApplicationName;
            var applicationVersion = MvcApplication.ApplicationVersion;
            var rolesForUser = await GetRolesForUserAsync();
            var vm = new TLayoutViewModelBase();
            var area = GetAreaName();
            var mvcActionName = GetActionName();
            var controllerName = GetControllerName();
            var pageTitle = new PageTitle(area,controllerName,mvcActionName);

            rolesForUser.Sort();

            vm.Set(applicationName, applicationVersion, pageTitle.Value, title1, area, () => GetMainMenus(area), rolesForUser);

            return vm;
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(UserManager);
            disposables.Add(SignInManager);
        }
    }
}