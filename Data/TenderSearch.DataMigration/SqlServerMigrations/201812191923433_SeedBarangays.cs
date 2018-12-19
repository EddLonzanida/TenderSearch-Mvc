using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedBarangays : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, CityMunicipalityId, Name";

        private readonly string tableName;

        public SeedBarangays()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 1, 'Adams (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 2, 'Bani')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 2, 'Buyon')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 2, 'Cabaruan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 2, 'Cabulalaan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 2, 'Cabusligan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 2, 'Cadaratan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 2, 'Calioet-Libong')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 2, 'Casilian')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 2, 'Corocor')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 2, 'Duripes')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 2, 'Ganagan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 2, 'Libtong')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 2, 'Macupit')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 2, 'Nambaran')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (16, 2, 'Natba')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (17, 2, 'Paninaan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (18, 2, 'Pasiocan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (19, 2, 'Pasngal')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (20, 2, 'Pipias')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (21, 2, 'Pulangi')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (22, 2, 'Pungto')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (23, 2, 'San Agustin I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (24, 2, 'San Agustin II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (25, 2, 'San Andres I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (26, 2, 'San Andres II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (27, 2, 'San Gabriel I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (28, 2, 'San Gabriel II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (29, 2, 'San Pedro I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (30, 2, 'San Pedro II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (31, 2, 'San Roque I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (32, 2, 'San Roque II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (33, 2, 'San Simon I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (34, 2, 'San Simon II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (35, 2, 'San Vicente (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (36, 2, 'Sangil')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (37, 2, 'Santa Filomena I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (38, 2, 'Santa Filomena II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (39, 2, 'Santa Rita (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (40, 2, 'Santo Cristo I (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (41, 2, 'Santo Cristo II (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (42, 2, 'Tambidao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (43, 2, 'Teppang')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (44, 2, 'Tubburan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (45, 3, 'Alay-Nangbabaan (Alay 15-B)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (46, 3, 'Alogoog')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (47, 3, 'Ar-arusip')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (48, 3, 'Aring')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (49, 3, 'Balbaldez')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (50, 3, 'Bato')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (51, 3, 'Camanga')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (52, 3, 'Canaan (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (53, 3, 'Caraitan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (54, 3, 'Gabut Norte')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (55, 3, 'Gabut Sur')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (56, 3, 'Garreta (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (57, 3, 'Labut')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (58, 3, 'Lacuben')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (59, 3, 'Lubigan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (60, 3, 'Mabusag Norte')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (61, 3, 'Mabusag Sur')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (62, 3, 'Madupayas')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (63, 3, 'Morong')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (64, 3, 'Nagrebcan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (65, 3, 'Napu')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (66, 3, 'La Virgen Milagrosa (Paguetpet)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (67, 3, 'Pagsanahan Norte')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (68, 3, 'Pagsanahan Sur')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (69, 3, 'Paltit')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (70, 3, 'Parang')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (71, 3, 'Pasuc')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (72, 3, 'Santa Cruz Norte')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (73, 3, 'Santa Cruz Sur')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (74, 3, 'Saud')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (75, 3, 'Turod')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (76, 4, 'Abaca')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (77, 4, 'Bacsil')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (78, 4, 'Banban')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (79, 4, 'Baruyen')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (80, 4, 'Dadaor')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (81, 4, 'Lanao')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (82, 4, 'Malasin')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (83, 4, 'Manayon')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (84, 4, 'Masikil')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (85, 4, 'Nagbalagan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (86, 4, 'Payac')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (87, 4, 'San Lorenzo (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (88, 4, 'Taguiporo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (89, 4, 'Utol')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (90, 5, 'Aglipay (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (91, 5, 'Baay')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (92, 5, 'Baligat')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (93, 5, 'Bungon')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (94, 5, 'Baoa East')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (95, 5, 'Baoa West')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (96, 5, 'Barani (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (97, 5, 'Ben-agan (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (98, 5, 'Bil-loca')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (99, 5, 'Biningan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (100, 5, 'Callaguip (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (101, 5, 'Camandingan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (102, 5, 'Camguidan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (103, 5, 'Cangrunaan (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (104, 5, 'Capacuan')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (105, 5, 'Caunayan (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (106, 5, 'Valdez Pob. (Caoayan)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (107, 5, 'Colo')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (108, 5, 'Pimentel (Cubol)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (109, 5, 'Dariwdiw')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (110, 5, 'Acosta Pob. (Iloilo)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (111, 5, 'Ablan Pob. (Labucao)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (112, 5, 'Lacub (Pob.)')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (113, 5, 'Mabaleng')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (114, 5, 'Magnuang')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (115, 5, 'Maipalig')
                    INSERT INTO {tableName} ({COLUMNS}) VALUES (116, 5, 'Nagbacalan')


                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 116");
        }
    }
}