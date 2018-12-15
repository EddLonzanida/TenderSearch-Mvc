using System.Collections.Generic;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data.Security;
using Eml.Asp.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TenderSearch.DataMigration.Seeders
{
    public static class UserRolesSeeder
    {
        public static List<string> Roles { get; } = new List<string>
        {
            eUserRoles.Admins.ToString(),
            eUserRoles.UserManagers.ToString(),
            eUserRoles.Users.ToString()
        };

        public static void Seed<T>(T context)
            where T : Data.TenderSearchDb, new()
        {
#if DEBUG
            using (var identityHelper = new IdentityHelper<ApplicationUser, IdentityRole, T>())
            {
                var roles = Roles.ConvertAll(r => new IdentityRole(r));

                identityHelper.CreateRoles(roles);
            }
#endif
        }
    }
}