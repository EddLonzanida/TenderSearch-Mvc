using TenderSearch.Data;
using TenderSearch.DataMigration.Seeders;
using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration
{
    public sealed class MigrationConfiguration : DbMigrationsConfiguration<TenderSearchDb>
    {
        ///// <summary>
        ///// Set this to false before the project's first production deployment
        ///// </summary>
        //private const bool USE_PRODUCTION_MIGRATION = false; 

        public MigrationConfiguration()
        {
            var isEnabled = false; //Disable if running in Release Mode
#if DEBUG
            isEnabled = true;
#endif
            AutomaticMigrationsEnabled = isEnabled;
            AutomaticMigrationDataLossAllowed = isEnabled;

            MigrationsDirectory = "SqlServerMigrations";
            MigrationsNamespace = "TenderSearch.DataMigration.SqlServerMigrations";
        }

        protected override void Seed(TenderSearchDb context)
        {
            //// This approach will be useful only during the early phase of development 
            //// This will reset all values (which is not applicable after go live.)
            RegionSeeder.Seed(context);
            ProvinceSeeder.Seed(context);
            CityMunicipalitySeeder.Seed(context);
            BarangaySeeder.Seed(context);
            LookupSeeder.Seed(context);
            CompanySeeder.Seed(context);
            ContractSeeder.Seed(context);
            EmployeeSeeder.Seed(context);
            AddressSeeder.Seed(context);
            DependentSeeder.Seed(context);
        }
    }
}

//Add-Migration InitialCreate
//Update-Database -verbose

//Add-Migration SeedAspNetRoles
//Update-Database -verbose

//Add-Migration SeedAspNetUsers
//Update-Database -verbose

//Add-Migration SeedAspNetUserRoles
//Update-Database -verbose

//Add-Migration SeedLookup
//Update-Database -verbose

//Add-Migration SeedContracts
//Update-Database -verbose

//Add-Migration Company
//Update-Database -verbose

//Add-Migration Contract
//Update-Database -verbose

//Add-Migration Employee
//Update-Database -verbose