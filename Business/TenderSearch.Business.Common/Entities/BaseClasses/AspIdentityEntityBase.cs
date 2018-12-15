using TenderSearch.Contracts;
using Eml.EntityBaseClasses;
using System.ComponentModel.DataAnnotations;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public class AspIdentityEntityBase : EntityBase<string>, IAspIdentityEntity
    {
        [Key]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
