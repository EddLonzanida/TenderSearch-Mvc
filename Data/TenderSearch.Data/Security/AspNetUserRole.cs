using Eml.EntityBaseClasses;
using System.ComponentModel.DataAnnotations;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Data.Security
{
    public class AspNetUserRole : AspIdentityEntityBase
    {
        [Required]
        [Display(Name = "User")]
        public string AspNetUserId { get; set; }

        public string RoleName { get; set; }

        public string OldRoleName { get; set; }

        //public virtual AspNetUser AspNetUser { get; set; }
    }
}