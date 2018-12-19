using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedAspNetUsers : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string TABLE_NAME = "AspNetUsers";

        public override void Up()
        {
            const string COLUMNS = @"Id, Email, UserName, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount";

            Sql($@"INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('07299bc6-21dd-442d-b0bc-545966dd2881', 'Joseph@Bjmp.com', 'Joseph', 1, 'AKg0c9qpOqEcyi3GCbweCUPQh2bhn6WgZ0CdXB6JgKMTDtXZtGcbm5HaC7KyjTLdQQ==', 'd85e4921-8227-40ef-8492-59b4acb09db7', 0, 0, 0, 0)
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('5ea506ec-ef49-4bd1-bf7e-9c3bb42bdb16', 'Francis@Bjmp.com', 'Francis', 1, 'ANh4LxzRhHpljrGz+7MyN3HwDIiywqm5/JGsbiS4ZnOBpv01Z42Z3L/IGvFKJbMODA==', '2c5eca7e-90c0-4353-970a-e2fe9a8751ba', 0, 0, 0, 0)
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES ('96a326f7-46bd-4d77-9efd-71ef8de7b116', 'Edd@Bjmp.com', 'Edd', 1, 'AEOSluS5uhEJ2ItFNaZMTkmYbKJVwNSIgH+Ol4qHg+zEU3/s75ev20PCUNklaAarzw==', '2d9c308a-43b8-4c51-bad1-a882abe99442', 0, 0, 0, 0)");
        }

        public override void Down()
        {
            Sql($@"DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN} IN ('07299bc6-21dd-442d-b0bc-545966dd2881'
                                                        , '5ea506ec-ef49-4bd1-bf7e-9c3bb42bdb16'
                                                        , '96a326f7-46bd-4d77-9efd-71ef8de7b116')");
        }
    }
}
