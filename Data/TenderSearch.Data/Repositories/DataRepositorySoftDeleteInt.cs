using Eml.Contracts.Entities;
using Eml.DataRepository;
using System.ComponentModel.Composition;

namespace TenderSearch.Data.Repositories
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositorySoftDeleteInt<T> : DataRepositorySoftDeleteInt<T, TenderSearchDb>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
    }

    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryInt<T> : DataRepositoryInt<T, TenderSearchDb>
        where T : class, IEntityBase<int>
    {
    }
}