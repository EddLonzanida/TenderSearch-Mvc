using TenderSearch.Data;
using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration
{
    public sealed class MigrationConfiguration : DbMigrationsConfiguration<TenderSearchDb>
    {
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
            ////// This approach will be useful only during the early phase of development 
            ////// This will reset all values (which is not applicable after go live.)
            //RegionSeeder.Seed(context);
            //ProvinceSeeder.Seed(context);
            //CityMunicipalitySeeder.Seed(context);
            //BarangaySeeder.Seed(context);
            //LookupSeeder.Seed(context);
            //CompanySeeder.Seed(context);
            //ContractSeeder.Seed(context);
            //EmployeeSeeder.Seed(context);
            //AddressSeeder.Seed(context);
            //DependentSeeder.Seed(context);
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

//Add-Migration SeedLookups
//Update-Database -verbose

//Add-Migration SeedRegions
//Update-Database -verbose

//Add-Migration SeedProvinces
//Update-Database -verbose

//Add-Migration SeedCityMunicipalities
//Update-Database -verbose

//Add-Migration SeedBarangays
//Update-Database -verbose

//Add-Migration SeedCompanies
//Update-Database -verbose

//Add-Migration SeedEmployees
//Update-Database -verbose

//Add-Migration SeedContracts
//Update-Database -verbose

//Add-Migration SeedDependents
//Update-Database -verbose
