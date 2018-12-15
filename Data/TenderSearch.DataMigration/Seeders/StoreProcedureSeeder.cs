using System;
using System.IO;
using System.Linq;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;

namespace TenderSearch.DataMigration.Seeders
{
    public static class StoreProcedureSeeder
    {
        public static void Seed<T>(T context)
            where T : Data.TenderSearchDb
        {
#if DEBUG
            Seeder.Execute("StoreProcedures", () =>
            {
                var sqlForDrop = GetDropScriptForGetContractDuplicates();
                var sqlForCreate = GetCreateScriptForGetContractDuplicates();

                context.Database.ExecuteSqlCommand(sqlForDrop, new object[0]);
                context.Database.ExecuteSqlCommand(sqlForCreate, new object[0]);
                context.DoSave("StoreProcedures");
            });
#endif
        }

        public static string GetCreateScriptForGetContractDuplicates()
        {
            var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SqlScripts");
            var sqlForCreate = Directory.GetFiles(baseDirectory, "Create*.sql").First();

            return File.ReadAllText(sqlForCreate);
        }

        public static string GetDropScriptForGetContractDuplicates()
        {
            var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SqlScripts");
            var sqlForDrop = Directory.GetFiles(baseDirectory, "Drop*.sql").First();

            return File.ReadAllText(sqlForDrop);
        }
    }
}