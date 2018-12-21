using Eml.ConfigParser;
using Eml.ControllerBase.Mvc.BaseClasses;
using Eml.ControllerBase.Mvc.Contracts;
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
using Eml.ControllerBase.Mvc.Extensions;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Business.Requests;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Users.Controllers.BaseClasses;
using TenderSearch.Web.Areas.Users.ViewModels;
using TenderSearch.Web.Configurations;
using TenderSearch.Web.Controllers.BaseClasses;
using TenderSearch.Web.Infrastructure;
using X.PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ContractController 
        : CrudControllerForCreateEditIndexWithParent<Contract, Company, ContractLayoutContentsCreateEditViewModel, ContractLayoutContentsIndexViewModel>
    {
        private readonly int contractExpiresSoonCountDown;

        private readonly IDataRepositorySoftDeleteInt<Lookup> lookupRepository;

        [ImportingConstructor]
        public ContractController(IMediator mediator, IDataRepositorySoftDeleteInt<Contract> repository, ILogger logger
            , IDataRepositorySoftDeleteInt<Company> parentRepository
            , IDataRepositorySoftDeleteInt<Lookup> lookupRepository
            , IConfigBase<int, ContractExpiresSoonCountDownConfig> contractExpiresSoonCountDownConfig)
            : base(mediator, repository, parentRepository, logger)
        {
            contractExpiresSoonCountDown = contractExpiresSoonCountDownConfig.Value;

            this.lookupRepository = lookupRepository;
        }

        protected override async Task<IPagedList<Contract>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Contract, bool>> whereClause = r => search == "" || r.ContractType.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.CompanyId.Equals(parentId));
            }

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page
                , r => r.Include(s => s.Company)
                , whereClause
                , orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Contract, bool>> whereClause = r => search == "" || r.ContractType.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.CompanyId.Equals(parentId));
            }

            return await repository.GetAutoCompleteIntellisenseAsync(r => r.Include(s => s.Company), whereClause , r => r.ContractType);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(Contract item, string routeAction)
        {
            const string htmlTagWithNoPairToReplace = "<hr>";

            var lineBreak = Environment.NewLine;
            var request = new DuplicateContractAsyncRequest(routeAction, item.CompanyId, item.Id, item.RenewalDate, item.EndDate);
            var messages = new List<string>();
            var response = await mediator.GetAsync(request);
            var models = response.Contracts;

            models.ForEach(r =>
            {
                messages.Add($"ContractType: <strong>{r.ContractType}</strong>" +
                             $"{lineBreak}RenewalDate: <strong>{r.RenewalDate.ToStringOrDefault("d")}</strong>" +
                             $"{lineBreak}EndDate: <strong>{r.EndDate.ToStringOrDefault("d")}</strong>");
            });

            var htmlTagsWithNoPairToReplace = UiMessage.GetHtmlTagsToReplace(new[] { htmlTagWithNoPairToReplace });
            var uiMessage = new UiMessage(messages, htmlTagsWithNoPairToReplace);

            return await Task.FromResult(uiMessage);
        }

        public override async Task<Contract> CreateNewItemWithParent(int parentId, string param)
        {
            var parent = await parentRepository.GetAsync(parentId);
            var item = new Contract
            {
                CompanyId = parentId,
                Company = parent
            };

            return item;
        }

        public override async Task BeforeCreateSave(Contract item)
        {
            parentRepository.SetUnchanged(item?.Company);

            if (item != null)
            {
                item.Company = null;
            }

            await Task.Delay(1);
        }

        public override async Task BeforeEditSave(Contract item)
        {
            parentRepository.SetUnchanged(item?.Company);

            await Task.Delay(1);
        }

        public override async Task<string> GetParentName(int parentId)
        {
            if (!HasParent(parentId)) return string.Empty;

            var parent = await parentRepository.GetAsync(parentId);

            return parent.Name;
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(Contract item, int parentId, eAction action)
        {
            var title1 = string.Empty;
            var title2 = string.Empty;
            var title3 = string.Empty;

            switch (action)
            {
                case eAction.Index:

                    title1 = $"Setup {GetTypeName().ToSpaceDelimitedWords().Pluralize()}";

                    break;

                case eAction.GetCreate:
                case eAction.PostCreate:

                    title1 = action.ToString();
                    title2 = GetTypeName().ToSpaceDelimitedWords();

                    break;

                case eAction.GetEdit:
                case eAction.PostEdit:

                    title1 = action.ToString();
                    title2 = item.ContractType;

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString();
                    title2 = item.ContractType;

                    break;

                case eAction.Details:

                    title1 = action.ToString();
                    title2 = item.ContractType;

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        public override int GetParentId(Contract item)
        {
            return item.CompanyId;
        }

        protected override Func<IQueryable<Contract>, IOrderedQueryable<Contract>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<Contract>, IOrderedQueryable<Contract>> orderBy = null;

            var eSortColumn = (eContract)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eContract.EndDate:

                        orderBy = r => r.OrderByDescending(x => x.EndDate).ThenBy(s => s.ContractType);
                        break;

                    case eContract.ContractType:

                        orderBy = r => r.OrderByDescending(x => x.ContractType).ThenBy(s => s.ContractType);
                        break;

                    case eContract.DateSigned:

                        orderBy = r => r.OrderByDescending(x => x.DateSigned).ThenBy(s => s.ContractType);
                        break;

                    case eContract.RenewalDate:

                        orderBy = r => r.OrderByDescending(x => x.RenewalDate).ThenBy(s => s.ContractType);
                        break;

                    case eContract.Price:

                        orderBy = r => r.OrderByDescending(x => x.Price).ThenBy(s => s.ContractType);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eContract.EndDate:

                    orderBy = r => r.OrderBy(x => x.EndDate).ThenBy(s => s.ContractType);
                    break;

                case eContract.ContractType:

                    orderBy = r => r.OrderBy(x => x.ContractType).ThenBy(s => s.ContractType);
                    break;

                case eContract.DateSigned:

                    orderBy = r => r.OrderBy(x => x.DateSigned).ThenBy(s => s.ContractType);
                    break;

                case eContract.RenewalDate:

                    orderBy = r => r.OrderBy(x => x.RenewalDate).ThenBy(s => s.ContractType);
                    break;

                case eContract.Price:

                    orderBy = r => r.OrderBy(x => x.Price).ThenBy(s => s.ContractType);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }

        protected override ContractLayoutContentsIndexViewModel GetLayoutContentsViewModelForIndex(IPagedList<Contract> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            return new ContractLayoutContentsIndexViewModel(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param)
            {
                GetExpirationCountDown = GetExpirationCountDown,
                //AllowAjaxCreate = false,
                //AllowAjaxEdit = false
            };
        }

        protected override bool GetAllowKnockoutJs(string param)
        {
            return true;
        }
        //protected override async Task<ILayoutViewModelBase> GetLayoutViewModelForIndex(IPagedList<Contract> pagedList, int parentId, string search, string title1, string title2, string title3, string targetTableBody, int page, bool isChildren, string param)
        //{
        //    //var vm = await GetLayoutViewModelBase<LayoutIndexViewModelBase<int, Contract, ContractLayoutContentsIndexViewModel>>(title1);

        //    //vm.ContentViewModel = new ContractLayoutContentsIndexViewModel(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param)
        //    //{
        //    //    GetExpirationCountDown = GetExpirationCountDown,
        //    //    AllowAjaxCreate = false,
        //    //    AllowAjaxEdit = false
        //    //};

        //    //return vm;

        //    var vm = await GetLayoutViewModelBase<LayoutIndexViewModelBase<int, Contract, ContractLayoutContentsIndexViewModel>>(title1);
        //    var contentsVm = new ContractLayoutContentsIndexViewModel(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param)
        //    {
        //        GetExpirationCountDown = GetExpirationCountDown,
        //        AllowAjaxCreate = false,
        //        AllowAjaxEdit = false
        //    };

        //    if (isChildren)
        //    {
        //        contentsVm.IndexActionName = ActionNames.IndexWithParent;
        //        contentsVm.SuggestionsActionName = ActionNames.IndexWithParent;
        //        contentsVm.CreateActionName = ActionNames.CreateWithParent;
        //    }

        //    vm.AllowKnockoutJs = GetAllowKnockoutJs(param);
        //    vm.ContentViewModel = contentsVm;
        //    vm.GenerateEditDetailsDeleteLinks = (htmlHelper, targetName, id, vm1, returnUrl, separator) => GenerateEditDetailsDeleteLinks(htmlHelper, targetName, id, vm1, returnUrl, separator);

        //    return vm;
        //}

        protected override ContractLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(Contract item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var contentsVm = new ContractLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize, parentId: parentId)
            {
                GetCompanies = GetCompanies,
                GetContractTypes = GetContractTypes,
            };

            return contentsVm;
        }

        public int? GetExpirationCountDown(Contract item)
        {
            if (!item.EndDate.HasValue) return null;

            var dateDiff = (int?)(item.EndDate.Value - DateTime.Today).TotalDays;

            return dateDiff > contractExpiresSoonCountDown ? null : dateDiff;
        }

        public IEnumerable<SelectListItem> GetDependentTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.Dependent, r => r.Text);
        }

        protected IEnumerable<SelectListItem> GetCompanies()
        {
            var results = parentRepository.GetAll();
            var selectLists = results.ToSelectListItems(r => r.Id, r => r.Name);

            return selectLists.ToMvcSelectListItem();
        }

        protected IEnumerable<SelectListItem> GetContractTypes()
        {
            return lookupRepository.GetLookupList(LookupGroups.ContractType, r => r.Text);
        }
    }
}
