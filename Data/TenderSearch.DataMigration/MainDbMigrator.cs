using System.ComponentModel.Composition;
using TenderSearch.Data;
using Eml.ConfigParser;
using Eml.DataRepository;
using Eml.DataRepository.Attributes;
using Eml.DataRepository.BaseClasses;

namespace TenderSearch.DataMigration
{
    [DbMigratorExport(Environments.PRODUCTION)]
    public class MainDbMigrator : MigratorBase<TenderSearchDb, MigrationConfiguration>
    {
        [ImportingConstructor]
        public MainDbMigrator(IConfigBase<string, MainDbConnectionString> mainDbConnectionString)
            : base(mainDbConnectionString.Value)
        {
        }
    }
}
