﻿using TenderSearch.Business.Common.Entities;
using TenderSearch.Tests.Integration.BaseClasses;
using Shouldly;
using TenderSearch.Data.Contracts;
using Xunit;

namespace TenderSearch.Tests.Integration.Repositories
{
    public class WhenExecutingRepositories : IntegrationTestDbBase
    {
        [Fact]
        public void CompanyRepository_ShouldRetrieveData()
        {
            var repository = classFactory.GetExport<IDataRepositorySoftDeleteInt<Company>>();

            var items = repository.GetAll();

            items.Count.ShouldBe(20);
        }

        [Fact]
        public void EmployeeRepository_ShouldRetrieveData()
        {
            var repository = classFactory.GetExport<IDataRepositorySoftDeleteInt<Employee>>();

            var items = repository.GetAll();

            items.Count.ShouldBe(19);
        }

        [Fact]
        public void ContractRepository_ShouldRetrieveData()
        {
            var repository = classFactory.GetExport<IDataRepositorySoftDeleteInt<Contract>>();

            var items = repository.GetAll();

            items.Count.ShouldBe(2);
        }
    }
}