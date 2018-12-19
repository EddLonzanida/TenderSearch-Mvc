using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedCompanies : DbMigration
    {
        private const int ROW_COUNT = 20;
        private const string ID_COLUMN = "Id";

        private readonly string tableName;

        public SeedCompanies()
        {
            tableName = GetType().Name.Replace("Seed", string.Empty);
        }

        public override void Up()
        {
            const string COLUMNS = "Id, Name, Description, Website, AbnCan";

            Sql($@"SET IDENTITY_INSERT {tableName} ON;");

            for (var i = 1; i <= ROW_COUNT; i++)
            {
                var ctr = i.ToString("00");

                Sql($@"INSERT INTO {tableName} ({COLUMNS}) VALUES ({i}, 'Company{ctr}', 'Description{ctr}', 'http://Website{ctr}.com', 'AbnCan{ctr}')");
            }

            Sql($@"SET IDENTITY_INSERT {tableName} OFF;");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {tableName} WHERE {ID_COLUMN} BETWEEN 1 AND {ROW_COUNT}");
        }
    }
}
