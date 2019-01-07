using Eml.Contracts.Entities;
using Eml.DataRepository;
using System.ComponentModel.Composition;
using TenderSearch.Data.Contracts;

namespace TenderSearch.Data.Repositories
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IDataRepositorySoftDeleteInt<>))]
    public class DataRepositorySoftDeleteInt<T> : DataRepositorySoftDeleteInt<T, TenderSearchDb>, IDataRepositorySoftDeleteInt<T>
        where T : class, IEntityBase<int>, IEntitySoftdeletableBase
    {
    }
}