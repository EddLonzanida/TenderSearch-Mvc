using System.Collections.Generic;
using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class Region : EntityWithNameBase
    {
        public virtual List<Province> Provinces { get; set; }
    }
}
