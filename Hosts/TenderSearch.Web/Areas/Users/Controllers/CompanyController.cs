using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TenderSearch.Web.Controllers.BaseClasses;
using X.PagedList;

namespace TenderSearch.Web.Areas.Users.Controllers
{
    [RouteArea(MvcArea.Users)]
    [Authorize(Roles = Authorize.Users)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyController : CrudController<Company>
    {
        [ImportingConstructor]
        public CompanyController(IMediator mediator, IDataRepositorySoftDeleteInt<Company> repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }

        protected override async Task<IPagedList<Company>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Company, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<Company, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            return await repository.GetAutoCompleteIntellisenseAsync(whereClause , r => r.Name);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(Company item, string routeAction)
        {
            var newValue = item.Name;

            Expression<Func<Company, bool>> whereClause = r => !string.IsNullOrEmpty(r.Name) && r.Name.Equals(newValue);

            if (routeAction != DuplicateNameAction.Create)
            {
                var itemId = item.Id;

                whereClause = whereClause.And(r => r.Id != itemId);
            }

            var hasDuplicates = await repository.HasDuplicatesAsync(whereClause);

            if (!hasDuplicates) return await Task.FromResult(new UiMessage());

            var cDuplicateMsg = $"Name: <strong>{item.Name}</strong>";

            return await Task.FromResult(new UiMessage(new[] { cDuplicateMsg }));
        }

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(Company item, int parentId, eAction action)
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
                    title2 = item.Name;

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString();
                    title2 = item.Name;

                    break;

                case eAction.Details:

                    title1 = action.ToString();
                    title2 = item.Name;

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override EditDetailsDeleteViewModel GenerateEditDetailsDeleteLinks(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Company> vm, string returnUrl, string separator = " | ", bool allowDetails = false)
        {
            var editDetailsDeleteLinks = base.GenerateEditDetailsDeleteLinks(htmlHelper, targetName, id, vm, returnUrl, separator, allowDetails);
            var contractsMvcHtml = GetContractsMvcHtml(htmlHelper, targetName, id, vm, returnUrl, separator);
            var employeesMvcHtml = GetEmployeesMvcHtml(htmlHelper, targetName, id, vm, returnUrl, separator);

            editDetailsDeleteLinks.MvcHtmlStrings.Add(contractsMvcHtml);
            editDetailsDeleteLinks.MvcHtmlStrings.Add(employeesMvcHtml);

            return editDetailsDeleteLinks;
        }

        protected MvcHtmlString GetContractsMvcHtml(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Company> vm, string url, string separator = " | ")
        {
            var mvcHtml = htmlHelper.ActionLink("Contracts", ActionNames.IndexWithParent,
                new
                {
                    parentId = id,
                    controller = ControllerNames.Contract,
                    returnUrl = Request?.Url?.AbsoluteUri
                });

            return mvcHtml;
        }

        protected MvcHtmlString GetEmployeesMvcHtml(HtmlHelper htmlHelper, string targetName, int id, ILayoutContentsIndexViewModel<int, Company> vm, string url, string separator = " | ")
        {
            var mvcHtml = htmlHelper.ActionLink("Employees", ActionNames.IndexWithParent,
                new
                {
                    controller = ControllerNames.Employee,
                    parentId = id,
                    returnUrl = Request?.Url?.AbsoluteUri
                });

            return mvcHtml;
        }

        protected override Func<IQueryable<Company>, IOrderedQueryable<Company>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null;

            var eSortColumn = (eCompany)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eCompany.Name:

                        orderBy = r => r.OrderByDescending(x => x.Name);
                        break;

                    case eCompany.Description:

                        orderBy = r => r.OrderByDescending(x => x.Description);
                        break;

                    case eCompany.Website:

                        orderBy = r => r.OrderByDescending(x => x.Website);
                        break;

                    case eCompany.AbnCan:

                        orderBy = r => r.OrderByDescending(x => x.AbnCan);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eCompany.Name:

                    orderBy = r => r.OrderBy(x => x.Name);
                    break;

                case eCompany.Description:

                    orderBy = r => r.OrderBy(x => x.Description);
                    break;

                case eCompany.Website:

                    orderBy = r => r.OrderBy(x => x.Website);
                    break;

                case eCompany.AbnCan:

                    orderBy = r => r.OrderBy(x => x.AbnCan);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }
    }
}
