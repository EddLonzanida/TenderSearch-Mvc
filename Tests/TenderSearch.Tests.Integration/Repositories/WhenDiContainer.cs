using TenderSearch.Business.Common.Entities;
using TenderSearch.Tests.Integration.BaseClasses;
using Eml.DataRepository.Contracts;
using Shouldly;
using Xunit;

namespace TenderSearch.Tests.Integration.Repositories
{
    public class WhenDiContainer : IntegrationTestDbBase
    {
        [Fact]
        public void CompanyRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IDataRepositorySoftDeleteInt<Company>>();

            sut.ShouldNotBeNull();
        }

        [Fact]
        public void EmployeeRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IDataRepositorySoftDeleteInt<Employee>>();

            sut.ShouldNotBeNull();
        }

        [Fact]
        public void ContractRepository_ShouldBeDiscoverable()
        {
            var sut = classFactory.GetExport<IDataRepositorySoftDeleteInt<Contract>>();

            sut.ShouldNotBeNull();
        }
    }
}