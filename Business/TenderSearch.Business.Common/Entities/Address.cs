using TenderSearch.Business.Common.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenderSearch.Business.Common.Entities
{
    public class Address : EntityWithAddressBase
    {
        [Index("IX_UniqueAll", 4, IsUnique = true)]
        [Display(Name = "Owner Name")]
        [Required]
        public int? OwnerId { get; set; }

        /// <summary>
        /// Employee, Dependent, Education, Office, Business
        /// </summary>
        [Index("IX_UniqueAll", 5, IsUnique = true)]
        [Display(Name = "Owner Type")]
        [Required]
        [MaxLength(10)]
        public string OwnerType { get; set; }

        [Index("IX_UniqueAll", 6, IsUnique = true)]
        [Display(Name = "Owner")]
        [NotMapped]
        public string OwnerDisplayName { get; set; }
    }
}
