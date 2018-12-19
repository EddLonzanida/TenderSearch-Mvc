using TenderSearch.Business.Common.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eml.Contracts.Entities;

namespace TenderSearch.Business.Common.Entities
{
    public class Lookup : EntityBase, ILookupTable
    {
        [Index("IX_UniqueAll", 1, IsUnique = true)]
        [MaxLength(50)]
        [Required]
        public string Group { get; set; }

        [Index("IX_UniqueAll", 2, IsUnique = true)]
        [MaxLength(50)]
        public string SubGroup { get; set; }

        [Index("IX_UniqueAll", 3, IsUnique = true)]
        [Required]
        public int? Value { get; set; }

        [Index("IX_UniqueAll", 4, IsUnique = true)]
        [MaxLength(250)]
        [Required]
        public string Text { get; set; }
    }
}
