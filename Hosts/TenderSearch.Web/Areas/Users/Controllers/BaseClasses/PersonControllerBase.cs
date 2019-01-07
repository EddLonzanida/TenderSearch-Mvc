using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TenderSearch.Business.Common.Dto;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Web.Infrastructure;
using TenderSearch.Web.Infrastructure.Contracts;
using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.Extensions;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using TenderSearch.Data;
using TenderSearch.Data.Contracts;
using TenderSearch.Web.Controllers.BaseClasses;

namespace TenderSearch.Web.Areas.Users.Controllers.BaseClasses
{
    public abstract class PersonControllerBase<T, TParent, TLayoutContentsCreateEditViewModel> 
        : CrudControllerForCreateEditWithParent<T, TParent, TLayoutContentsCreateEditViewModel>, IPersonSuggestion
        where T : class, IEntityBase<int>, new()
        where TParent : class, IEntityBase<int>, IEntitySoftdeletableBase, new()
        where TLayoutContentsCreateEditViewModel : class, ILayoutContentsCreateEditViewModel<int, T>
    {
        protected readonly IDataRepositorySoftDeleteInt<Lookup> lookupRepository;

        protected PersonControllerBase(IMediator mediator
            , IDataRepositoryBase<int, T, TenderSearchDb> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger
            , IDataRepositorySoftDeleteInt<Lookup> lookupRepository)
            : base(mediator, repository, parentRepository, logger)
        {
            this.lookupRepository = lookupRepository;
        }

        public IEnumerable<SelectListItem> GetGenders()
        {
            return lookupRepository.GetLookupList(LookupGroups.Gender, r => r.Text.Substring(0, 1));
        }

        public IEnumerable<SelectListItem> GetMaritalStatus()
        {
            return lookupRepository.GetLookupList(LookupGroups.MaritalStatus, r => r.Text);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route(ActionNames.PersonSuggestions)]
        public async Task<ActionResult> PersonSuggestions(string search = "", string param = null)
        {
            const string METHOD_NAME = ActionNames.PersonSuggestions;

            return await ExecuteSuggestionsFacade(METHOD_NAME, () => DoGetPersonSuggestionsAsync(search, param));
        }

        protected abstract Task<IEnumerable<PersonSuggestion>> DoGetPersonSuggestionsAsync(string search, string param);
    }
}