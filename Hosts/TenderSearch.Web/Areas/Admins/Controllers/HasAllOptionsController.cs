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
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Areas.Admins.ViewModels;
using TenderSearch.Web.Controllers.BaseClasses;
using X.PagedList;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace TenderSearch.Web.Areas.Admins.Controllers
{
    [RouteArea(MvcArea.Admins)]
    [Authorize(Roles = Authorize.Admins)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HasAllOptionsController : CrudControllerForCreateEditIndexWithParent<HasAllOptions, HasAllOptionsLayoutContentsCreateEditViewModel, HasAllOptionsLayoutContentsIndexViewModel>
    {
        private readonly IDataRepositorySoftDeleteInt<Employee> parentRepository;

        [ImportingConstructor]
        public HasAllOptionsController(IMediator mediator, IDataRepositorySoftDeleteInt<HasAllOptions> repository, ILogger logger, IDataRepositorySoftDeleteInt<Employee> parentRepository)
            : base(mediator, repository, logger)
        {
            this.parentRepository = parentRepository;
        }

        protected override async Task<IPagedList<HasAllOptions>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<HasAllOptions, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.ParentId == parentId);
            }

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<HasAllOptions, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            if (parentId != default)
            {
                whereClause = whereClause.And(r => r.ParentId.Equals(parentId));
            }

            return await repository.GetAutoCompleteIntellisenseAsync(whereClause, r => r.Name);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(HasAllOptions item, string routeAction)
        {
            var newValue = item.Name;

            Expression<Func<HasAllOptions, bool>> whereClause = r => !string.IsNullOrWhiteSpace(r.Name) && r.Name == newValue;

            if (routeAction != DuplicateNameAction.Create)
            {
                var itemId = item.Id;

                whereClause = whereClause.And(r => r.Id != itemId);
            }

            if (item.ParentId != default)
            {
                var itemParentId = item.ParentId;

                whereClause = whereClause.And(r => r.ParentId != itemParentId);
            }

            var hasDuplicates = await repository.HasDuplicatesAsync(whereClause);

            if (!hasDuplicates) return await Task.FromResult(new UiMessage());

            var cDuplicateMsg = $"Name: <strong>{item.Name}</strong>";

            return await Task.FromResult(new UiMessage(new[] { cDuplicateMsg }));
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(HasAllOptions item, int parentId, eAction action)
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
                    title2 = item.Name;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.Name;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                case eAction.Details:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.Name;

                    if (!Request.IsAjaxRequest()) title3 = await GetParentName(parentId);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<HasAllOptions>, IOrderedQueryable<HasAllOptions>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<HasAllOptions>, IOrderedQueryable<HasAllOptions>> orderBy = null;

            var eSortColumn = (eHasAllOptions)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eHasAllOptions.Name:

                        orderBy = r => r.OrderByDescending(x => x.Name);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eHasAllOptions.Name:

                    orderBy = r => r.OrderBy(x => x.Name);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }

        public override async Task<HasAllOptions> CreateNewItemWithParent(int parentId, string param)
        {
            var parent = await parentRepository.GetAsync(parentId);
            var item = new HasAllOptions
            {
                //     OwnerDisplayName = parent.DisplayName,
                //     OwnerId = parentId,
                Employee = parent
            };

            return item;
        }

        public override async Task BeforeCreateSave(HasAllOptions item)
        {
            parentRepository.SetUnchanged(item?.Employee);

            if (item != null)
            {
                item.Employee = null;
            }

            await Task.Delay(1);
        }

        public override async Task BeforeEditSave(HasAllOptions item)
        {
            parentRepository.SetUnchanged(item?.Employee);

            if (item != null)
            {
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

        public override int GetParentId(HasAllOptions item)
        {
            return item.ParentId;
        }

        public override async Task<HasAllOptions> FindItemAsync(int id, eAction action)
        {
            var item = await repository.GetAsync(r => r.Include(s => s.Employee), r => r.Id == id);

            return item.ToList().FirstOrDefault();
        }

        protected override HasAllOptionsLayoutContentsIndexViewModel GetLayoutContentsViewModelForIndex(IPagedList<HasAllOptions> pagedList, string title1, string title2, string title3, string search, string targetTableBody, int page, int parentId, string param)
        {
            var contentsVm = new HasAllOptionsLayoutContentsIndexViewModel(pagedList, title1, title2, title3, search, targetTableBody, page, parentId, param)
            {
                GetExpirationCountDown = GetExpirationCountDown
            };

            return contentsVm;
        }

        public int? GetExpirationCountDown(HasAllOptions item)
        {
            return null;
        }
        /// <summary>
        /// Tip: override GenerateEditDetailsDeleteLinks and supply custom values for the 'param' parameter
        /// </summary>
        protected override HasAllOptionsLayoutContentsCreateEditViewModel GetLayoutContentsViewModelForCreateEdit(HasAllOptions item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var contentsVm = new HasAllOptionsLayoutContentsCreateEditViewModel(item, title1, title2, title3, pageSize, labelClassColumnCount, parentId, param)
            {
                GetCustomDropDown = GetCustomDropDown
            };

            return contentsVm;
        }

        private IEnumerable<SelectListItem> GetCustomDropDown()
        {
            var items = new List<string> { "CustomDropDown1", "CustomDropDown2" };

            var selectLists = items.ConvertAll(r => new Eml.Extensions.SelectListItem { Value = r, Text = r });

            return selectLists.ToMvcSelectListItem();
        }
    }
}
