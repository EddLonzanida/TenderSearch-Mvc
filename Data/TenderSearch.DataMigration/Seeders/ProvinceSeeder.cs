using System.Data.Entity.Migrations;
using TenderSearch.Business.Common.Entities;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;

namespace TenderSearch.DataMigration.Seeders
{
    public static class ProvinceSeeder
    {
        public static void Seed<T>(T context)
            where T : Data.TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Province).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Provinces.AddOrUpdate(r => r.Id,
                    new Province { Id = 1, RegionId = 15, Name = "Abra" }, 
                    new Province { Id = 2, RegionId = 17, Name = "Agusan Del Norte" }, 
                    new Province { Id = 3, RegionId = 17, Name = "Agusan Del Sur" }, 
                    new Province { Id = 4, RegionId = 7, Name = "Aklan" }, 
                    new Province { Id = 5, RegionId = 6, Name = "Albay" }, 
                    new Province { Id = 6, RegionId = 7, Name = "Antique" }, 
                    new Province { Id = 7, RegionId = 15, Name = "Apayao" }, 
                    new Province { Id = 8, RegionId = 3, Name = "Aurora" }, 
                    new Province { Id = 9, RegionId = 16, Name = "Basilan" }, 
                    new Province { Id = 10, RegionId = 3, Name = "Bataan" }, 
                    new Province { Id = 11, RegionId = 2, Name = "Batanes" }, 
                    new Province { Id = 12, RegionId = 4, Name = "Batangas" }, 
                    new Province { Id = 13, RegionId = 15, Name = "Benguet" }, 
                    new Province { Id = 14, RegionId = 9, Name = "Biliran" }, 
                    new Province { Id = 15, RegionId = 8, Name = "Bohol" }, 
                    new Province { Id = 16, RegionId = 11, Name = "Bukidnon" }, 
                    new Province { Id = 17, RegionId = 3, Name = "Bulacan" }, 
                    new Province { Id = 18, RegionId = 2, Name = "Cagayan" }, 
                    new Province { Id = 19, RegionId = 6, Name = "Camarines Norte" }, 
                    new Province { Id = 20, RegionId = 6, Name = "Camarines Sur" }, 
                    new Province { Id = 21, RegionId = 11, Name = "Camiguin" }, 
                    new Province { Id = 22, RegionId = 7, Name = "Capiz" }, 
                    new Province { Id = 23, RegionId = 6, Name = "Catanduanes" }, 
                    new Province { Id = 24, RegionId = 4, Name = "Cavite" }, 
                    new Province { Id = 25, RegionId = 8, Name = "Cebu" }, 
                    new Province { Id = 26, RegionId = 14, Name = "City of Manila" }, 
                    new Province { Id = 27, RegionId = 12, Name = "Compostela Valley" }, 
                    new Province { Id = 28, RegionId = 13, Name = "Cotabato (North Cotabato)" }, 
                    new Province { Id = 29, RegionId = 13, Name = "Cotabato City" }, 
                    new Province { Id = 30, RegionId = 12, Name = "Davao Del Norte" }, 
                    new Province { Id = 31, RegionId = 12, Name = "Davao Del Sur" }, 
                    new Province { Id = 32, RegionId = 12, Name = "Davao Occidental" }, 
                    new Province { Id = 33, RegionId = 12, Name = "Davao Oriental" }, 
                    new Province { Id = 34, RegionId = 17, Name = "Dinagat Islands" }, 
                    new Province { Id = 35, RegionId = 9, Name = "Eastern Samar" }, 
                    new Province { Id = 36, RegionId = 7, Name = "Guimaras" }, 
                    new Province { Id = 37, RegionId = 15, Name = "Ifugao" }, 
                    new Province { Id = 38, RegionId = 1, Name = "Ilocos Norte" }, 
                    new Province { Id = 39, RegionId = 1, Name = "Ilocos Sur" }, 
                    new Province { Id = 40, RegionId = 7, Name = "Iloilo" }, 
                    new Province { Id = 41, RegionId = 2, Name = "Isabela" }, 
                    new Province { Id = 42, RegionId = 10, Name = "Isabela" }, 
                    new Province { Id = 43, RegionId = 15, Name = "Kalinga" }, 
                    new Province { Id = 44, RegionId = 1, Name = "La Union" }, 
                    new Province { Id = 45, RegionId = 4, Name = "Laguna" }, 
                    new Province { Id = 46, RegionId = 11, Name = "Lanao Del Norte" }, 
                    new Province { Id = 47, RegionId = 16, Name = "Lanao Del Sur" }, 
                    new Province { Id = 48, RegionId = 9, Name = "Leyte" }, 
                    new Province { Id = 49, RegionId = 16, Name = "Maguindanao" }, 
                    new Province { Id = 50, RegionId = 5, Name = "Marinduque" }, 
                    new Province { Id = 51, RegionId = 6, Name = "Masbate" }, 
                    new Province { Id = 52, RegionId = 11, Name = "Misamis Occidental" }, 
                    new Province { Id = 53, RegionId = 11, Name = "Misamis Oriental" }, 
                    new Province { Id = 54, RegionId = 15, Name = "Mountain Province" }, 
                    new Province { Id = 55, RegionId = 14, Name = "NCR, City of Manila, First District" }, 
                    new Province { Id = 56, RegionId = 14, Name = "NCR, Fourth District" }, 
                    new Province { Id = 57, RegionId = 14, Name = "NCR, Second District" }, 
                    new Province { Id = 58, RegionId = 14, Name = "NCR, Third District" }, 
                    new Province { Id = 59, RegionId = 7, Name = "Negros Occidental" }, 
                    new Province { Id = 60, RegionId = 8, Name = "Negros Oriental" }, 
                    new Province { Id = 61, RegionId = 9, Name = "Northern Samar" }, 
                    new Province { Id = 62, RegionId = 3, Name = "Nueva Ecija" }, 
                    new Province { Id = 63, RegionId = 2, Name = "Nueva Vizcaya" }, 
                    new Province { Id = 64, RegionId = 5, Name = "Occidental Mindoro" }, 
                    new Province { Id = 65, RegionId = 5, Name = "Oriental Mindoro" }, 
                    new Province { Id = 66, RegionId = 5, Name = "Palawan" }, 
                    new Province { Id = 67, RegionId = 3, Name = "Pampanga" }, 
                    new Province { Id = 68, RegionId = 1, Name = "Pangasinan" }, 
                    new Province { Id = 69, RegionId = 4, Name = "Quezon" }, 
                    new Province { Id = 70, RegionId = 2, Name = "Quirino" }, 
                    new Province { Id = 71, RegionId = 4, Name = "Rizal" }, 
                    new Province { Id = 72, RegionId = 5, Name = "Romblon" }, 
                    new Province { Id = 73, RegionId = 9, Name = "Samar (Western Samar)" }, 
                    new Province { Id = 74, RegionId = 13, Name = "Sarangani" }, 
                    new Province { Id = 75, RegionId = 8, Name = "Siquijor" }, 
                    new Province { Id = 76, RegionId = 6, Name = "Sorsogon" }, 
                    new Province { Id = 77, RegionId = 13, Name = "South Cotabato" }, 
                    new Province { Id = 78, RegionId = 9, Name = "Southern Leyte" }, 
                    new Province { Id = 79, RegionId = 13, Name = "Sultan Kudarat" }, 
                    new Province { Id = 80, RegionId = 16, Name = "Sulu" }, 
                    new Province { Id = 81, RegionId = 17, Name = "Surigao Del Norte" }, 
                    new Province { Id = 82, RegionId = 17, Name = "Surigao Del Sur" }, 
                    new Province { Id = 83, RegionId = 3, Name = "Tarlac" }, 
                    new Province { Id = 84, RegionId = 16, Name = "Tawi-Tawi" }, 
                    new Province { Id = 85, RegionId = 3, Name = "Zambales" }, 
                    new Province { Id = 86, RegionId = 10, Name = "Zamboanga Del Norte" }, 
                    new Province { Id = 87, RegionId = 10, Name = "Zamboanga Del Sur" }, 
                    new Province { Id = 88, RegionId = 10, Name = "Zamboanga Sibugay" }
                );

                context.DoSave(entityName);
            });
#endif
        }
    }
}