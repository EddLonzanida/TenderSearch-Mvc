using System.Data.Entity.Migrations;
using TenderSearch.Business.Common.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;

namespace TenderSearch.DataMigration.Seeders
{
    public static class RegionSeeder
    {
        public static void Seed<T>(T context)
            where T : Data.TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Region).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Regions.AddOrUpdate(r => r.Id,
                    new Region { Id = 1, Name = "Region I (Ilocos Region)" },
                    new Region { Id = 2, Name = "Region II (Cagayan Valley)" },
                    new Region { Id = 3, Name = "Region III (Central Luzon)" },
                    new Region { Id = 4, Name = "Region IV-A (Calabarzon)" },
                    new Region { Id = 5, Name = "Region IV-B (Mimaropa)" },
                    new Region { Id = 6, Name = "Region V (Bicol Region)" },
                    new Region { Id = 7, Name = "Region VI (Western Visayas)" },
                    new Region { Id = 8, Name = "Region VII (Central Visayas)" },
                    new Region { Id = 9, Name = "Region VIII (Eastern Visayas)" },
                    new Region { Id = 10, Name = "Region IX (Zamboanga Peninsula)" },
                    new Region { Id = 11, Name = "Region X (Northern Mindanao)" },
                    new Region { Id = 12, Name = "Region XI (Davao Region)" },
                    new Region { Id = 13, Name = "Region XII (Soccsksargen)" },
                    new Region { Id = 14, Name = "National Capital Region (NCR)" },
                    new Region { Id = 15, Name = "Cordillera Administrative Region (CAR)" },
                    new Region { Id = 16, Name = "Autonomous Region In Muslim Mindanao (ARMM)" },
                    new Region { Id = 17, Name = "Region XIII (CARAGA)" }
                );

                context.DoSave(entityName);
            });
#endif
        }
    }
}
