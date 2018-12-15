using System;
using System.Data.Entity.Migrations;
using System.Globalization;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;
using TenderSearch.Data;
using TenderSearch.Business.Common.Entities;

namespace TenderSearch.DataMigration.Seeders
{
    public static class DependentSeeder
    {
        public static void Seed<T>(T context)
            where T : TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Dependent).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Dependents.AddOrUpdate(r => r.Id,
                    new Dependent { Id = 1, EmployeeId = 1, FirstName = "SonFirstName", LastName = "SonLastName", MiddleName = "N", DisplayName = "SonLastName, SonFirstName N", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Son" },
                    new Dependent { Id = 2, EmployeeId = 1, FirstName = "DaugtherFirsName", LastName = "DaugtherLastName", MiddleName = "G", DisplayName = "DaugtherLastName, DaugtherFirsName G", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Daugther" },
                    new Dependent { Id = 3, EmployeeId = 1, FirstName = "WifeFirstName", LastName = "WifeFirstName", MiddleName = "G", DisplayName = "WifeFirstName, WifeFirstName G", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Married", Relationship = "Wife" },
                    new Dependent { Id = 4, EmployeeId = 4, FirstName = "SonFirstName", LastName = "SonLastName", MiddleName = "M", DisplayName = "SonLastName, SonFirstName M", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Son" },
                    new Dependent { Id = 5, EmployeeId = 4, FirstName = "DaugtherFirsName", LastName = "DaugtherLastName", MiddleName = "D", DisplayName = "DaugtherLastName, DaugtherFirsName D", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Daugther" },
                    new Dependent { Id = 6, EmployeeId = 4, FirstName = "WifeFirstName", LastName = "WifeFirstName", MiddleName = "P", DisplayName = "WifeFirstName, WifeFirstName P", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Annulled", Relationship = "Wife" },
                    new Dependent { Id = 7, EmployeeId = 8, FirstName = "SonFirstName", LastName = "SonLastName", MiddleName = "P", DisplayName = "SonLastName, SonFirstName P", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Son" },
                    new Dependent { Id = 8, EmployeeId = 8, FirstName = "DaugtherFirsName", LastName = "DaugtherLastName", MiddleName = "D", DisplayName = "DaugtherLastName, DaugtherFirsName D", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Married", Relationship = "Daugther" },
                    new Dependent { Id = 9, EmployeeId = 8, FirstName = "WifeFirstName", LastName = "WifeFirstName", MiddleName = "M", DisplayName = "WifeFirstName, WifeFirstName M", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Wife" },
                    new Dependent { Id = 10, EmployeeId = 12, FirstName = "SonFirstName", LastName = "SonLastName", MiddleName = "S", DisplayName = "SonLastName, SonFirstName S", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Son" },
                    new Dependent { Id = 11, EmployeeId = 12, FirstName = "DaugtherFirsName", LastName = "DaugtherLastName", MiddleName = "T", DisplayName = "DaugtherLastName, DaugtherFirsName T", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "F", CivilStatus = "Married", Relationship = "Daugther" },
                    new Dependent { Id = 12, EmployeeId = 12, FirstName = "WifeFirstName", LastName = "WifeFirstName", MiddleName = "A", DisplayName = "WifeFirstName, WifeFirstName A", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "F", CivilStatus = "Single", Relationship = "Wife" },
                    new Dependent { Id = 13, EmployeeId = 16, FirstName = "SonFirstName", LastName = "SonLastName", MiddleName = "V", DisplayName = "SonLastName, SonFirstName V", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Annulled", Relationship = "Son" },
                    new Dependent { Id = 14, EmployeeId = 16, FirstName = "DaugtherFirsName", LastName = "DaugtherLastName", MiddleName = "Y", DisplayName = "DaugtherLastName, DaugtherFirsName Y", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Single", Relationship = "Daugther" },
                    new Dependent { Id = 15, EmployeeId = 16, FirstName = "WifeFirstName", LastName = "WifeFirstName", MiddleName = "T", DisplayName = "WifeFirstName, WifeFirstName T", BirthDate = DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false)), Gender = "M", CivilStatus = "Married", Relationship = "Wife" }

                );

                context.DoSave(entityName);
            });
#endif
        }
    }
}
