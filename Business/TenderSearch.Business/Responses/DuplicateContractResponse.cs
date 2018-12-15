using System.Collections.Generic;
using System.Linq;
using TenderSearch.Business.Common.Entities;
using Eml.Mediator.Contracts;

namespace TenderSearch.Business.Responses
{
    public class DuplicateContractResponse : IResponse
    {
        public List<Contract> Contracts { get; }

        public DuplicateContractResponse(IEnumerable<Contract> contracts)
        {
            Contracts = contracts.ToList();
        }
    }
}
