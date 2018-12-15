using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class CityMunicipality : EntityWithNameBase
    {
        [Required]
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }

        [MaxLength(6)]
        [Required]
        public string ZipCode { get; set; }

        public virtual Province Province { get; set; }

        public virtual List<Barangay> Barangays { get; set; }
    }
}