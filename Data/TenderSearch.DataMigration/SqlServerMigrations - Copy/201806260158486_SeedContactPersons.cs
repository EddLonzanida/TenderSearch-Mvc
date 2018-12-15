using System.Data.Entity.Migrations;

namespace TenderSearch.DataMigration.SqlServerMigrations
{
    public partial class SeedContactPersons : DbMigration
    {
        private const string ID_COLUMN = "Id";
        private const string TABLE_NAME = "Employees";

        public override void Up()
        {
            const string COLUMNS = "Id, CompanyId, EmployeeNumber, FirstName, LastName, MiddleName, DisplayName, BirthDate, Gender, CivilStatus, AtmNumber, SssNumber, TinNumber, PhilHealthNumber, PagIbigNumber, PassportNumber, DepartmentName, RankTypeId, CategoryTypeId, GroupAreaId, StatusTypeId, MembershipDate, HiredDate, EmploymentEndDate, EmployeeId";

            Sql($@"SET IDENTITY_INSERT {TABLE_NAME} ON; 
                INSERT INTO {TABLE_NAME} ({COLUMNS}) VALUES (1, 1, 'Z160260', 'Nico Angelo', 'Zulueta', 'N', 'Zulueta, Nico Angelo N', '2018-11-12', 'M', 'Single', '37248657',  '123456',  '123456', '123456', '123456', '123456', 'Department01',3, 2, 1, 1, '2018-11-12', '2018-11-12', '2018-11-12')
                SET IDENTITY_INSERT {TABLE_NAME} OFF");

        }

        public override void Down()
        {
            Sql($@"DELETE FROM {TABLE_NAME} WHERE {ID_COLUMN} IN (1)");
        }
    }
}
