using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc;
using Eml.ControllerBase.Mvc.BaseClasses;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.CustomBinders;
using Eml.ControllerBase.Mvc.ViewModels;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data;
using TenderSearch.Web.IdentityConfig;
using TenderSearch.Web.Utils;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <summary>
    /// Uses Generic TKey id. Implementation of CrudControllerMvcBase with repository
    /// Uses int as an id
    /// Add Attribute: [Export].
    /// Add Attribute: [PartCreationPolicy(CreationPolicy.NonShared)].
    /// </summary>
    public abstract class CrudControllerBaseInt<T, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerWithRepositoryMvcBase<int, T, TenderSearchDb, TRepository, TLayoutContentsCreateEditViewModel, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T, TenderSearchDb>
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditWithEntityViewModel<int, T>
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteWithEntityViewModel<int, T>
    {
        private ApplicationUserManager _userManager;

        protected CrudControllerBaseInt(TRepository repository, ILogger logger) 
            : base(repository, logger)
        {
        }

        protected CrudControllerBaseInt(IMediator mediator, TRepository repository, ILogger logger) 
            : base(mediator, repository, logger)
        {
        }

        protected ApplicationUserManager UserManager => _userManager ??
                                                        (_userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());

        #region ICrudControllerBase
        [HttpGet]
        [Route("{parentId}/" + ActionNames.IndexWithParent)]
        public override async Task<ActionResult> IndexWithParent(int parentId, int? page = 1, bool? desc = false, int? sortColumn = 0, string searchTerm = "", string returnUrl = null, string param = null)
        {
            var pageValue = page ?? 1;
            var descValue = desc ?? false;
            var sortColumnValue = sortColumn ?? 0;

            return await DoIndexAsync(parentId, pageValue, descValue, sortColumnValue, searchTerm, returnUrl, true, param);
        }

        [HttpGet]
        [Route("")]
        public override async Task<ActionResult> Index(int? page = 1, bool? desc = false, int? sortColumn = 0, string searchTerm = "", string returnUrl = null, string param = null)
        {
            const bool isChildren = false;

            var pageValue = page ?? 1;
            var descValue = desc ?? false;
            var sortColumnValue = sortColumn ?? 0;

            return await DoIndexAsync(default, pageValue, descValue, sortColumnValue, searchTerm, returnUrl, isChildren, param);
        }

        [HttpGet]
        [Route(ActionNames.Details + "/{id}")]
        public override async Task<ActionResult> Details(int id, string returnUrl = null)
        {
            return await DoGetDetailsAsync(id, returnUrl);
        }

        [HttpGet]
        [Route("{parentId}/" + ActionNames.SuggestionsWithParent)]
        public override async Task<ActionResult> SuggestionsWithParent(int parentId, string search = "", string param = null)
        {
            return await DoGetSuggestionsAsync(parentId, search, param);
        }

        [HttpGet]
        [Route(ActionNames.Suggestions)]
        public override async Task<ActionResult> Suggestions(string search = "", string param = null)
        {
            return await DoGetSuggestionsAsync(default, search, param);
        }

        [HttpGet]
        [Route(ActionNames.Edit + "/{id}")]
        public override async Task<ActionResult> Edit(int id, string returnUrl = null, string param = null)
        {
            return await DoGetEditAsync(id, returnUrl, param);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(ActionNames.Edit)]
        public override async Task<ActionResult> Edit(LayoutWithContentViewModelBase<TLayoutContentsCreateEditViewModel> vm, string returnUrl = null, string param = null)
        {
            var item = vm.ContentViewModel.Item;

            return await DoPostEditAsync(item, returnUrl);
        }

        [HttpGet]
        [Route(ActionNames.Create)]
        public override async Task<ActionResult> Create(string returnUrl = null, string param = null)
        {
            return await DoGetCreateAsync(default, returnUrl, "");
        }

        [HttpGet]
        [Route("{parentId}/" + ActionNames.CreateWithParent)]
        public override async Task<ActionResult> CreateWithParent(int parentId, string returnUrl = null, string param = null)
        {
            return await DoGetCreateAsync(parentId, returnUrl, param);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(ActionNames.Create)]
        public override async Task<ActionResult> Create(LayoutWithContentViewModelBase<TLayoutContentsCreateEditViewModel> vm, string returnUrl = null, string param = null)
        {
            var item = vm.ContentViewModel.Item;

            return await DoPostCreateAsync(item, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{parentId}/" + ActionNames.CreateWithParent)]
        public override async Task<ActionResult> CreateWithParent(LayoutWithContentViewModelBase<TLayoutContentsCreateEditViewModel> vm, string returnUrl = null, string param = null)
        {
            var item = vm.ContentViewModel.Item;

            return await DoPostCreateAsync(item, returnUrl);
        }

        [HttpGet]
        [Route(ActionNames.Delete + "/{id}")]
        public override async Task<ActionResult> Delete(int id, string returnUrl = null)
        {
            return await DoGetDeleteAsync(id, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(ActionNames.Delete)]
        public override async Task<ActionResult> Delete([ModelBinder(typeof(DeletionReasonBinder))]T item, string returnUrl = null)
        {
            return await DoPostDeleteAsync(item, returnUrl);
        }
        #endregion // ICrudControllerBase

        protected override async Task<ActionResult> ShowError(UiMessage uiMessage, Exception exception = null)
        {
            if (exception == null)
            {
                exception = new Exception(uiMessage.GetMessages());
            }

            var errorMessage = uiMessage.GetHtmlMessages();
            var vm = await GetErrorViewModel(exception, errorMessage);

            LogError(uiMessage);

            return View("Error", vm);
        }

        protected override string GetApplicationVersion()
        {
            return MvcApplication.ApplicationVersion;
        }

        protected override string GetApplicationName()
        {
            return MvcApplication.ApplicationName;
        }

        protected override async Task<List<string>> GetRolesForUserAsync(string userName = "")
        {
            if (string.IsNullOrWhiteSpace(userName)) userName = User.Identity.Name;

            var user = await UserManager.FindByNameAsync(userName);
            var rolesForUser = await UserManager.GetRolesAsync(user.Id);

            return (List<string>)rolesForUser;
        }

        protected override async Task<ErrorViewModel> GetErrorViewModel(Exception exception, string errorMessage)
        {
            var controllerName = GetControllerName();
            var mvcActionName = GetActionName();
            var vm = await GetLayoutViewModelBase<ErrorViewModel>("Error");

            vm.ErrorMessage = errorMessage;
            vm.SetHandleErrorInfo(exception, controllerName, mvcActionName);

            return vm;
        }

        public override MenuItem GetMainMenus(string area)
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
            var pageTitle = new PageTitle(area, controllerName, mvcActionName);

            rolesForUser.Sort();

            vm.Set(applicationName, applicationVersion, pageTitle.Value, title1, area, () => GetMainMenus(area), rolesForUser);

            return vm;
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            // UserManager?.Dispose();
        }
    }
}