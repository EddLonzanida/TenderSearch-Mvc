using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedContracts : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string COLUMNS = "Id, CompanyId, ContractType, Price, DateSigned, EndDate, RenewalDate";

        private readonly string tableName;

        public SeedContracts()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            Sql($@"SET IDENTITY_INSERT {tableName} ON; 
                INSERT INTO {tableName} ({COLUMNS}) VALUES (1, 1, 'Master contact', 100, '2017-01-18', '2018-01-18', '2017-01-18')
                INSERT INTO {tableName} ({COLUMNS}) VALUES (2, 1, 'Standard contact', 200, '2018-01-19', '2019-01-19', '2018-01-19')
                SET IDENTITY_INSERT {tableName} OFF");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND 2");
        }
    }
}
