using TenderSearch.Business.Common.Entities;
using TenderSearch.Data;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;
using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.Seeders
{
    public static class LookupSeeder
    {
        public static void Seed<T>(T context)
            where T : TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Lookup).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Lookups.AddOrUpdate(r => r.Id,
                    new Lookup { Id = 1, Group = "EmployeeStatusType", SubGroup = null, Value = 1, Text = "Active" },
                    new Lookup { Id = 2, Group = "EmployeeStatusType", SubGroup = null, Value = 2, Text = "Pension" },
                    new Lookup { Id = 3, Group = "EmployeeStatusType", SubGroup = null, Value = 3, Text = "Survivor" },
                    new Lookup { Id = 4, Group = "AddressType", SubGroup = null, Value = 1, Text = "Home" },
                    new Lookup { Id = 5, Group = "AddressType", SubGroup = null, Value = 2, Text = "Office" },
                    new Lookup { Id = 6, Group = "AddressType", SubGroup = null, Value = 3, Text = "Business" },
                    new Lookup { Id = 7, Group = "AddressOwnerType", SubGroup = null, Value = 1, Text = "Employee" },
                    new Lookup { Id = 8, Group = "AddressOwnerType", SubGroup = null, Value = 2, Text = "Dependent" },
                    new Lookup { Id = 9, Group = "AddressOwnerType", SubGroup = null, Value = 3, Text = "Education" },
                    new Lookup { Id = 10, Group = "AddressOwnerType", SubGroup = null, Value = 4, Text = "Office" },
                    new Lookup { Id = 11, Group = "AddressOwnerType", SubGroup = null, Value = 5, Text = "Business" },
                    new Lookup { Id = 12, Group = "Dependent", SubGroup = null, Value = 1, Text = "Son" },
                    new Lookup { Id = 13, Group = "Dependent", SubGroup = null, Value = 2, Text = "Daugther" },
                    new Lookup { Id = 14, Group = "Dependent", SubGroup = null, Value = 3, Text = "Husband" },
                    new Lookup { Id = 15, Group = "Dependent", SubGroup = null, Value = 4, Text = "Wife" },
                    new Lookup { Id = 16, Group = "Dependent", SubGroup = null, Value = 5, Text = "Father" },
                    new Lookup { Id = 14, Group = "Dependent", SubGroup = null, Value = 6, Text = "Mother" },
                    new Lookup { Id = 17, Group = "Dependent", SubGroup = null, Value = 7, Text = "Brother" },
                    new Lookup { Id = 18, Group = "Dependent", SubGroup = null, Value = 8, Text = "Sister" },
                    new Lookup { Id = 19, Group = "Salutation", SubGroup = null, Value = 1, Text = "Mr." },
                    new Lookup { Id = 20, Group = "Salutation", SubGroup = null, Value = 2, Text = "Mrs." },
                    new Lookup { Id = 21, Group = "MaritalStatus", SubGroup = null, Value = 1, Text = "Single" },
                    new Lookup { Id = 22, Group = "MaritalStatus", SubGroup = null, Value = 2, Text = "Married" },
                    new Lookup { Id = 23, Group = "MaritalStatus", SubGroup = null, Value = 3, Text = "Widowed" },
                    new Lookup { Id = 24, Group = "MaritalStatus", SubGroup = null, Value = 4, Text = "Separated" },
                    new Lookup { Id = 25, Group = "MaritalStatus", SubGroup = null, Value = 5, Text = "Legally Separated" },
                    new Lookup { Id = 26, Group = "RankType", SubGroup = null, Value = 1, Text = "NONE" },
                    new Lookup { Id = 27, Group = "RankType", SubGroup = null, Value = 2, Text = "CIV" },
                    new Lookup { Id = 28, Group = "RankType", SubGroup = null, Value = 3, Text = "JO1" },
                    new Lookup { Id = 29, Group = "RankType", SubGroup = null, Value = 4, Text = "JO2" },
                    new Lookup { Id = 30, Group = "RankType", SubGroup = null, Value = 5, Text = "JO3" },
                    new Lookup { Id = 31, Group = "RankType", SubGroup = null, Value = 6, Text = "INSP" },
                    new Lookup { Id = 32, Group = "RankType", SubGroup = null, Value = 7, Text = "SINSP" },
                    new Lookup { Id = 33, Group = "RankType", SubGroup = null, Value = 8, Text = "SJO1" },
                    new Lookup { Id = 34, Group = "RankType", SubGroup = null, Value = 9, Text = "SJO2" },
                    new Lookup { Id = 35, Group = "RankType", SubGroup = null, Value = 10, Text = "SJO3" },
                    new Lookup { Id = 36, Group = "RankType", SubGroup = null, Value = 11, Text = "CSUPT" },
                    new Lookup { Id = 37, Group = "RankType", SubGroup = null, Value = 12, Text = "SJO4" },
                    new Lookup { Id = 38, Group = "RankType", SubGroup = null, Value = 13, Text = "SSUPT" },
                    new Lookup { Id = 39, Group = "RankType", SubGroup = null, Value = 14, Text = "SUPT" },
                    new Lookup { Id = 40, Group = "RankType", SubGroup = null, Value = 15, Text = "DIR" },
                    new Lookup { Id = 41, Group = "RankType", SubGroup = null, Value = 16, Text = "CINSP" },
                    new Lookup { Id = 42, Group = "PositionTitle", SubGroup = null, Value = 1, Text = "Accounts Officer" },
                    new Lookup { Id = 43, Group = "PositionTitle", SubGroup = null, Value = 2, Text = "Administrator" },
                    new Lookup { Id = 44, Group = "PositionTitle", SubGroup = null, Value = 3, Text = "ASST" },
                    new Lookup { Id = 45, Group = "PositionTitle", SubGroup = null, Value = 4, Text = "Audit" },
                    new Lookup { Id = 46, Group = "PositionTitle", SubGroup = null, Value = 5, Text = "Bookkeeper" },
                    new Lookup { Id = 47, Group = "PositionTitle", SubGroup = null, Value = 6, Text = "CAPCON Officer" },
                    new Lookup { Id = 48, Group = "PositionTitle", SubGroup = null, Value = 7, Text = "Data Extractor" },
                    new Lookup { Id = 49, Group = "PositionTitle", SubGroup = null, Value = 8, Text = "Extension" },
                    new Lookup { Id = 50, Group = "PositionTitle", SubGroup = null, Value = 9, Text = "Extra User" },
                    new Lookup { Id = 51, Group = "PositionTitle", SubGroup = null, Value = 10, Text = "General Clerk" },
                    new Lookup { Id = 52, Group = "PositionTitle", SubGroup = null, Value = 11, Text = "IT" },
                    new Lookup { Id = 53, Group = "PositionTitle", SubGroup = null, Value = 12, Text = "Loan Officer" },
                    new Lookup { Id = 54, Group = "PositionTitle", SubGroup = null, Value = 13, Text = "Loan Staff" },
                    new Lookup { Id = 55, Group = "PositionTitle", SubGroup = null, Value = 14, Text = "Manager/CEO" },
                    new Lookup { Id = 56, Group = "PositionTitle", SubGroup = null, Value = 15, Text = "Mgt Info. Specialist" },
                    new Lookup { Id = 57, Group = "PositionTitle", SubGroup = null, Value = 16, Text = "Officer" },
                    new Lookup { Id = 58, Group = "PositionTitle", SubGroup = null, Value = 17, Text = "P-IT-TS" },
                    new Lookup { Id = 59, Group = "PositionTitle", SubGroup = null, Value = 18, Text = "Sales Clerk" },
                    new Lookup { Id = 60, Group = "PositionTitle", SubGroup = null, Value = 19, Text = "System" },
                    new Lookup { Id = 61, Group = "PositionTitle", SubGroup = null, Value = 20, Text = "System Engineer" },
                    new Lookup { Id = 62, Group = "PositionTitle", SubGroup = null, Value = 21, Text = "Treasurer" },
                    new Lookup { Id = 63, Group = "PositionTitle", SubGroup = null, Value = 22, Text = "User" },
                    new Lookup { Id = 64, Group = "PositionTitle", SubGroup = null, Value = 23, Text = "Utility" },
                    new Lookup { Id = 65, Group = "CategoryType", SubGroup = null, Value = 1, Text = "Associate" },
                    new Lookup { Id = 66, Group = "CategoryType", SubGroup = null, Value = 2, Text = "Member" },
                    new Lookup { Id = 67, Group = "CategoryType", SubGroup = null, Value = 3, Text = "Other" },
                    new Lookup { Id = 68, Group = "CategoryType", SubGroup = null, Value = 4, Text = "Savers" },
                    new Lookup { Id = 69, Group = "Gender", SubGroup = null, Value = 1, Text = "Male" },
                    new Lookup { Id = 70, Group = "Gender", SubGroup = null, Value = 2, Text = "Female" },
                    new Lookup { Id = 71, Group = "Department", SubGroup = null, Value = 1, Text = "Department01" },
                    new Lookup { Id = 72, Group = "Department", SubGroup = null, Value = 2, Text = "Department02" },
                    new Lookup { Id = 73, Group = "Department", SubGroup = null, Value = 3, Text = "Department03" },
                    new Lookup { Id = 74, Group = "Department", SubGroup = null, Value = 4, Text = "Department04" },
                    new Lookup { Id = 75, Group = "ContractType", SubGroup = null, Value = 1, Text = "Standard contract" },
                    new Lookup { Id = 76, Group = "ContractType", SubGroup = null, Value = 2, Text = "Master contact" }

                );

                context.DoSave(entityName);
            });
#endif
        }
    }
}
