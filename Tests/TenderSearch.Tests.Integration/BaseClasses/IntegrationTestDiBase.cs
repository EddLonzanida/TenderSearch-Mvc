using Eml.ClassFactory.Contracts;
using Xunit;

namespace TenderSearch.Tests.Integration.BaseClasses
{
    [Collection(IntegrationTestDiFixture.COLLECTION_DEFINITION)]
    public abstract class IntegrationTestDiBase
    {
        protected readonly IClassFactory classFactory;

        protected IntegrationTestDiBase()
        {
            classFactory = IntegrationTestDiFixture.ClassFactory;
        }
    }
}
