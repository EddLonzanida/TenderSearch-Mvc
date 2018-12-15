using TenderSearch.Business.Common.Entities.BaseClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TenderSearch.Data.Security
{
    public class AspNetUser : AspIdentityEntityBase
    {
        [NotMapped]
        public bool HasRole => AspNetUserRoles.Count > 0;

        public virtual List<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

        public string RolesAsText
        {
            get
            {
                if (!AspNetUserRoles.Any()) return string.Empty;

                var aRoles = AspNetUserRoles.ConvertAll(r => r.RoleName).ToArray();

                return string.Join("; ", aRoles);
            }
        }
    }
}