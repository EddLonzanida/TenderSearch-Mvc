using TenderSearch.Tests.Integration.BaseClasses;
using Shouldly;
using Xunit;

namespace TenderSearch.Tests.Integration
{
    public class WhenIdentityMigrations : IntegrationTestDbBase
    {
        [Fact]
        private void Migration_ShouldExecute()
        {
            classFactory.ShouldNotBeNull();
        }
    }
}
