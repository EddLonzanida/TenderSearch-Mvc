using Eml.Contracts.Entities;
using Eml.DataRepository.Contracts;

namespace TenderSearch.Data.Contracts
{
    public interface IDataRepositoryInt<T> : IDataRepositoryInt<T, TenderSearchDb>
        where T : class, IEntityBase<int>
    {
    }

}
