using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class Province : EntityWithNameBase
    {
        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual List<CityMunicipality> Municipalities { get; set; }
    }
}