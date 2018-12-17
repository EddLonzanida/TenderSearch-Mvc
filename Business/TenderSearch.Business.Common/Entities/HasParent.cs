using TenderSearch.Business.Common.Entities.BaseClasses;

namespace TenderSearch.Business.Common.Entities
{
    public class HasParent : EntityWithNameBase
    {
        public int ParentId { get; set; }
        public string Description { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
