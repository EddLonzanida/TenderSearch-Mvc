using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedCompanies : DbMigration
    {
        private const int ROW_COUNT = 20;
        private const string ID_COLUMN = "Id";
        private const string TABLE_NAME = "Companies";

        public override void Up()
        {
            const string COLUMNS = "Id, Name, Description, Website, AbnCan";

            Sql($@"SET IDENTITY_INSERT {TABLE_NAME} ON;");

            for (var i = 1; i <= ROW_COUNT; i++)
            {
                var ctr = i.ToString("00");

                Sql($@"INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ({i}, 'Company{ctr}', 'Description{ctr}', 'http://Website{ctr}.com', 'AbnCan{ctr}')");
            }

            Sql($@"SET IDENTITY_INSERT {TABLE_NAME} OFF;");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN} BETWEEN 1 AND {ROW_COUNT}");
        }
    }
}
