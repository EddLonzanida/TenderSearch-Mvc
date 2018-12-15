using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Business.RequestEngines;
using TenderSearch.Business.Requests;
using TenderSearch.Business.Responses;
using TenderSearch.Tests.Unit.BaseClasses;
using Eml.DataRepository;
using NSubstitute;
using Shouldly;
using Xunit;

namespace TenderSearch.Tests.Unit.RequestEngines
{
    public class DuplicateContractAsyncEngineTests : EngineTestBase<DuplicateContractAsyncRequest, DuplicateContractResponse>
    {
        public DuplicateContractAsyncEngineTests()
        {
            var bjmpConnectionString = new MainDbConnectionString();
            engine = new DuplicateContractAsyncEngine(contractRepository, bjmpConnectionString);
        }

        [Fact]
        public async Task Engine_ShouldGetDuplicatesWhenEditing()
        {
            const string actionName = "Edit";
            const int companyId = 1;
            const int contractId = 1;
            var renewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false));
            var endDate = DateTime.Parse("18/01/2018", new CultureInfo("en-AU", false));
            var request = new DuplicateContractAsyncRequest(actionName, companyId, contractId, renewalDate, endDate);

            var result = await engine.GetAsync(request);

            await contractRepository.Received().GetAsync(Arg.Any<Expression<Func<Contract, bool>>>());
            var items = result.Contracts.ToList();
            items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Engine_ShouldGetDuplicatesWhenCreating()
        {
            const string actionName = "Create";
            const int companyId = 1;
            const int contractId = -1;
            var renewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false));
            var endDate = DateTime.Parse("19/01/2019", new CultureInfo("en-AU", false));
            var request = new DuplicateContractAsyncRequest(actionName, companyId, contractId, renewalDate, endDate);

            var result = await engine.GetAsync(request);

            await contractRepository.Received().GetAsync(Arg.Any<Expression<Func<Contract, bool>>>());
            var items = result.Contracts.ToList();
            items.Count.ShouldBe(2);
        }
    }
}