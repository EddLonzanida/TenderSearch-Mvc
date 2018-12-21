﻿using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;
using System.Threading.Tasks;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc cref="CrudController&lt;T&gt;" />
    public abstract class CrudControllerWithParent<T, TParent> : CrudController<T>, IControllerWithParent<int, T>
        where T : class, IEntityBase<int>, new()
        where TParent : class, IEntityBase<int>, IEntitySoftdeletableBase, new()
    {
        public abstract override Task<T> CreateNewItemWithParent(int parentId, string param);

        public abstract override Task BeforeCreateSave(T item);

        public abstract override Task BeforeEditSave(T item);

        public abstract override Task<string> GetParentName(int parentId);

        public abstract override int GetParentId(T item);

        protected readonly IDataRepositorySoftDeleteInt<TParent> parentRepository;

        protected CrudControllerWithParent(IDataRepositoryBase<int, T> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : this(null, repository, parentRepository, logger)
        {
        }

        protected CrudControllerWithParent(IMediator mediator
            , IDataRepositoryBase<int, T> repository
            , IDataRepositorySoftDeleteInt<TParent> parentRepository
            , ILogger logger)
            : base(mediator, repository, logger)
        {
            this.parentRepository = parentRepository;
        }
    }
}