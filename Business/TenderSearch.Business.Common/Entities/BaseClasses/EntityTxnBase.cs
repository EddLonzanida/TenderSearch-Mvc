using Eml.Contracts.Entities;
using Eml.EntityBaseClasses;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public abstract class EntityTxnBase<T> : EntityTxnBaseInt<T>
        where T : IEntityBase<int>
    {
    }
}
