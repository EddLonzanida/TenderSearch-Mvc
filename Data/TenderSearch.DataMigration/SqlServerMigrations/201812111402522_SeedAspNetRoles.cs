using TenderSearch.Contracts.Infrastructure;
using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedAspNetRoles : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string TABLE_NAME = "AspNetRoles";

        public override void Up()
        {
            const string COLUMNS = "Id, Name";

            Sql($@"INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('ce066b63-deab-4da6-bbd9-9384cd022d18', '{eUserRoles.Admins.ToString()}')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('3aec2eef-6ae3-4162-9113-c3d850d311dc', '{eUserRoles.UserManagers.ToString()}')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('385af7e8-b598-4ed7-a61c-a7d241707909', '{eUserRoles.Users.ToString()}')");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN} IN ('ce066b63-deab-4da6-bbd9-9384cd022d18', '3aec2eef-6ae3-4162-9113-c3d850d311dc', '385af7e8-b598-4ed7-a61c-a7d241707909')");
        }
    }
}
