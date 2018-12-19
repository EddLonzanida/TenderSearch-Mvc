    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;

    namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedDependents : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, EmployeeId, FirstName, LastName, MiddleName, DisplayName, BirthDate, Gender, CivilStatus, Relationship";

        private readonly string tableName;

        public SeedDependents()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 1, 'SonFirstName', 'SonLastName', 'N', 'SonLastName, SonFirstName N', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Son')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 1, 'DaugtherFirsName', 'DaugtherLastName', 'G', 'DaugtherLastName, DaugtherFirsName G', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Daugther')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (3, 1, 'WifeFirstName', 'WifeFirstName', 'G', 'WifeFirstName, WifeFirstName G', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Married', 'Wife')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (4, 4, 'SonFirstName', 'SonLastName', 'M', 'SonLastName, SonFirstName M', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Son')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (5, 4, 'DaugtherFirsName', 'DaugtherLastName', 'D', 'DaugtherLastName, DaugtherFirsName D', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Daugther')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (6, 4, 'WifeFirstName', 'WifeFirstName', 'P', 'WifeFirstName, WifeFirstName P', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Annulled', 'Wife')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (7, 8, 'SonFirstName', 'SonLastName', 'P', 'SonLastName, SonFirstName P', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Son')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (8, 8, 'DaugtherFirsName', 'DaugtherLastName', 'D', 'DaugtherLastName, DaugtherFirsName D', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Married', 'Daugther')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (9, 8, 'WifeFirstName', 'WifeFirstName', 'M', 'WifeFirstName, WifeFirstName M', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Wife')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (10, 12, 'SonFirstName', 'SonLastName', 'S', 'SonLastName, SonFirstName S', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Son')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (11, 12, 'DaugtherFirsName', 'DaugtherLastName', 'T', 'DaugtherLastName, DaugtherFirsName T', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'F', 'Married', 'Daugther')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (12, 12, 'WifeFirstName', 'WifeFirstName', 'A', 'WifeFirstName, WifeFirstName A', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'F', 'Single', 'Wife')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (13, 16, 'SonFirstName', 'SonLastName', 'V', 'SonLastName, SonFirstName V', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Annulled', 'Son')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (14, 16, 'DaugtherFirsName', 'DaugtherLastName', 'Y', 'DaugtherLastName, DaugtherFirsName Y', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Single', 'Daugther')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (15, 16, 'WifeFirstName', 'WifeFirstName', 'T', 'WifeFirstName, WifeFirstName T', '{DateTime.Parse("2018-11-12", new CultureInfo("en-PH", false))}', 'M', 'Married', 'Wife')

                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 77");
        }
    }
}
