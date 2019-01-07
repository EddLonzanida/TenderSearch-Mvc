namespace TenderSearch.DataMigration.SqlServerMigrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        OwnerId = c.Int(nullable: false),
                        OwnerType = c.String(nullable: false, maxLength: 10),
                        BarangayId = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 50),
                        StreetAddress = c.String(nullable: false, maxLength: 256),
                        AddressType = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barangays", t => t.BarangayId, cascadeDelete: true)
                .Index(t => new { t.BarangayId, t.StreetAddress, t.AddressType, t.OwnerId, t.OwnerType }, unique: true, name: "IX_UniqueAll");
            
            CreateTable(
                "dbo.Barangays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                        CityMunicipalityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityMunicipalities", t => t.CityMunicipalityId, cascadeDelete: true)
                .Index(t => t.CityMunicipalityId);
            
            CreateTable(
                "dbo.CityMunicipalities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false, maxLength: 6),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        RegionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        Description = c.String(),
                        Website = c.String(nullable: false),
                        AbnCan = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        CompanyId = c.Int(nullable: false),
                        ContractType = c.String(nullable: false),
                        DateSigned = c.DateTime(),
                        EndDate = c.DateTime(),
                        RenewalDate = c.DateTime(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        CompanyId = c.Int(nullable: false),
                        EmployeeNumber = c.String(nullable: false, maxLength: 50),
                        DisplayName = c.String(maxLength: 50),
                        AtmNumber = c.String(maxLength: 15),
                        SssNumber = c.String(maxLength: 15),
                        TinNumber = c.String(maxLength: 15),
                        PhilHealthNumber = c.String(maxLength: 15),
                        PagIbigNumber = c.String(maxLength: 15),
                        PassportNumber = c.String(maxLength: 15),
                        DepartmentName = c.String(),
                        MembershipDate = c.DateTime(nullable: false),
                        HiredDate = c.DateTime(nullable: false),
                        EmploymentEndDate = c.DateTime(),
                        RankTypeId = c.Int(nullable: false),
                        CategoryTypeId = c.Int(nullable: false),
                        StatusTypeId = c.Int(nullable: false),
                        GroupAreaId = c.Int(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 2),
                        CivilStatus = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Dependents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        DisplayName = c.String(maxLength: 50),
                        Relationship = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 2),
                        CivilStatus = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateDeleted = c.DateTime(),
                        DeletionReason = c.String(),
                        Group = c.String(nullable: false, maxLength: 50),
                        SubGroup = c.String(maxLength: 50),
                        Value = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Group, t.SubGroup, t.Value, t.Text }, unique: true, name: "IX_UniqueAll");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Dependents", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Contracts", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Addresses", "BarangayId", "dbo.Barangays");
            DropForeignKey("dbo.Provinces", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.CityMunicipalities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Barangays", "CityMunicipalityId", "dbo.CityMunicipalities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Lookups", "IX_UniqueAll");
            DropIndex("dbo.Dependents", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.Contracts", new[] { "CompanyId" });
            DropIndex("dbo.Provinces", new[] { "RegionId" });
            DropIndex("dbo.CityMunicipalities", new[] { "ProvinceId" });
            DropIndex("dbo.Barangays", new[] { "CityMunicipalityId" });
            DropIndex("dbo.Addresses", "IX_UniqueAll");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Lookups");
            DropTable("dbo.Dependents");
            DropTable("dbo.Employees");
            DropTable("dbo.Contracts");
            DropTable("dbo.Companies");
            DropTable("dbo.Regions");
            DropTable("dbo.Provinces");
            DropTable("dbo.CityMunicipalities");
            DropTable("dbo.Barangays");
            DropTable("dbo.Addresses");
        }
    }
}
