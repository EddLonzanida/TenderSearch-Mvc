using System.ComponentModel.DataAnnotations;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class Barangay : EntityWithNameBase
    {
        [Display(Name = "Barangay")]
        public override string Name { get; set; }

        [Display(Name = "City/Municipality")]
        [Required]
        public virtual int CityMunicipalityId { get; set; }

        public virtual CityMunicipality CityMunicipality { get; set; }

        //public virtual List<TxnEmployee> TxnEmployees { get; set; }

        //public virtual List<TxnDependent> TxnDependents { get; set; }
    }
}