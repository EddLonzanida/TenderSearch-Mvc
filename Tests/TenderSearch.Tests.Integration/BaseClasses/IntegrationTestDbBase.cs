using Eml.ClassFactory.Contracts;
using Eml.Mediator.Contracts;
using Xunit;

namespace TenderSearch.Tests.Integration.BaseClasses
{
    [Collection(IntegrationTestDbFixture.COLLECTION_DEFINITION)]
    public abstract class IntegrationTestDbBase
    {
        protected readonly IClassFactory classFactory;

        protected readonly IMediator mediator;

        protected IntegrationTestDbBase()
        {
            classFactory = IntegrationTestDbFixture.ClassFactory;

            mediator = classFactory.GetExport<IMediator>();
        }
    }
}
