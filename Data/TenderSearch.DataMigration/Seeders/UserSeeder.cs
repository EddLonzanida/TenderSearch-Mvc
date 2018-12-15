using System.Collections.Generic;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data.Security;
using Eml.Asp.Identity;
using Eml.DataRepository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TenderSearch.DataMigration.Seeders
{
    public static class UserSeeder
    {
        private const string EMAIL_DOMAIN = "tendersearch.com";

        public static List<UserWithRoles> WithRoles { get; } = new List<UserWithRoles>
        {
            new UserWithRoles("Edd", new[] { eUserRoles.Admins.ToString(), eUserRoles.UserManagers.ToString(), eUserRoles.Users.ToString() }, EMAIL_DOMAIN),
            new UserWithRoles("Francis", new[] { eUserRoles.UserManagers.ToString() }, EMAIL_DOMAIN),
            new UserWithRoles("Joseph", new[] { eUserRoles.Users.ToString() }, EMAIL_DOMAIN)
        };

        public static void Seed<T>(T context)
            where T : Data.TenderSearchDb, new()
        {
#if DEBUG
            Seeder.Execute("Users", () =>
            {
                using (var identityHelper = new IdentityHelper<ApplicationUser, IdentityRole, T>())
                {
                    identityHelper.CreateUsers(WithRoles);
                }
            });
#endif
        }
    }
}