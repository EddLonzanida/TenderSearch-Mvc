using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class HasAllOptions : EntityWithNameBase
    {
        public int ParentId { get; set; }
        public string Description { get; set; }
    }
}
