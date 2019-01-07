using Eml.ControllerBase.Mvc.Contracts;
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
using System.Web.Mvc.Html;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data;
using TenderSearch.Data.Security;
using TenderSearch.Web.Areas.UserManagers.Controllers.BaseClasses;
using X.PagedList;

namespace TenderSearch.Web.Areas.UserManagers.Controllers
{
    [RouteArea(MvcArea.UserManagers)]
    [Authorize(Roles = Authorize.UserManagers)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AspNetUserController : UserManagersControllerBase<AspNetUser>
    {
        [ImportingConstructor]
        public AspNetUserController(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }

        protected override async Task<IPagedList<AspNetUser>> GetItemsAsync(string parentId, int page, bool desc, int sortColumn, string search, string param)
        {
            var pageSize = new PageSizeConfig();
            var items = await UserManager.Users
                .OrderBy(r => r.UserName)
                .Where(r => search == null || r.UserName.Contains(search))
                .ToListAsync();
            var tmpItems = items
                .Select(r => new AspNetUser
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    Email = r.Email
                })
                .ToList();

            await tmpItems.ForEachAsync(async tmpItem =>
            {
                var roles = await GetRolesForUserAsync(tmpItem.UserName);

                roles.ToList().ForEach(r => tmpItem.AspNetUserRoles.Add(new AspNetUserRole
                {
                    Id = r,
                    RoleName = r,
                    AspNetUserId = tmpItem.Id,
                    UserName = tmpItem.UserName,
                    Email = tmpItem.Email
                }));
            });

            tmpItems.ForEach(tmpItem =>
            {
                tmpItem.AspNetUserRoles.OrderBy(r => r.RoleName);
            });

            return tmpItems
                .OrderBy(r => r.HasRole)
                .ThenBy(r => r.UserName)
                .ToPagedList(page, pageSize.Value);
        }

        protected override async Task<List<string>> GetSuggestionsAsync(string parentId, string search, string param)
        {
            var intellisenseSizeConfig = new IntellisenseCountConfig();
            var models = await UserManager.Users
                .OrderBy(r => r.UserName)
                .Where(r => r.UserName.Contains(search))
                .Take(intellisenseSizeConfig.Value)
                .ToListAsync();

            var result = models.Select(r => r.UserName).ToList();

            return result;
        }

        protected override async Task<UiMessage> IsDuplicateAsync(AspNetUser item, string routeAction)
        {
            var uiMessage = new UiMessage();

            return await Task.FromResult(uiMessage);
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(AspNetUser item, string parentId, eAction action)
        {
            var title1 = string.Empty;
            var title2 = string.Empty;
            var title3 = string.Empty;

            switch (action)
            {
                case eAction.Index:

                    title1 = "Manage Users";

                    break;

                case eAction.GetCreate:
                case eAction.PostCreate:

                    title1 = action.ToString();
                    title2 = GetTypeName().ToSpaceDelimitedWords();

                    break;
                case eAction.GetEdit:
                case eAction.PostEdit:

                    title1 = action.ToString();
                    title2 = item.UserName;

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString();
                    title2 = item.UserName;
                    break;

                case eAction.Details:

                    title1 = action.ToString();
                    title2 = item.UserName;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>> GetOrderBy(int sortColumn, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public override async Task<AspNetUser> FindItemAsync(TenderSearchDb db, string id, eAction action)
        {
            var user = await UserManager.FindByIdAsync(id);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles;

            var result = new AspNetUser
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                AspNetUserRoles = user.Roles.ToList()
                    .ConvertAll(r => new AspNetUserRole
                    {
                        Id = r.RoleId,
                        AspNetUserId = user.Id,
                        RoleName = roles.First(x => x.Id == r.RoleId).Name
                    })
            };

            return result;
        }

        protected override async Task AddAsync(TenderSearchDb db, AspNetUser item)
        {
            await Task.Delay(1);
        }

        protected override async Task FinalizeDelete(TenderSearchDb db, AspNetUser itemFromDb, string newDeletionReason, DateTime timeStamp, string returnUrl)
        {
            await Task.Delay(1);
        }

        protected override EditDetailsDeleteViewModel GenerateEditDetailsDeleteLinks(HtmlHelper htmlHelper, string targetName, string id, ILayoutContentsIndexViewModel<string, AspNetUser> vm, string returnUrl, string separator = " | ", bool allowDetails = false)
        {
            var mvcHtmlStrings = new List<MvcHtmlString>();
            var editRoleMvcHtml = GetAspNetUserRoleMvcHtml(htmlHelper, id, targetName, returnUrl, vm.ControllerName, caption: "Edit Role");

            mvcHtmlStrings.Add(editRoleMvcHtml);

            return new EditDetailsDeleteViewModel(mvcHtmlStrings, separator);
        }

        protected MvcHtmlString GetAspNetUserRoleMvcHtml(HtmlHelper htmlHelper, string entityId, string targetName, string returnUrl, string controllerName = null, string caption = "Edit")
        {
            var mvcHtml = htmlHelper.ActionLink(caption, ActionNames.IndexWithParent,
                new
                {
                    parentId = entityId,
                    controller = ControllerNames.AspNetUserRole,
                    returnUrl = Request?.Url?.AbsoluteUri
                });

            return mvcHtml;
        }

        //protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search = "")
        //{
        //    var models = await UserManager.Users
        //        .OrderBy(r => r.UserName)
        //        .Where(r => r.UserName.Contains(search))
        //        .Take(repository.IntellisenseSize)
        //        .ToListAsync();

        //    var result = models.Select(r => r.UserName).ToList();

        //    return result;
        //}

        //protected override async Task<UiMessage> IsDuplicateAsync(AspNetUser item, string routeAction)
        //{
        //    var uiMessage = new UiMessage();

        //    return await Task.FromResult(uiMessage);
        //}

        //protected override async Task FinalizeEdit(AspNetUser item)
        //{
        //    await Task.Delay(1);
        //}

        //protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(AspNetUser item, int parentId, eAction action)
        //{
        //    return await Task.FromResult(("Title1", "Title2", "Title3"));
        //}
    }
}

//[RouteArea(MvcArea.UserManagers)]
//[Export]
//[PartCreationPolicy(CreationPolicy.NonShared)]
//[Authorize(Roles = Authorize.UserManagers)]
//public class AspNetUserController : UserManagerBase<AspNetUser>
//{
//    protected readonly IIdentityHelper<ApplicationUser, IdentityRole, TenderSearchDb> identityHelper;

//    [ImportingConstructor]
//    public AspNetUserController(IIdentityHelper<ApplicationUser, IdentityRole, TenderSearchDb> identityHelper)
//    {
//        this.identityHelper = identityHelper;
//    }

//    protected override string GetTitle()
//    {
//        return "User Registration";
//    }
//    protected override string GetName(AspNetUser item)
//    {
//        return item.UserName;
//    }

//    protected override async Task<object> GetAutoCompleteIntellisenseAsync(string parentId, string term)
//    {
//        var userManager = identityHelper.UserManager;
//        var models = await userManager.Users
//            .OrderBy(r => r.UserName)
//            .Where(r => r.UserName.Contains(term))
//            .Take(INTELLISENSE_SIZE)
//            .ToListAsync();

//        return models.Select(r => new { label = r.UserName });

//    }

//    protected override async Task<object> GetPagedListAsync(string parentId, string searchTerm = null, int page = 1)
//    {
//        var userManager = identityHelper.UserManager;
//        var items = await userManager.Users
//            .OrderBy(r => r.UserName)
//            .Where(r => searchTerm == null || r.UserName.Contains(searchTerm))
//            .ToListAsync();
//        var tmpItems = items
//            .Select(r => new AspNetUser { UserName = r.UserName, Email = r.Email })
//            .ToList();

//        foreach (var i in tmpItems)
//        {
//            var user = await userManager.FindByNameAsync(i.UserName);
//            var roles = await userManager.GetRolesAsync(user.Id);
//            roles.ToList().ForEach(r => i.Roles.Add(r));

//        }

//        return tmpItems
//            .OrderBy(r => r.HasRole)
//            .ThenBy(r => r.UserName)
//            .ToPagedList(page, PAGE_SIZE);
//    }

//    protected override void EditItemCommit(AspNetUser item)
//    {
//        throw new NotImplementedException();

//    }

//    protected override void CreateItemCommit(AspNetUser item)
//    {

//        throw new NotImplementedException();
//    }

//    protected override void DeleteItemCommit(string id, string roleName)
//    {
//        var userManager = identityHelper.UserManager;
//        var user = userManager.FindByName(id);

//        userManager.Delete(user);
//    }

//    protected override AspNetUser GetItem(string id, string roleName)
//    {
//        if (string.IsNullOrWhiteSpace(id)) return null;

//        var userManager = identityHelper.UserManager;
//        var user = userManager.FindByName(id);

//        return user != null ? new AspNetUser { UserName = user.UserName } : null;
//    }
//}