using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedRegions : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, Name";

        private readonly string tableName;

        public SeedRegions()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 'Region I (Ilocos Region)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 'Region II (Cagayan Valley)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 'Region III (Central Luzon)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 'Region IV-A (Calabarzon)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 'Region IV-B (Mimaropa)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 'Region V (Bicol Region)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 'Region VI (Western Visayas)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 'Region VII (Central Visayas)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 'Region VIII (Eastern Visayas)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 'Region IX (Zamboanga Peninsula)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 'Region X (Northern Mindanao)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 'Region XI (Davao Region)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 'Region XII (Soccsksargen)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 'National Capital Region (NCR)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 'Cordillera Administrative Region (CAR)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (16, 'Autonomous Region In Muslim Mindanao (ARMM)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (17, 'Region XIII (CARAGA)')
                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 17");
        }
    }
}
