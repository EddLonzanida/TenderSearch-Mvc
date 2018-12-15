using TenderSearch.Business.Common.Entities.BaseClasses;
using Eml.Contracts.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TenderSearch.Business.Common.Entities
{
    public class Dependent : PersonBase
    {
        [Display(Name = "Employee")]
        [Required]
        public int? EmployeeId { get; set; }

        [SortOrder(3)]
        [Display(Name = "Dependent Name")]
        [MaxLength(50)]
        public override string DisplayName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
