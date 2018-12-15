using System.ComponentModel.DataAnnotations;
using Eml.Contracts.Attributes;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public abstract class EntityWithNameBase : EntityBase
    {
        [SortOrder(0)]
        [MaxLength(255)]
        [Required]
        public virtual string Name { get; set; }
    }
}
