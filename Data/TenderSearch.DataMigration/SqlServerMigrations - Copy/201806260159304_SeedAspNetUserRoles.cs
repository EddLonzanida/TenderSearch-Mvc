using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedAspNetUserRoles : DbMigration
    {
        private const string TABLE_NAME = "AspNetUserRoles";

        public override void Up()
        {
            const string COLUMNS = "UserId, RoleId";

            Sql($@"INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('07299bc6-21dd-442d-b0bc-545966dd2881', '385af7e8-b598-4ed7-a61c-a7d241707909')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('96a326f7-46bd-4d77-9efd-71ef8de7b116', '385af7e8-b598-4ed7-a61c-a7d241707909')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('5ea506ec-ef49-4bd1-bf7e-9c3bb42bdb16', '3aec2eef-6ae3-4162-9113-c3d850d311dc')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('96a326f7-46bd-4d77-9efd-71ef8de7b116', '3aec2eef-6ae3-4162-9113-c3d850d311dc')
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('96a326f7-46bd-4d77-9efd-71ef8de7b116', 'ce066b63-deab-4da6-bbd9-9384cd022d18')");
        }

        public override void Down()
        {
            const string ID_COLUMN1 = "UserId";
            const string ID_COLUMN2 = "RoleId";

            Sql($@"DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN1} = '07299bc6-21dd-442d-b0bc-545966dd2881' AND {ID_COLUMN2} = '385af7e8-b598-4ed7-a61c-a7d241707909'
                    DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN1} = '96a326f7-46bd-4d77-9efd-71ef8de7b116' AND {ID_COLUMN2} = '385af7e8-b598-4ed7-a61c-a7d241707909'
                    DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN1} = '5ea506ec-ef49-4bd1-bf7e-9c3bb42bdb16' AND {ID_COLUMN2} = '3aec2eef-6ae3-4162-9113-c3d850d311dc'
                    DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN1} = '96a326f7-46bd-4d77-9efd-71ef8de7b116' AND {ID_COLUMN2} = '3aec2eef-6ae3-4162-9113-c3d850d311dc'
                    DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN1} = '96a326f7-46bd-4d77-9efd-71ef8de7b116' AND {ID_COLUMN2} = 'ce066b63-deab-4da6-bbd9-9384cd022d18'");
        }
    }
}
