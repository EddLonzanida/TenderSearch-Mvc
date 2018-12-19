using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedLookups : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, [Group], SubGroup, Value, Text";

        private readonly string tableName;

        public SeedLookups()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 'EmployeeStatusType', null, 1, 'Active')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 'EmployeeStatusType', null, 2, 'Pension')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 'EmployeeStatusType', null, 3, 'Survivor')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 'AddressType', null, 1, 'Home')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 'AddressType', null, 2, 'Office')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 'AddressType', null, 3, 'Business')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 'AddressOwnerType', null, 1, 'Employee')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 'AddressOwnerType', null, 2, 'Dependent')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 'AddressOwnerType', null, 3, 'Education')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 'AddressOwnerType', null, 4, 'Office')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 'AddressOwnerType', null, 5, 'Business')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 'Dependent', null, 1, 'Son')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 'Dependent', null, 2, 'Daugther')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 'Dependent', null, 3, 'Husband')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 'Dependent', null, 4, 'Wife')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (16, 'Dependent', null, 5, 'Father')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (17, 'Dependent', null, 6, 'Mother')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (18, 'Dependent', null, 7, 'Brother')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (19, 'Dependent', null, 8, 'Sister')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (20, 'Salutation', null, 1, 'Mr.')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (21, 'Salutation', null, 2, 'Mrs.')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (22, 'MaritalStatus', null, 1, 'Single')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (23, 'MaritalStatus', null, 2, 'Married')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (24, 'MaritalStatus', null, 3, 'Widowed')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (25, 'MaritalStatus', null, 4, 'Separated')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (26, 'MaritalStatus', null, 5, 'Legally Separated')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (27, 'RankType', null, 1, 'NONE')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (28, 'RankType', null, 2, 'CIV')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (29, 'RankType', null, 3, 'JO1')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (30, 'RankType', null, 4, 'JO2')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (31, 'RankType', null, 5, 'JO3')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (32, 'RankType', null, 6, 'INSP')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (33, 'RankType', null, 7, 'SINSP')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (34, 'RankType', null, 8, 'SJO1')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (35, 'RankType', null, 9, 'SJO2')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (36, 'RankType', null, 10, 'SJO3')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (37, 'RankType', null, 11, 'CSUPT')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (38, 'RankType', null, 12, 'SJO4')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (39, 'RankType', null, 13, 'SSUPT')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (40, 'RankType', null, 14, 'SUPT')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (41, 'RankType', null, 15, 'DIR')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (42, 'RankType', null, 16, 'CINSP')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (43, 'PositionTitle', null, 1, 'Accounts Officer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (44, 'PositionTitle', null, 2, 'Administrator')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (45, 'PositionTitle', null, 3, 'ASST')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (46, 'PositionTitle', null, 4, 'Audit')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (47, 'PositionTitle', null, 5, 'Bookkeeper')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (48, 'PositionTitle', null, 6, 'CAPCON Officer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (49, 'PositionTitle', null, 7, 'Data Extractor')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (50, 'PositionTitle', null, 8, 'Extension')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (51, 'PositionTitle', null, 9, 'Extra User')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (52, 'PositionTitle', null, 10, 'General Clerk')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (53, 'PositionTitle', null, 11, 'IT')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (54, 'PositionTitle', null, 12, 'Loan Officer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (55, 'PositionTitle', null, 13, 'Loan Staff')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (56, 'PositionTitle', null, 14, 'Manager/CEO')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (57, 'PositionTitle', null, 15, 'Mgt Info. Specialist')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (58, 'PositionTitle', null, 16, 'Officer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (59, 'PositionTitle', null, 17, 'P-IT-TS')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (60, 'PositionTitle', null, 18, 'Sales Clerk')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (61, 'PositionTitle', null, 19, 'System')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (62, 'PositionTitle', null, 20, 'System Engineer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (63, 'PositionTitle', null, 21, 'Treasurer')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (64, 'PositionTitle', null, 22, 'User')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (65, 'PositionTitle', null, 23, 'Utility')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (66, 'CategoryType', null, 1, 'Associate')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (67, 'CategoryType', null, 2, 'Member')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (68, 'CategoryType', null, 3, 'Other')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (69, 'CategoryType', null, 4, 'Savers')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (70, 'Gender', null, 1, 'Male')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (71, 'Gender', null, 2, 'Female')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (72, 'Department', null, 1, 'Department01')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (73, 'Department', null, 2, 'Department02')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (74, 'Department', null, 3, 'Department03')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (75, 'Department', null, 4, 'Department04')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (76, 'ContractType', null, 1, 'Standard contract')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (77, 'ContractType', null, 2, 'Master contact')

                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 77");
        }
    }
}
