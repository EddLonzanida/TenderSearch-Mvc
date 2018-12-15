using TenderSearch.Tests.Integration.BaseClasses;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.Extensions;
using Shouldly;
using Xunit;

namespace TenderSearch.Tests.Integration
{
    public class WhenDiContainer : IntegrationTestDiBase
    {
        [Fact]
        public void Migrator_ShouldBeDiscoverable()
        {
            var dbMigration = classFactory.GetMigrator(Environments.PRODUCTION);

            dbMigration.ShouldNotBeNull();
        }
    }
}
