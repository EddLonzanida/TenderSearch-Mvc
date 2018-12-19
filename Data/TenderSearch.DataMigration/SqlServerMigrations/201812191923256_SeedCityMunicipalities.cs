using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedCityMunicipalities : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, ProvinceId, ZipCode, Name";


        private readonly string tableName;

        public SeedCityMunicipalities()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 1, 'xxxx', 'Adams')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 1, 'xxxx', 'Bacarra')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 1, 'xxxx', 'Badoc')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 1, 'xxxx', 'Bangui')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 1, 'xxxx', 'City of Batac')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 1, 'xxxx', 'Burgos')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 1, 'xxxx', 'Carasi')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 1, 'xxxx', 'Currimao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 1, 'xxxx', 'Dingras')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 1, 'xxxx', 'Dumalneg')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 1, 'xxxx', 'Banna (Espiritu)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 1, 'xxxx', 'Laoag City (Capital)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 1, 'xxxx', 'Marcos')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 1, 'xxxx', 'Nueva Era')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 1, 'xxxx', 'Pagudpud')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (16, 1, 'xxxx', 'Paoay')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (17, 1, 'xxxx', 'Pasuquin')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (18, 1, 'xxxx', 'Piddig')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (19, 1, 'xxxx', 'Pinili')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (20, 1, 'xxxx', 'San Nicolas')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (21, 1, 'xxxx', 'Sarrat')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (22, 1, 'xxxx', 'Solsona')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (23, 1, 'xxxx', 'Vintar')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (24, 2, 'xxxx', 'Alilem')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (25, 2, 'xxxx', 'Banayoyo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (26, 2, 'xxxx', 'Bantay')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (27, 2, 'xxxx', 'Burgos')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (28, 2, 'xxxx', 'Cabugao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (29, 2, 'xxxx', 'City of Candon')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (30, 2, 'xxxx', 'Caoayan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (31, 2, 'xxxx', 'Cervantes')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (32, 2, 'xxxx', 'Galimuyod')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (33, 2, 'xxxx', 'Gregorio Del Pilar (Concepcion)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (34, 2, 'xxxx', 'Lidlidda')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (35, 2, 'xxxx', 'Magsingal')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (36, 2, 'xxxx', 'Nagbukel')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (37, 2, 'xxxx', 'Narvacan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (38, 2, 'xxxx', 'Quirino (Angkaki)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (39, 2, 'xxxx', 'Salcedo (Baugen)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (40, 2, 'xxxx', 'San Emilio')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (41, 2, 'xxxx', 'San Esteban')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (42, 2, 'xxxx', 'San Ildefonso')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (43, 2, 'xxxx', 'San Juan (Lapog)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (44, 2, 'xxxx', 'San Vicente')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (45, 2, 'xxxx', 'Santa')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (46, 2, 'xxxx', 'Santa Catalina')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (47, 2, 'xxxx', 'Santa Cruz')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (48, 2, 'xxxx', 'Santa Lucia')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (49, 2, 'xxxx', 'Santa Maria')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (50, 2, 'xxxx', 'Santiago')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (51, 2, 'xxxx', 'Santo Domingo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (52, 2, 'xxxx', 'Sigay')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (53, 2, 'xxxx', 'Sinait')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (54, 2, 'xxxx', 'Sugpon')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (55, 2, 'xxxx', 'Suyo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (56, 2, 'xxxx', 'Tagudin')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (57, 2, 'xxxx', 'City of Vigan (Capital)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (58, 3, 'xxxx', 'Agoo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (59, 3, 'xxxx', 'Aringay')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (60, 3, 'xxxx', 'Bacnotan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (61, 3, 'xxxx', 'Bagulin')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (62, 3, 'xxxx', 'Balaoan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (63, 3, 'xxxx', 'Bangar')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (64, 3, 'xxxx', 'Bauang')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (65, 3, 'xxxx', 'Burgos')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (66, 3, 'xxxx', 'Caba')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (67, 3, 'xxxx', 'Luna')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (68, 3, 'xxxx', 'Naguilian')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (69, 3, 'xxxx', 'Pugo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (70, 3, 'xxxx', 'Rosario')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (71, 3, 'xxxx', 'City of San Fernando (Capital)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (72, 3, 'xxxx', 'San Gabriel')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (73, 3, 'xxxx', 'San Juan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (74, 3, 'xxxx', 'Santo Tomas')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (75, 3, 'xxxx', 'Santol')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (76, 3, 'xxxx', 'Sudipen')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (77, 3, 'xxxx', 'Tubao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (78, 4, 'xxxx', 'Agno')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (79, 4, 'xxxx', 'Aguilar')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (80, 4, 'xxxx', 'City of Alaminos')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (81, 4, 'xxxx', 'Alcala')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (82, 4, 'xxxx', 'Anda')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (83, 4, 'xxxx', 'Asingan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (84, 4, 'xxxx', 'Balungao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (85, 4, 'xxxx', 'Bani')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (86, 4, 'xxxx', 'Basista')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (87, 4, 'xxxx', 'Bautista')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (88, 4, 'xxxx', 'Bayambang')

                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 88");
        }
    }
}
