using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedProvinces : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, RegionId, Name";

        private readonly string tableName;

        public SeedProvinces()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 1, 'Ilocos Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 1, 'Ilocos Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 1, 'La Union')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 1, 'Pangasinan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 2, 'Batanes')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 2, 'Cagayan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 2, 'Isabela')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 2, 'Nueva Vizcaya')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 2, 'Quirino')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 3, 'Bataan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 3, 'Bulacan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 3, 'Nueva Ecija')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 3, 'Pampanga')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 3, 'Tarlac')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 3, 'Zambales')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (16, 3, 'Aurora')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (17, 4, 'Batangas')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (18, 4, 'Cavite')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (19, 4, 'Laguna')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (20, 4, 'Quezon')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (21, 4, 'Rizal')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (22, 5, 'Marinduque')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (23, 5, 'Occidental Mindoro')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (24, 5, 'Oriental Mindoro')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (25, 5, 'Palawan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (26, 5, 'Romblon')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (27, 6, 'Albay')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (28, 6, 'Camarines Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (29, 6, 'Camarines Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (30, 6, 'Catanduanes')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (31, 6, 'Masbate')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (32, 6, 'Sorsogon')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (33, 7, 'Aklan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (34, 7, 'Antique')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (35, 7, 'Capiz')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (36, 7, 'Iloilo')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (37, 7, 'Negros Occidental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (38, 7, 'Guimaras')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (39, 8, 'Bohol')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (40, 8, 'Cebu')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (41, 8, 'Negros Oriental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (42, 8, 'Siquijor')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (43, 9, 'Eastern Samar')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (44, 9, 'Leyte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (45, 9, 'Northern Samar')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (46, 9, 'Samar (Western Samar)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (47, 9, 'Southern Leyte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (48, 9, 'Biliran')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (49, 10, 'Zamboanga Del Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (50, 10, 'Zamboanga Del Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (51, 10, 'Zamboanga Sibugay')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (52, 10, 'City of Isabela')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (53, 11, 'Bukidnon')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (54, 11, 'Camiguin')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (55, 11, 'Lanao Del Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (56, 11, 'Misamis Occidental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (57, 11, 'Misamis Oriental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (58, 12, 'Davao Del Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (59, 12, 'Davao Del Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (60, 12, 'Davao Oriental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (61, 12, 'Compostela Valley')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (62, 12, 'Davao Occidental')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (63, 13, 'Cotabato (North Cotabato)')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (64, 13, 'South Cotabato')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (65, 13, 'Sultan Kudarat')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (66, 13, 'Sarangani')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (67, 13, 'Cotabato City')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (68, 14, 'NCR, City of Manila, First District')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (69, 14, 'City of Manila')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (70, 14, 'NCR, Second District')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (71, 14, 'NCR, Third District')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (72, 14, 'NCR, Fourth District')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (73, 15, 'Abra')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (74, 15, 'Benguet')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (75, 15, 'Ifugao')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (76, 15, 'Kalinga')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (77, 15, 'Mountain Province')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (78, 15, 'Apayao')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (79, 16, 'Basilan')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (80, 16, 'Lanao Del Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (81, 16, 'Maguindanao')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (82, 16, 'Sulu')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (83, 16, 'Tawi-Tawi')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (84, 17, 'Agusan Del Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (85, 17, 'Agusan Del Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (86, 17, 'Surigao Del Norte')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (87, 17, 'Surigao Del Sur')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (88, 17, 'Dinagat Islands')
                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 88");
        }
    }
}
