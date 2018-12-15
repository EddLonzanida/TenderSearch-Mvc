using System;
using System.Globalization;
using System.Threading.Tasks;
using TenderSearch.Business.Requests;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TenderSearch.Tests.Integration.RequestEngines
{
    public class WhenCallingEngines : IntegrationTestDbBase
    {
        [Fact]
        public async Task DuplicateContractAsyncEngine_ShouldBeExecuted()
        {
            var renewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false));
            var request = new DuplicateContractAsyncRequest(DuplicateNameAction.Create, 1, 1,  renewalDate, renewalDate);

            var response = await mediator.GetAsync(request);

            response.Contracts.Count.ShouldBe(1);
        }
    }
}