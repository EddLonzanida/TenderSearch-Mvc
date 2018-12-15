using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.Extensions;
using Eml.ControllerBase.Mvc.Infrastructures;
using Eml.ControllerBase.Mvc.ViewModels;
using Eml.DataRepository;
using Eml.Extensions;
using Eml.Logger;
using Eml.Mediator.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data;
using TenderSearch.Data.Security;
using TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses;
using TenderSearch.Web.Areas.UserManagers.ViewModels;
using X.PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.UserManagers.Controllers
{
    [Authorize(Roles = Authorize.UserManagers)]
    [RouteArea(MvcArea.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AspNetUserRoleController : UserManagersControllerBase<AspNetUserRole, AspNetUserRoleLayoutContentsCreateEditViewModel>
    {
        [ImportingConstructor]
        public AspNetUserRoleController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected override async Task<IPagedList<AspNetUserRole>> GetItemsAsync(string parentId, int page, bool desc, int sortColumn, string search, string param)
        {
            var pageSize = new PageSizeConfig();
            var roles = await UserManager.GetRolesAsync(parentId);
            var user = await UserManager.FindByIdAsync(parentId);

            return roles
                .Where(r => search == null || r.Contains(search))
                .OrderBy(r => r)
                .Select(r => new AspNetUserRole
                {
                    Id = r,
                    RoleName = r,
                    AspNetUserId = parentId,
                    UserName = user.UserName,
                    Email = user.Email
                })
                .ToPagedList(page, pageSize.Value);
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string parentId, string search, string param)
        {
            var intellisenseCountConfig = new IntellisenseCountConfig();
            var roles = await UserManager.GetRolesAsync(parentId);

            return roles.ToList()
                .Where(r => r.IndexOf(search, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(r => r)
                .Take(intellisenseCountConfig.Value)
                .Select(r => r)
                .ToList();
        }

        protected override EditDetailsDeleteViewModel GenerateEditDetailsDeleteLinks(HtmlHelper htmlHelper, string targetName, string id, ILayoutContentsIndexViewModel<string, AspNetUserRole> vm, string returnUrl, string separator = " | ",  bool allowDetails = false)
        {
            var mvcHtmlStrings = new List<MvcHtmlString>();
            var deleteMvcHtml = GetDeleteMvcHtml(htmlHelper, vm, id, targetName, vm.DeleteActionName, returnUrl, vm.ControllerName);

            mvcHtmlStrings.Add(deleteMvcHtml);

            return new EditDetailsDeleteViewModel(mvcHtmlStrings, separator);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(AspNetUserRole item, string routeAction)
        {
            var uiMessage = new UiMessage();

            return await Task.FromResult(uiMessage);
        }

        public override Task<string> GetParentName(string parentId)
        {
            if (!HasParent(parentId)) return Task.FromResult("");

            var user = UserManager.FindById(parentId);

            return Task.FromResult(user.UserName);
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(AspNetUserRole item, string parentId, eAction action)
        {
            var title1 = string.Empty;
            var title2 = string.Empty;
            var title3 = string.Empty;

            switch (action)
            {
                case eAction.Index:

                    title1 = "Manage Roles";
                    title2 = await GetParentName(parentId);

                    break;

                case eAction.GetCreate:
                case eAction.PostCreate:

                    title1 = "Add";
                    title2 = "Roles";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetEdit:
                case eAction.PostEdit:

                    title1 = action.ToString();
                    title2 = item.RoleName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString();
                    title2 = item.RoleName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.Details:

                    title1 = action.ToString();
                    title2 = item.RoleName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<AspNetUserRole>, IOrderedQueryable<AspNetUserRole>> GetOrderBy(int sortColumn, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public override async Task<AspNetUserRole> FindItemAsync(string id, eAction action)
        {
            var aId = id.Split('|');
            var parentId = aId[0];
            var entityId = aId[1];
            var user = await UserManager.FindByIdAsync(parentId);

            using (var db = new TenderSearchDb())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var item = roleManager.Roles.First(r => r.Name == entityId);
                var result = new AspNetUserRole
                {
                    AspNetUserId = parentId,
                    Id = item.Id,
                    RoleName = item.Name,
                    UserName = user.UserName,
                    Email = user.Email
                };

                return await Task.FromResult(result);
            }
        }

        protected override async Task AddAsync(AspNetUserRole item)
        {
            await UserManager.AddToRoleAsync(item.AspNetUserId, item.RoleName);
        }

        private IEnumerable<SelectListItem> GetRoles(string parentId)
        {
            using (var db = new TenderSearchDb())
            {
                var user = UserManager.FindById(parentId);
                var exceptedItems = UserManager.GetRoles(user.Id);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var results = roleManager.Roles
                    .OrderBy(x => x.Name)
                    .Where(x => !exceptedItems.Contains(x.Name))
                    .ToList();

                return results.ToSelectListItems(r => r.Name, r => r.Name).ToMvcSelectListItem();
            }
        }

        protected override async Task FinalizeDelete(AspNetUserRole itemFromDb, string newDeletionReason, DateTime timeStamp, string returnUrl)
        {
            var result = await UserManager.RemoveFromRoleAsync(itemFromDb.AspNetUserId, itemFromDb.RoleName);
        }

        protected override AspNetUserRoleLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(AspNetUserRole item,
            string title1, string title2, string title3, int pageSize, int labelClassColumnCount = 4,
            string parentId = default)
        {
            var user = UserManager.FindById(parentId);

            item.AspNetUserId = parentId;
            item.Email = user.Email;
            item.UserName = user.UserName;

            var contentsVm =
                new AspNetUserRoleLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize,
                    parentId: parentId)
                {
                    GetRoles = GetRoles,
                };

            return contentsVm;
        }
    }
}


//protected readonly IIdentityHelper<ApplicationUser, IdentityRole, TenderSearchDb> identityHelper;

//[ImportingConstructor]
//public AspNetUserRoleController(IIdentityHelper<ApplicationUser, IdentityRole, TenderSearchDb> identityHelper)
//{
//    this.identityHelper = identityHelper;
//}

//protected override string GetTitle()
//{
//    return "User Roles";
//}

//protected override string GetName(AspNetUserRole Item)
//{
//    return "Manage Roles";
//}

//protected override async Task<object> GetAutoCompleteIntellisenseAsync(string parentId, string term)
//{
//    var userManager = identityHelper.UserManager;
//    var user = await userManager.FindByNameAsync(parentId);
//    var roles = await userManager.GetRolesAsync(user.Id);

//    return roles
//        .Where(r => r.IndexOf(term, 0, StringComparison.OrdinalIgnoreCase) >= 0)
//        .OrderBy(r => r)
//        .Take(INTELLISENSE_SIZE)
//        .Select(r => new { label = r });

//}

//protected override async Task<object> GetPagedListAsync(string parentId, string searchTerm = null, int page = 1)
//{
//    var userManager = identityHelper.UserManager;
//    var user = await userManager.FindByNameAsync(parentId);
//    var roles = await userManager.GetRolesAsync(user.Id);

//    return roles
//        .Where(r => searchTerm == null || r.Contains(searchTerm))
//        .OrderBy(r => r)
//        .Select(r => new AspNetUserRole { UserName = parentId, Role = r, Email = user.Email })
//        .ToPagedList(page, PAGE_SIZE);
//}


//protected override void EditItemCommit(AspNetUserRole item)
//{
//    if (string.IsNullOrEmpty(item.Role))
//    {
//        throw new Exception("You did not select any Role.");
//    }

//    if (string.Equals(item.Role, item.OldRole, StringComparison.CurrentCultureIgnoreCase))
//    {
//        return; //do nothing
//    }

//    var userManager = identityHelper.UserManager;
//    var user = userManager.FindByName(item.UserName);
//    var roles = userManager.GetRoles(user.Id);

//    if (roles != null) userManager.RemoveFromRoles(user.Id, roles.ToArray());
//    var identityResult = userManager.AddToRole(user.Id, item.Role);
//    //if (identityResult.Succeeded) context.SaveChanges();

//}

//protected override void CreateItemCommit(AspNetUserRole item)
//{
//    var userManager = identityHelper.UserManager;
//    var user = userManager.FindByName(item.UserName);

//    userManager.AddToRole(user.Id, item.Role);
//    //context.SaveChanges();
//}

//protected override void DeleteItemCommit(string id, string roleName)
//{
//    var userManager = identityHelper.UserManager;
//    var user = userManager.FindByName(id);

//    userManager.RemoveFromRole(user.Id, roleName);
//    //context.SaveChanges();
//}

//protected override AspNetUserRole GetItem(string id, string roleName)
//{
//    var user = GetUser(id);
//    if (user == null) throw new Exception("User doesn't exist: " + id);

//    return new AspNetUserRole { UserName = id, Role = roleName, OldRole = roleName, Email = user.Email };
//}

//protected override string GetParentLabel(string parentId)
//{
//    var user = GetUser(parentId);
//    ViewBag.ParentSubLabel = user.Email;
//    return parentId;
//}
//protected override string GetParentID(AspNetUserRole item)
//{
//    return item.UserName;
//}
//protected override AspNetUserRole CreateItem(string parentId, string email)
//{

//    return new AspNetUserRole { UserName = parentId, Email = email };
//}

//protected override void SendEmail(AspNetUserRole item)
//{
//    if (!string.IsNullOrEmpty(item.OldRole))
//    {
//        var oldRole = item.OldRole;
//        if (string.Equals(item.Role, oldRole, StringComparison.CurrentCultureIgnoreCase)) return;
//    }

//    var urlLink = $"{Request.Url?.Scheme}://{Request.Url?.Authority}{Url.Content("~")}";
//    Mailer.SendEmail(item, urlLink);
//}

//protected override string GetApplicationName()
//{
//    return MvcApplication.ApplicationName;
//}

//protected override string GetApplicationVersion()
//{
//    return MvcApplication.ApplicationVersion;
//}
