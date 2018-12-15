using System;
using TenderSearch.Business.Responses;
using Eml.Mediator.Contracts;

namespace TenderSearch.Business.Requests
{
    public class DuplicateContractAsyncRequest : IRequestAsync<DuplicateContractAsyncRequest, DuplicateContractResponse>
    {
        public string ActionName { get; }

        public int CompanyId { get; }

        public int ContractId { get; }

        public DateTime? RenewalDate { get; }

        public DateTime? EndDate { get; }

        public DuplicateContractAsyncRequest(string actionName, int companyId, int contractId, DateTime? renewalDate, DateTime? endDate)
        {
            ActionName = actionName;
            CompanyId = companyId;
            ContractId = contractId;
            RenewalDate = renewalDate;
            EndDate = endDate;
        }
    }
}