using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class Company : EntityWithNameBase
    {
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required]
        public string AbnCan { get; set; }

        public virtual List<Contract> Contracts { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
