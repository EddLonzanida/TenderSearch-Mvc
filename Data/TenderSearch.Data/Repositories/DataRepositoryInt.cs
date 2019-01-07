using System.ComponentModel.Composition;
using Eml.Contracts.Entities;
using Eml.DataRepository;
using TenderSearch.Data.Contracts;

namespace TenderSearch.Data.Repositories
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IDataRepositoryInt<>))]
    public class DataRepositoryInt<T> : DataRepositoryInt<T, TenderSearchDb>, IDataRepositoryInt<T>
        where T : class, IEntityBase<int>
    {
    }
}