using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Users.ViewModels;
using TenderSearch.Web.Infrastructure;
using TenderSearch.Web.Infrastructure.Contracts;
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
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using TenderSearch.Web.Controllers.BaseClasses;
using X.PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddressController : CrudControllerForCreateEdit<Address, AddressLayoutContentsCreateEditViewModel>, IPersonSuggestion, IBarangaySuggestion
    {
        protected readonly IDataRepositorySoftDeleteInt<Employee> employeeRepository;
        protected readonly IDataRepositorySoftDeleteInt<Dependent> dependentRepository;
        protected readonly IDataRepositorySoftDeleteInt<Barangay> barangayRepository;
        protected readonly IDataRepositorySoftDeleteInt<Lookup> lookupRepository;

        [ImportingConstructor]
        public AddressController(IMediator mediator , IDataRepositorySoftDeleteInt<Address> repository
            , ILogger logger , IDataRepositorySoftDeleteInt<Employee> employeeRepository , IDataRepositorySoftDeleteInt<Dependent> dependentRepository
            , IDataRepositorySoftDeleteInt<Barangay> barangayRepository , IDataRepositorySoftDeleteInt<Lookup> lookupRepository)
            : base(mediator, repository, logger)
        {
            this.employeeRepository = employeeRepository;
            this.dependentRepository = dependentRepository;
            this.barangayRepository = barangayRepository;
            this.lookupRepository = lookupRepository;
        }

        protected override async Task<IPagedList<Address>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Address, bool>> whereClause = r => search == "" || r.StreetAddress.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.OwnerId == parentId);

                if (param != null)
                {
                    whereClause = whereClause.And(r => r.OwnerType == param);
                }
            }

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, whereClause, orderBy);

            var previousOwnerId = 0;
            var canFetch = false;
            var displayName = "";

            await result.ForEachAsync(async r =>
            {
                canFetch = previousOwnerId != r.OwnerId;

                var ownerType2 = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), r.OwnerType);

                switch (ownerType2)
                {
                    case eAddressOwnerType.Employee:
                        if (canFetch)
                        {
                            var ownerId = r.OwnerId ?? 0;
                            var employee = await employeeRepository.GetAsync(ownerId);

                            displayName = employee?.DisplayName;
                        }

                        r.OwnerDisplayName = displayName;

                        break;

                    case eAddressOwnerType.Dependent:
                        if (canFetch)
                        {
                            var ownerId = r.OwnerId ?? 0;
                            var dependent = await dependentRepository.GetAsync(ownerId);

                            displayName = dependent?.DisplayName;
                        }

                        r.OwnerDisplayName = displayName;
                        break;

                    case eAddressOwnerType.Education:

                        r.OwnerDisplayName = "Education Not yet implemented";
                        break;

                    case eAddressOwnerType.Office:

                        r.OwnerDisplayName = "Office Not yet implemented";
                        break;

                    case eAddressOwnerType.Business:

                        r.OwnerDisplayName = "Business Not yet implemented";
                        break;

                    default:
                        //TODO merge with others: Dependent, Education, Office
                        break;
                }
            });

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Address, bool>> whereClause = r => search == ""
                                                               || r.StreetAddress.ToLower().Contains(search)
                                                               || r.AddressType.ToLower().Contains(search)
                                                               || r.OwnerType.ToLower().Contains(search)
                                                               || r.Barangay.Name.ToLower().Contains(search);
            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.OwnerId == parentId);
            }

            if (!string.IsNullOrWhiteSpace(param))
            {
                whereClause = whereClause.And(r => r.OwnerType == param);
            }

            return await repository.GetAutoCompleteIntellisenseAsync(r => r.Include(s => s.Barangay)
                , whereClause
                , r => r.StreetAddress);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(Address item, string routeAction)
        {
            var newOwnerId = item.OwnerId;
            var newStreetAddress = item.StreetAddress;
            var newOwnerType = item.OwnerType;
            var newBarangayId = item.BarangayId;
            var newAddressType = item.AddressType;

            Expression<Func<Address, bool>> whereClause = r => r.OwnerId == newOwnerId
                                                               && r.StreetAddress == newStreetAddress
                                                               && r.OwnerType == newOwnerType
                                                               && r.BarangayId == newBarangayId
                                                               && r.AddressType == newAddressType;

            if (routeAction != DuplicateNameAction.Create)
            {
                var itemId = item.Id;

                whereClause = whereClause.And(r => r.Id != itemId);
            }

            var hasDuplicates = await repository.HasDuplicatesAsync(whereClause);

            if (!hasDuplicates) return await Task.FromResult(new UiMessage());

            var cDuplicateMsg = $"Name: <strong>{item.StreetAddress}</strong>";

            return await Task.FromResult(new UiMessage(new[] { cDuplicateMsg }));
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(Address item, int parentId, eAction action)
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
                    title2 = $"{item.AddressType} Address";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = $"{item.AddressType} Address";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.Details:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = $"{item.AddressType} Address";

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<Address>, IOrderedQueryable<Address>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<Address>, IOrderedQueryable < Address>> orderBy = null;

            var eSortColumn = (eAddress)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eAddress.AddressType:

                        orderBy = r => r.OrderByDescending(x => x.AddressType);
                        break;

                    case eAddress.StreetAddress:

                        orderBy = r => r.OrderByDescending(x => x.StreetAddress);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eAddress.AddressType:

                    orderBy = r => r.OrderBy(x => x.AddressType);
                    break;

                case eAddress.StreetAddress:

                    orderBy = r => r.OrderBy(x => x.StreetAddress);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }

        protected override bool GetAllowKnockoutJs(string param)
        {
            return true;
        }

        protected override LayoutContentsIndexViewModel<int, Address> GetLayoutContentsViewModelForIndex(IPagedList<Address> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            var contentsVm = base.GetLayoutContentsViewModelForIndex(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param);

            if (!string.IsNullOrWhiteSpace(param))
            {
                contentsVm.HasParent = true;
            }

            return contentsVm;
        }

        private PersonSuggestion GetInitialPersonSuggestion(Address item)
        {
            var ownerId = item?.OwnerId;

            var id = ownerId ?? default;

            if (id == default)
            {
                var tmpOwner1 = new Employee();
                var result1 = tmpOwner1.CreateSuggestion();

                return result1;
            }

            var ownerType = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), item.OwnerType);

            switch (ownerType)
            {
                case eAddressOwnerType.Employee:

                    return employeeRepository.Get(id).CreateSuggestion();

                case eAddressOwnerType.Dependent:

                    return dependentRepository.Get(id).CreateSuggestion();

                case eAddressOwnerType.Education:

                    throw new NotImplementedException();

                case eAddressOwnerType.Office:

                    throw new NotImplementedException();

                case eAddressOwnerType.Business:

                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Tip: override GenerateEditDetailsDeleteLinks and supply custom values for the 'param' parameter
        /// </summary>
        protected override AddressLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(Address item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var initialPersonSuggestion = GetInitialPersonSuggestion(item);
            var initialSuggestion = item?.Barangay?.CreateSuggestion();
            var action = GetActionName();

            if (initialSuggestion == null)
            {
                var tmpItem = new Barangay();

                initialSuggestion = tmpItem.CreateSuggestion();
            }

            var contentsVm = new AddressLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize, action, parentId: parentId)
            {
                GetAddressTypes = GetAddressTypes,
                GetOwnerTypes = GetOwnerTypes,
                InitialBarangaySuggestion = initialSuggestion,
                InitialStreetAddress = item?.StreetAddress,
                InitialPersonSuggestion = initialPersonSuggestion
            };

            if (!string.IsNullOrWhiteSpace(param))
            {
                contentsVm.HasParent = true;
            }

            return contentsVm;
        }

        private IEnumerable<SelectListItem> GetAddressTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.AddressType, r => r.Text);
        }

        private IEnumerable<SelectListItem> GetOwnerTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.AddressOwnerType, r => r.Text);
        }

        public override async Task<Address> CreateNewItemWithParent(int parentId, string param)
        {
            var ownerType = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), param);
            var ownerDisplayName = "";

            switch (ownerType)
            {
                case eAddressOwnerType.Employee:

                    var employee = await employeeRepository.GetAsync(parentId);

                    ownerDisplayName = employee.DisplayName;

                    break;
                case eAddressOwnerType.Dependent:

                    var dependent = await dependentRepository.GetAsync(parentId);

                    ownerDisplayName = dependent.DisplayName;
                    break;
                case eAddressOwnerType.Education:

                    ownerDisplayName = $"CreateNewItemWithParent {ownerType.ToString()} not yet implemented";
                    break;
                case eAddressOwnerType.Office:

                    ownerDisplayName = $"CreateNewItemWithParent {ownerType.ToString()} not yet implemented";
                    break;
                case eAddressOwnerType.Business:

                    ownerDisplayName = $"CreateNewItemWithParent {ownerType.ToString()} not yet implemented";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var item = new Address
            {
                OwnerDisplayName = ownerDisplayName,
                OwnerId = parentId,
                OwnerType = param
            };

            return item;
        }

        public override async Task BeforeCreateSave(Address item)
        {
            barangayRepository.SetUnchanged(item?.Barangay);

            if (item != null)
            {
                item.Barangay = null;

                if (item.OwnerId == 0)
                {
                    throw new EntityException("Please select a valid Owner.");
                }

                if (item.BarangayId == 0)
                {
                    throw new EntityException("Please select a valid Barangay.");
                }
            }

            await Task.Delay(1);
        }

        public override async Task BeforeEditSave(Address item)
        {
            barangayRepository.SetUnchanged(item?.Barangay);

            if (item != null)
            {
                item.Barangay = null;
            }

            await Task.Delay(1);
        }

        public override async Task<string> GetParentName(int parentId)
        {
            if (!HasParent(parentId)) return string.Empty;

            var param = GetParam();
            var ownerType = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), param);

            switch (ownerType)
            {
                case eAddressOwnerType.Employee:

                    var employee = await employeeRepository.GetAsync(parentId);

                    return employee.DisplayName;

                case eAddressOwnerType.Dependent:

                    var dependent = await dependentRepository.GetAsync(parentId);

                    return dependent.DisplayName;

                case eAddressOwnerType.Education:
                    throw new NotImplementedException();
                case eAddressOwnerType.Office:
                    throw new NotImplementedException();
                case eAddressOwnerType.Business:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override int GetParentId(Address item)
        {
            var ownerId = item.OwnerId ?? 0;

            return ownerId;
        }

        public override async Task<Address> FindItemAsync(int id, eAction action)
        {
            var item = await base.FindItemAsync(id, action);
            var param = item.OwnerType;

            if (action == eAction.GetEdit)
            {
                var ownerId = item.OwnerId ?? 0;
                var ownerType = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), param);

                switch (ownerType)
                {
                    case eAddressOwnerType.Employee:

                        var employee = await employeeRepository.GetAsync(ownerId);

                        item.OwnerDisplayName = employee.DisplayName;

                        break;
                    case eAddressOwnerType.Dependent:

                        var dependent = await dependentRepository.GetAsync(ownerId);

                        item.OwnerDisplayName = dependent.DisplayName;

                        break;
                    case eAddressOwnerType.Education:
                        throw new NotImplementedException();
                    case eAddressOwnerType.Office:
                        throw new NotImplementedException();
                    case eAddressOwnerType.Business:
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return item;
        }

        [HttpGet]
        [Route(ActionNames.PersonSuggestions)]
        public async Task<ActionResult> PersonSuggestions(string search = "", string param = null)
        {
            const string METHOD_NAME = ActionNames.PersonSuggestions;

            return await ExecuteSuggestionsFacade(METHOD_NAME, () => DoGetPersonSuggestionsAsync(search, param));
        }

        [HttpGet]
        [Route(ActionNames.BarangaySuggestions)]
        public async Task<ActionResult> BarangaySuggestions(string search = "")
        {
            const string METHOD_NAME = ActionNames.BarangaySuggestions;

            return await ExecuteSuggestionsFacade(METHOD_NAME, () => DoGetBarangaySuggestionsAsync(search));
        }

        protected async Task<IEnumerable<PersonSuggestion>> DoGetPersonSuggestionsAsync(string search, string param)
        {
            search = search.Trim().ToLower();

            //var param = GetParam();

            var ownerType = (eAddressOwnerType)Enum.Parse(typeof(eAddressOwnerType), param);

            switch (ownerType)
            {
                case eAddressOwnerType.Employee:

                    Expression<Func<Employee, bool>> employeeWhereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

                    var employees = await employeeRepository.GetAsync(employeeWhereClause, r => r.OrderBy(e => e.DisplayName), 1);

                    return employees.Select(b => b.CreateSuggestion());

                case eAddressOwnerType.Dependent:

                    Expression<Func<Dependent, bool>> dependentWhereClause = r => search == "" || r.DisplayName.ToLower().Contains(search);

                    var dependents = await dependentRepository.GetAsync(dependentWhereClause, r => r.OrderBy(e => e.DisplayName), 1);

                    return dependents.Select(b => b.CreateSuggestion());

                case eAddressOwnerType.Education:

                    throw new NotImplementedException();

                case eAddressOwnerType.Office:

                    throw new NotImplementedException();

                case eAddressOwnerType.Business:

                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private async Task<IEnumerable<BarangaySuggestion>> DoGetBarangaySuggestionsAsync(string search)
        {
            search = search.Trim().ToLower();

            Expression<Func<Barangay, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            IQueryable<Barangay> EagerLoad(IQueryable<Barangay> r) => r.Include(a => a.CityMunicipality.Province.Region);

            var results = await barangayRepository.GetAsync(EagerLoad, whereClause, r => r.OrderBy(s => s.Name), 1);
            var suggestions = results.Select(b => b.CreateSuggestion());

            return suggestions;
        }
    }
}
