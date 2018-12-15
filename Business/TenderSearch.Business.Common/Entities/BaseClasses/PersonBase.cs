using System;
using System.ComponentModel.DataAnnotations;
using Eml.Contracts.Attributes;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public abstract class PersonBase : EntityBase
    {
        [SortOrder(1)]
        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [SortOrder(0)]
        [Display(Name = "First Name")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [SortOrder(2)]
        [Display(Name = "Middle Name")]
        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        public virtual string DisplayName { get; set; }

        [SortOrder(4)]
        [Required]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [SortOrder(5)]
        [Required]
        [MaxLength(2)]
        public string Gender { get; set; }

        [Display(Name = "Civil Status")]
        [Required]
        [MaxLength(8)]
        public string CivilStatus { get; set; }
    }
}
