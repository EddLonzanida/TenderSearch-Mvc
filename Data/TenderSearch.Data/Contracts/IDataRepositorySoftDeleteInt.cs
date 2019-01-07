using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace TenderSearch.Data.Contracts
{
    public interface IDataRepositorySoftDeleteInt<T> : IDataRepositorySoftDeleteInt<T, TenderSearchDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
    }
}
