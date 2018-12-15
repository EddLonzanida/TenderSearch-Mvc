using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using TenderSearch.Business.Common.Entities;
using Eml.DataRepository.Contracts;
using Eml.Mediator.Contracts;
using NSubstitute;

namespace TenderSearch.Tests.Unit.BaseClasses
{
    public abstract class EngineTestBase<T1, T2> : IDisposable
        where T1 : IRequestAsync<T1, T2> where T2 : IResponse
    {
        private List<Contract> contractList;

        protected IDataRepositorySoftDeleteInt<Contract> contractRepository;

        protected IRequestAsyncEngine<T1, T2> engine;


        protected EngineTestBase()
        {
            contractList = new List<Contract>
            {
                new Contract
                {
                    Id = 1,
                    CompanyId = 1,
                    DateSigned = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false)),
                    RenewalDate = DateTime.Parse("18/01/2017", new CultureInfo("en-AU", false)),
                    EndDate = DateTime.Parse("18/01/2018", new CultureInfo("en-AU", false)),
                    Price = 100,
                    ContractType = "Master contact"
                },
                new Contract
                {
                    Id = 2,
                    CompanyId = 1,
                    DateSigned = DateTime.Parse("19/01/2018", new CultureInfo("en-AU", false)),
                    RenewalDate = DateTime.Parse("19/01/2018", new CultureInfo("en-AU", false)),
                    EndDate = DateTime.Parse("19/01/2019", new CultureInfo("en-AU", false)),
                    Price = 100,
                    ContractType = "Standard contact"
                }
            };
            contractRepository = Substitute.For<IDataRepositorySoftDeleteInt<Contract>>();
            contractRepository.GetAsync(Arg.Any<Expression<Func<Contract, bool>>>()).Returns(contractList);
        }

        public void Dispose()
        {
            engine?.Dispose();
            contractRepository?.Dispose();
        }
    }
}
