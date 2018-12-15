using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eml.Contracts.Attributes;
using Eml.Contracts.Entities;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public abstract class EntityWithAddressBase : EntityBase
    {
        [Index("IX_UniqueAll", 1, IsUnique = true)]
        [Display(Name = "Barangay")]
        [Required]
        public int? BarangayId { get; set; }

        [Display(Name = "Contact Phone")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [SortOrder(0)]
        [Index("IX_UniqueAll", 2, IsUnique = true)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Street Address")]
        [MaxLength(256)]
        [Required]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Home, Office, Business
        /// </summary>
        [Index("IX_UniqueAll", 3, IsUnique = true)]
        [Display(Name = "Address Type")]
        [MaxLength(10)]
        [Required]
        public string AddressType { get; set; }

        public virtual Barangay Barangay { get; set; }
    }

    public abstract class EntityWithAddressBase<T> : EntityTxnBase<T>
        where T : IEntityBase<int>
    {
        [Display(Name = "Barangay")]
        [Required]
        public int? BarangayId { get; set; }

        [Display(Name = "Contact Phone")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [SortOrder(0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Street Address")]
        [MaxLength(256)]
        [Required]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Home, Office, Business
        /// </summary>
        [Display(Name = "Address Type")]
        [Required]
        public int AddressType { get; set; }

        public virtual Barangay Barangay { get; set; }
    }
}
