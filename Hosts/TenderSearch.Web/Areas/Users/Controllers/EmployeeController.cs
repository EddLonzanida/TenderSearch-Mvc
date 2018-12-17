using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.Extensions;
using Eml.ControllerBase.Mvc.Infrastructures;
using Eml.ControllerBase.Mvc.ViewModels;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Extensions;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Users.Controllers.BaseClasses;
using TenderSearch.Web.Areas.Users.ViewModels;
using TenderSearch.Web.Infrastructure;
using X.PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeController : PersonControllerBase<Employee, EmployeeLayoutContentsCreateEditViewModel>
    {
        private readonly IDataRepositorySoftDeleteInt<Company> parentRepository;

        [ImportingConstructor]
        public EmployeeController(IMediator mediator, IDataRepositorySoftDeleteInt<Employee> repository,
            ILogger logger, IDataRepositorySoftDeleteInt<Lookup> lookupRepository, IDataRepositorySoftDeleteInt<Company> parentRepository)
            : base(mediator, repository, logger, lookupRepository)
        {
            this.parentRepository = parentRepository;
        }

        protected override async Task<IPagedList<Employee>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Employee, bool>> whereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Employee, bool>> whereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

            return await repository.GetAutoCompleteIntellisenseAsync(whereClause , r => r.DisplayName);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(Employee item, string routeAction)
        {
            var newValue = item.DisplayName;

            Expression<Func<Employee, bool>> whereClause = r => r.DisplayName == newValue;

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

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(Employee item, int parentId, eAction action)
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
                    title2 = $"{item.FirstName} {item.LastName}";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = $"{item.FirstName} {item.LastName}";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.Details:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = $"{item.FirstName} {item.LastName}";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<Employee>, IOrderedQueryable<Employee>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null;

            var eSortColumn = (eEmployee)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eEmployee.EmployeeNumber:

                        orderBy = r => r.OrderByDescending(x => x.EmployeeNumber);
                        break;

                    case eEmployee.AtmNumber:

                        orderBy = r => r.OrderByDescending(x => x.AtmNumber);
                        break;

                    case eEmployee.SssNumber:

                        orderBy = r => r.OrderByDescending(x => x.SssNumber);
                        break;

                    case eEmployee.TinNumber:

                        orderBy = r => r.OrderByDescending(x => x.TinNumber);
                        break;

                    case eEmployee.PhilHealthNumber:

                        orderBy = r => r.OrderByDescending(x => x.PhilHealthNumber);
                        break;

                    case eEmployee.PagIbigNumber:

                        orderBy = r => r.OrderByDescending(x => x.PagIbigNumber);
                        break;

                    case eEmployee.PassportNumber:

                        orderBy = r => r.OrderByDescending(x => x.PassportNumber);
                        break;

                    case eEmployee.DepartmentName:

                        orderBy = r => r.OrderByDescending(x => x.DepartmentName);
                        break;

                    case eEmployee.MembershipDate:

                        orderBy = r => r.OrderByDescending(x => x.MembershipDate);
                        break;

                    case eEmployee.HiredDate:

                        orderBy = r => r.OrderByDescending(x => x.HiredDate);
                        break;

                    case eEmployee.EmploymentEndDate:

                        orderBy = r => r.OrderByDescending(x => x.EmploymentEndDate);
                        break;

                    case eEmployee.FirstName:

                        orderBy = r => r.OrderByDescending(x => x.FirstName);
                        break;

                    case eEmployee.LastName:

                        orderBy = r => r.OrderByDescending(x => x.LastName);
                        break;

                    case eEmployee.MiddleName:

                        orderBy = r => r.OrderByDescending(x => x.MiddleName);
                        break;

                    case eEmployee.DisplayName:

                        orderBy = r => r.OrderByDescending(x => x.DisplayName);
                        break;

                    case eEmployee.BirthDate:

                        orderBy = r => r.OrderByDescending(x => x.BirthDate);
                        break;

                    case eEmployee.Gender:

                        orderBy = r => r.OrderByDescending(x => x.Gender);
                        break;

                    case eEmployee.CivilStatus:

                        orderBy = r => r.OrderByDescending(x => x.CivilStatus);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eEmployee.EmployeeNumber:

                    orderBy = r => r.OrderBy(x => x.EmployeeNumber);
                    break;

                case eEmployee.AtmNumber:

                    orderBy = r => r.OrderBy(x => x.AtmNumber);
                    break;

                case eEmployee.SssNumber:

                    orderBy = r => r.OrderBy(x => x.SssNumber);
                    break;

                case eEmployee.TinNumber:

                    orderBy = r => r.OrderBy(x => x.TinNumber);
                    break;

                case eEmployee.PhilHealthNumber:

                    orderBy = r => r.OrderBy(x => x.PhilHealthNumber);
                    break;

                case eEmployee.PagIbigNumber:

                    orderBy = r => r.OrderBy(x => x.PagIbigNumber);
                    break;

                case eEmployee.PassportNumber:

                    orderBy = r => r.OrderBy(x => x.PassportNumber);
                    break;

                case eEmployee.DepartmentName:

                    orderBy = r => r.OrderBy(x => x.DepartmentName);
                    break;

                case eEmployee.MembershipDate:

                    orderBy = r => r.OrderBy(x => x.MembershipDate);
                    break;

                case eEmployee.HiredDate:

                    orderBy = r => r.OrderBy(x => x.HiredDate);
                    break;

                case eEmployee.EmploymentEndDate:

                    orderBy = r => r.OrderBy(x => x.EmploymentEndDate);
                    break;

                case eEmployee.FirstName:

                    orderBy = r => r.OrderBy(x => x.FirstName);
                    break;

                case eEmployee.LastName:

                    orderBy = r => r.OrderBy(x => x.LastName);
                    break;

                case eEmployee.MiddleName:

                    orderBy = r => r.OrderBy(x => x.MiddleName);
                    break;

                case eEmployee.DisplayName:

                    orderBy = r => r.OrderBy(x => x.DisplayName);
                    break;

                case eEmployee.BirthDate:

                    orderBy = r => r.OrderBy(x => x.BirthDate);
                    break;

                case eEmployee.Gender:

                    orderBy = r => r.OrderBy(x => x.Gender);
                    break;

                case eEmployee.CivilStatus:

                    orderBy = r => r.OrderBy(x => x.CivilStatus);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }

        public override async Task<Employee> CreateNewItemWithParent(int parentId, string param)
        {
            var parent = await parentRepository.GetAsync(parentId);

            return new Employee
            {
                Company = parent,
                CompanyId = parentId
            };
        }

        public override async Task BeforeCreateSave(Employee item)
        {
            if (item != null)
            {
                item.DisplayName = $"{item.LastName}, {item.FirstName} {item.MiddleName}";
            }

            await Task.Delay(1);
        }

        public override async Task BeforeEditSave(Employee item)
        {
            if (item != null)
            {
                item.DisplayName = $"{item.LastName}, {item.FirstName} {item.MiddleName}";
            }

            await Task.Delay(1);
        }

        public override async Task<string> GetParentName(int parentId)
        {
            if (!HasParent(parentId)) return string.Empty;

            var parent = await parentRepository.GetAsync(parentId);

            return parent.Name;
        }

        public override int GetParentId(Employee item)
        {
            return item.CompanyId;
        }

        public IEnumerable<SelectListItem> GetCategoryTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.CategoryType);
        }

        public IEnumerable<SelectListItem> GetDepartments()
        {
            return lookupRepository.GetLookupList(LookupGroups.Department, r => r.Text);
        }

        public IEnumerable<SelectListItem> GetRankTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.RankType);
        }

        public IEnumerable<SelectListItem> GetStatusTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.EmployeeStatusType);
        }

        protected override LayoutContentsIndexViewModel<int, Employee> GetLayoutContentsViewModelForIndex(IPagedList<Employee> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            var contentsVm = base.GetLayoutContentsViewModelForIndex(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param);

            contentsVm.AllowAjaxCreate = false;
            contentsVm.AllowAjaxEdit = false;

            return contentsVm;
        }

        protected override EmployeeLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(Employee item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var contentsVm = new EmployeeLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId)
            {
                GetStatusTypes = GetStatusTypes,
                GetRankTypes = GetRankTypes,
                GetGenders = GetGenders,
                GetMaritalStatus = GetMaritalStatus,
                GetCategoryTypes = GetCategoryTypes,
                GetDepartments = GetDepartments
            };

            return contentsVm;
        }

        protected override EditDetailsDeleteViewModel GenerateEditDetailsDeleteLinks(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Employee> vm, string returnUrl, string separator = " | ", bool allowDetails = false)
        {
            var editDetailsDeleteLinks = base.GenerateEditDetailsDeleteLinks(htmlHelper, targetName, id, vm, returnUrl, separator, true);
            var addressMvcHtml = GetAddressMvcHtml(htmlHelper, targetName, id, vm, returnUrl, separator);
            var dependentsMvcHtml = GetDependentsMvcHtml(htmlHelper, targetName, id, vm, returnUrl, separator);

            editDetailsDeleteLinks.MvcHtmlStrings.Add(addressMvcHtml);
            editDetailsDeleteLinks.MvcHtmlStrings.Add(dependentsMvcHtml);

            return editDetailsDeleteLinks;
        }

        protected MvcHtmlString GetAddressMvcHtml(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Employee> vm, string returnUrl, string separator = " | ")
        {
            var mvcHtml = htmlHelper.ActionLink("Address", ActionNames.IndexWithParent,
                new
                {
                    controller = ControllerNames.Address,
                    parentId = id,
                    returnUrl = Request?.Url?.AbsoluteUri,
                    param = eAddressOwnerType.Employee
                });

            return mvcHtml;
        }

        protected MvcHtmlString GetDependentsMvcHtml(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Employee> vm, string returnUrl, string separator = " | ")
        {
            var mvcHtml = htmlHelper.ActionLink("Dependents", ActionNames.IndexWithParent,
                new
                {
                    controller = ControllerNames.Dependent,
                    parentId = id,
                    returnUrl = Request?.Url?.AbsoluteUri
                });

            return mvcHtml;
        }

        protected override Task<IEnumerable<PersonSuggestion>> DoGetPersonSuggestionsAsync(string search, string param)
        {
            throw new NotImplementedException();
        }
    }
}
