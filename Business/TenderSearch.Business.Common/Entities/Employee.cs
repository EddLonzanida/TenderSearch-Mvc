using TenderSearch.Business.Common.Entities.BaseClasses;
using Eml.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenderSearch.Business.Common.Entities
{
    public class Employee : PersonBase
    {
        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        //[Index("IX_EmployeeNumberAndDisplayName", 1, IsUnique = true)]
        [Display(Name = "Employee Number")]
        [MaxLength(50)]
        [Required]
        public string EmployeeNumber { get; set; }

        //[Index("IX_EmployeeNumberAndDisplayName", 2, IsUnique = true)]
        [SortOrder(3)]
        [Display(Name = "Employee Name")]
        [MaxLength(50)]
        public override string DisplayName { get; set; }

        [Display(Name = "ATM Number")]
        [MaxLength(15)]
        public string AtmNumber { get; set; }

        [Display(Name = "SSS Number")]
        [MaxLength(15)]
        public string SssNumber { get; set; }

        [Display(Name = "TIN Number")]
        [MaxLength(15)]
        public string TinNumber { get; set; }

        [Display(Name = "Philhealth Number")]
        [MaxLength(15)]
        public string PhilHealthNumber { get; set; }

        [Display(Name = "Pag-Ibig Number")]
        [MaxLength(15)]
        public string PagIbigNumber { get; set; }

        [Display(Name = "Passport Number")]
        [MaxLength(15)]
        public string PassportNumber { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? MembershipDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Date Hired")]
        public DateTime? HiredDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentEndDate { get; set; }

        [Display(Name = "Rank Type")]
        [Required]
        public int RankTypeId { get; set; }

        [Display(Name = "Category Type")]
        [Required]
        public int CategoryTypeId { get; set; }

        /// <summary>
        /// Active, Pension, Survivor
        /// </summary>
        [Display(Name = "StatusType")]
        [Required]
        public int StatusTypeId { get; set; }

        [Display(Name = "GroupArea")]
        public int GroupAreaId { get; set; }

        //public virtual List<EmployeeEducation> EmployeeEducations { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<Dependent> Dependents { get; set; }
    }
}
