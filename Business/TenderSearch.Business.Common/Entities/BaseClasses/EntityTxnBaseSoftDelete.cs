using Eml.Contracts.Entities;
using Eml.EntityBaseClasses;

namespace TenderSearch.Business.Common.Entities.BaseClasses
{
    public abstract class EntityTxnBaseSoftDelete<T> : EntityTxnBaseSoftDeleteInt<T>
        where T : IEntityBase<int>
    {
    }
}
