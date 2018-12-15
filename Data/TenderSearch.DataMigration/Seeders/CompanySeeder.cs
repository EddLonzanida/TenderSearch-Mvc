using System.Data.Entity.Migrations;
using TenderSearch.Business.Common.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;
using TenderSearch.Data;

namespace TenderSearch.DataMigration.Seeders
{
    public static class CompanySeeder
    {
        public static void Seed<T>(T context)
            where T : TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Company).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Companies.AddOrUpdate(r => r.Id,
                    new Company { Id = 1, Name = "Company01", Description = "Description01", Website = "http://Website01.com", AbnCan = "AbnCan01" },
                    new Company { Id = 2, Name = "Company02", Description = "Description02", Website = "http://Website02.com", AbnCan = "AbnCan02" }
                );

                context.DoSave(entityName);
            });
#endif
        }
    }
}