using TenderSearch.Business.Common.Entities;
using TenderSearch.Data;
using Eml.DataRepository;
using Eml.DataRepository.Extensions;
using Eml.Extensions;
using System.Data.Entity.Migrations;
using TenderSearch.Web.Infrastructure;

namespace TenderSearch.DataMigration.Seeders
{
    public static class AddressSeeder
    {
        public static void Seed<T>(T context)
            where T : TenderSearchDb
        {
#if DEBUG
            var entityName = typeof(Address).Name.Pluralize();

            Seeder.Execute(entityName, () =>
            {
                context.Addresses.AddOrUpdate(r => r.Id,
                    new Address { Id = 1, BarangayId = 1, OwnerId = 1, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress1", PhoneNumber = "123" },
                    new Address { Id = 2, BarangayId = 2, OwnerId = 1, AddressType = eAddressType.Office.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress2", PhoneNumber = "456" },
                    new Address { Id = 3, BarangayId = 3, OwnerId = 1, AddressType = eAddressType.Business.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress3", PhoneNumber = "789" },
                    new Address { Id = 4, BarangayId = 4, OwnerId = 2, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress1", PhoneNumber = "123" },
                    new Address { Id = 5, BarangayId = 5, OwnerId = 2, AddressType = eAddressType.Office.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress2", PhoneNumber = "456" },
                    new Address { Id = 6, BarangayId = 6, OwnerId = 2, AddressType = eAddressType.Business.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress3", PhoneNumber = "789" },
                    new Address { Id = 7, BarangayId = 7, OwnerId = 3, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress1", PhoneNumber = "123" },
                    new Address { Id = 8, BarangayId = 8, OwnerId = 3, AddressType = eAddressType.Office.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress2", PhoneNumber = "456" },
                    new Address { Id = 9, BarangayId = 9, OwnerId = 3, AddressType = eAddressType.Business.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress3", PhoneNumber = "789" },
                    new Address { Id = 10, BarangayId = 10, OwnerId = 7, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress1", PhoneNumber = "123" },
                    new Address { Id = 11, BarangayId = 11, OwnerId = 7, AddressType = eAddressType.Office.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress2", PhoneNumber = "456" },
                    new Address { Id = 12, BarangayId = 12, OwnerId = 7, AddressType = eAddressType.Business.ToString(), OwnerType = eAddressOwnerType.Employee.ToString(), StreetAddress = "MailingAddress3", PhoneNumber = "789" },
                    new Address { Id = 13, BarangayId = 1, OwnerId = 1, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 14, BarangayId = 2, OwnerId = 2, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 15, BarangayId = 3, OwnerId = 3, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 16, BarangayId = 4, OwnerId = 4, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 17, BarangayId = 5, OwnerId = 5, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 18, BarangayId = 6, OwnerId = 6, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 19, BarangayId = 7, OwnerId = 7, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 20, BarangayId = 8, OwnerId = 5, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 21, BarangayId = 9, OwnerId = 9, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 22, BarangayId = 10, OwnerId = 10, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 23, BarangayId = 11, OwnerId = 11, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 24, BarangayId = 12, OwnerId = 12, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 25, BarangayId = 11, OwnerId = 13, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 26, BarangayId = 12, OwnerId = 14, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" },
                    new Address { Id = 27, BarangayId = 8, OwnerId = 15, AddressType = eAddressType.Home.ToString(), OwnerType = eAddressOwnerType.Dependent.ToString(), StreetAddress = "Dependent's MailingAddress", PhoneNumber = "123" }

                    );

                context.DoSave(entityName);
            });
#endif
        }
    }
}
