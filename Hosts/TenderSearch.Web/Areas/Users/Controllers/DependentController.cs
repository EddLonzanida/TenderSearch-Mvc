using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.Extensions;
using Eml.ControllerBase.Mvc.Infrastructures;
using Eml.ControllerBase.Mvc.ViewModels;
using Eml.DataRepository.Contracts;
using Eml.Extensions;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Users.Controllers.BaseClasses;
using TenderSearch.Web.Areas.Users.ViewModels;
using TenderSearch.Web.Infrastructure;
using X.PagedList;
using eDependent = TenderSearch.Business.Common.Entities.eDependent;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DependentController : PersonControllerBase<Dependent, DependentLayoutContentsCreateEditViewModel>
    {
        private readonly IDataRepositorySoftDeleteInt<Employee> parentRepository;

        [ImportingConstructor]
        public DependentController(IMediator mediator, IDataRepositorySoftDeleteInt<Dependent> repository, ILogger logger, IDataRepositorySoftDeleteInt<Employee> parentRepository, IDataRepositorySoftDeleteInt<Lookup> lookupRepository)
            : base(mediator, repository, logger, lookupRepository)
        {
            this.parentRepository = parentRepository;
        }

        protected override async Task<IPagedList<Dependent>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Dependent, bool>> whereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.EmployeeId == parentId);
            }

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, r => r.Include(s => s.Employee), whereClause, orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Dependent, bool>> whereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.EmployeeId.Equals(parentId));
            }

            return await repository.GetAutoCompleteIntellisenseAsync(whereClause, r => r.DisplayName);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(Dependent item, string routeAction)
        {
            var newDisplayName = item.DisplayName;
            var newEmployeeId = item.EmployeeId;

            Expression<Func<Dependent, bool>> whereClause = r => r.DisplayName == newDisplayName && r.EmployeeId == newEmployeeId;

            if (routeAction != DuplicateNameAction.Create)
            {
                var itemId = item.Id;

                whereClause = whereClause.And(r => r.Id != itemId);
            }

            var hasDuplicates = await repository.HasDuplicatesAsync(whereClause);

            if (!hasDuplicates) return await Task.FromResult(new UiMessage());

            var cDuplicateMsg = $"Name: <strong>{item.DisplayName}</strong>";

            return await Task.FromResult(new UiMessage(new[] { cDuplicateMsg }));
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(Dependent item, int parentId, eAction action)
        {
            var title1 = string.Empty;
            var title2 = string.Empty;
            var title3 = string.Empty;

            switch (action)
            {
                case eAction.Index:

                    title1 = $"Setup {GetTypeName().ToSpaceDelimitedWords().Pluralize()}";
                    title2 = await GetParentName(parentId);

                    break;

                case eAction.GetCreate:
                case eAction.PostCreate:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = GetTypeName().ToSpaceDelimitedWords();

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetEdit:
                case eAction.PostEdit:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.DisplayName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.DisplayName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.Details:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.DisplayName;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<Dependent>, IOrderedQueryable<Dependent>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<Dependent>, IOrderedQueryable<Dependent>> orderBy = null;

            var eSortColumn = (eDependent)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eDependent.DisplayName:

                        orderBy = r => r.OrderByDescending(x => x.DisplayName);
                        break;
                    case eDependent.FirstName:

                        orderBy = r => r.OrderByDescending(x => x.FirstName);
                        break;
                    case eDependent.LastName:

                        orderBy = r => r.OrderByDescending(x => x.LastName);
                        break;
                    case eDependent.MiddleName:

                        orderBy = r => r.OrderByDescending(x => x.MiddleName);
                        break;
                    case eDependent.BirthDate:

                        orderBy = r => r.OrderByDescending(x => x.BirthDate);
                        break;
                    case eDependent.Gender:

                        orderBy = r => r.OrderByDescending(x => x.Gender);
                        break;
                    case eDependent.Relationship:

                        orderBy = r => r.OrderByDescending(x => x.Relationship);
                        break;
                    case eDependent.CivilStatus:

                        orderBy = r => r.OrderBy(x => x.CivilStatus);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eDependent.DisplayName:

                    orderBy = r => r.OrderBy(x => x.DisplayName);
                    break;
                case eDependent.FirstName:

                    orderBy = r => r.OrderBy(x => x.FirstName);
                    break;
                case eDependent.LastName:

                    orderBy = r => r.OrderBy(x => x.LastName);
                    break;
                case eDependent.MiddleName:

                    orderBy = r => r.OrderBy(x => x.MiddleName);
                    break;
                case eDependent.BirthDate:

                    orderBy = r => r.OrderBy(x => x.BirthDate);
                    break;
                case eDependent.Gender:

                    orderBy = r => r.OrderBy(x => x.Gender);
                    break;
                case eDependent.Relationship:

                    orderBy = r => r.OrderBy(x => x.Relationship);
                    break;
                case eDependent.CivilStatus:

                    orderBy = r => r.OrderBy(x => x.CivilStatus);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }

        public override async Task<Dependent> CreateNewItemWithParent(int parentId, string param)
        {
            var parent = await parentRepository.GetAsync(parentId);

            var item = new Dependent
            {
                EmployeeId = parentId,
                Employee = parent
            };

            return await Task.FromResult(item);
        }

        public override async Task BeforeCreateSave(Dependent item)
        {
            parentRepository.SetUnchanged(item?.Employee);

            if (item != null)
            {
                item.DisplayName = $"{item.LastName}, {item.FirstName} {item.MiddleName}";
                item.Employee = null;
            }

            await Task.Delay(1);
        }

        public override async Task BeforeEditSave(Dependent item)
        {
            parentRepository.SetUnchanged(item?.Employee);

            if (item != null)
            {
                item.DisplayName = $"{item.LastName}, {item.FirstName} {item.MiddleName}";
                item.Employee = null;
            }

            await Task.Delay(1);
        }

        public override async Task<string> GetParentName(int parentId)
        {
            if (!HasParent(parentId)) return string.Empty;

            var parent = await parentRepository.GetAsync(parentId);

            return parent.DisplayName;
        }

        public override int GetParentId(Dependent item)
        {
            return item.EmployeeId ?? 0;
        }

        public override async Task<Dependent> FindItemAsync(int id, eAction action)
        {
            var item = await repository.GetAsync(r => r.Include(s => s.Employee), r => r.Id == id);

            return item.ToList().FirstOrDefault();
        }

        protected override bool GetAllowKnockoutJs(string param)
        {
            return true;
        }

        protected override EditDetailsDeleteViewModel GenerateEditDetailsDeleteLinks(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Dependent> vm, string returnUrl, string separator = " | ", bool allowDetails = false)
        {
            var editDetailsDeleteLinks = base.GenerateEditDetailsDeleteLinks(htmlHelper, targetName, id, vm, returnUrl, separator, allowDetails);
            var addressMvcHtml = GetAddressMvcHtml(htmlHelper, targetName, id, vm, returnUrl, separator);

            editDetailsDeleteLinks.MvcHtmlStrings.Add(addressMvcHtml);

            return editDetailsDeleteLinks;
        }

        protected MvcHtmlString GetAddressMvcHtml(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Dependent> vm, string returnUrl, string separator = " | ")
        {
            var mvcHtml = htmlHelper.ActionLink("Address", ActionNames.IndexWithParent,
                new
                {
                    controller = ControllerNames.Address,
                    parentId = id,
                    returnUrl = Request?.Url?.AbsoluteUri,
                    param = eAddressOwnerType.Dependent
                });

            return mvcHtml;
        }

        protected override LayoutContentsIndexViewModel<int, Dependent> GetLayoutContentsViewModelForIndex(IPagedList<Dependent> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            var parameter = HasParent(parentId).ToString();
            var contentsVm = base.GetLayoutContentsViewModelForIndex(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, parameter);

            return contentsVm;
        }

        /// <summary>
        /// Tip: override GenerateEditDetailsDeleteLinks and supply custom values for the 'param' parameter
        /// </summary>
        protected override DependentLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(Dependent item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var action = GetActionName();
            var initialPersonSuggestion = GetInitialPersonSuggestion(item);
            var contentsVm = new DependentLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize, action, parentId: parentId)
            {
                GetGenders = GetGenders,
                GetMaritalStatus = GetMaritalStatus,
                GetDependentTypes = GetDependentTypes,
                InitialPersonSuggestion = initialPersonSuggestion
            };

            return contentsVm;
        }

        private PersonSuggestion GetInitialPersonSuggestion(Dependent item)
        {
            var ownerId = item?.EmployeeId;

            var id = ownerId ?? default;

            if (id == default)
            {
                var tmpOwner1 = new Employee();
                var result1 = tmpOwner1.CreateSuggestion();

                return result1;
            }

            var tmpOwner2 = parentRepository.Get(id);
            var result2 = tmpOwner2.CreateSuggestion();

            return result2;
        }

        public IEnumerable<SelectListItem> GetDependentTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.Dependent, r => r.Text);
        }

        protected override async Task<IEnumerable<PersonSuggestion>> DoGetPersonSuggestionsAsync(string search, string param)
        {
            Expression<Func<Employee, bool>> employeeWhereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

            var employees = await parentRepository.GetAsync(employeeWhereClause, r => r.OrderBy(e => e.DisplayName), 1);

            return employees.Select(b => b.CreateSuggestion());
        }
    }
}
