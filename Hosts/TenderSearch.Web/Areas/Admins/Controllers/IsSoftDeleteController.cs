﻿using Eml.ControllerBase.Mvc.Infrastructures;
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
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Controllers.BaseClasses;
using X.PagedList;

namespace TenderSearch.Web.Areas.Admins.Controllers
{
    [RouteArea(MvcArea.Admins)]
    [Authorize(Roles = Authorize.Admins)]
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IsSoftDeleteController : CrudController<IsSoftDelete>
    {
        [ImportingConstructor]
        public IsSoftDeleteController(IMediator mediator, IDataRepositorySoftDeleteInt<IsSoftDelete> repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }

        protected override async Task<IPagedList<IsSoftDelete>> GetItemsAsync(int parentId, int page, bool isDesc, int sortColumn, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<IsSoftDelete, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            var orderBy = GetOrderBy(sortColumn, isDesc);
            var result = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return result;
        }

        protected override async Task<List<string>> GetSuggestionsAsync(int parentId, string search, string param)
        {
            search = search.Trim().ToLower();

            Expression<Func<IsSoftDelete, bool>> whereClause = r => search == "" || r.Name.ToLower().Contains(search);

            return await repository.GetAutoCompleteIntellisenseAsync(whereClause, r => r.Name);
        }

        protected override async Task<UiMessage> IsDuplicateAsync(IsSoftDelete item, string routeAction)
        {
            var newValue = item.Name;

            Expression<Func<IsSoftDelete, bool>> whereClause = r => !string.IsNullOrWhiteSpace(r.Name) && r.Name == newValue;

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

        protected override async Task<(string Title1, string Title2, string Title3)> GetTitle123Async(IsSoftDelete item, int parentId, eAction action)
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

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = GetTypeName().ToSpaceDelimitedWords();

                    break;

                case eAction.GetEdit:
                case eAction.PostEdit:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.Name;

                    break;

                case eAction.GetDelete:
                case eAction.PostDelete:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.Name;

                    break;

                case eAction.Details:

                    title1 = action.ToString().Replace("Get", string.Empty).Replace("Post", string.Empty);
                    title2 = item.Name;

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            var result = (title1, title2, title3);

            return await Task.FromResult(result);
        }

        protected override Func<IQueryable<IsSoftDelete>, IOrderedQueryable<IsSoftDelete>> GetOrderBy(int sortColumn, bool isDesc)
        {
            Func<IQueryable<IsSoftDelete>, IOrderedQueryable<IsSoftDelete>> orderBy = null;

            var eSortColumn = (eIsSoftDelete)sortColumn;

            if (isDesc)
            {
                switch (eSortColumn)
                {
                    case eIsSoftDelete.Name:

                        orderBy = r => r.OrderByDescending(x => x.Name);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return orderBy;
            }

            switch (eSortColumn)
            {
                case eIsSoftDelete.Name:

                    orderBy = r => r.OrderBy(x => x.Name);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orderBy;
        }


    }
}
