﻿using Eml.Contracts.Entities;
using Eml.ControllerBase.Mvc.Contracts;
using Eml.ControllerBase.Mvc.ViewModels.LayoutContents;
using Eml.DataRepository.Contracts;
using Eml.Logger;
using Eml.Mediator.Contracts;

namespace TenderSearch.Web.Controllers.BaseClasses
{
    /// <inheritdoc />
    public abstract class CrudControllerForIndexDetailsDelete<T, TRepository, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerBaseInt<T, TRepository, LayoutContentsCreateEditViewModel<int, T>, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TRepository : class, IDataRepositoryBase<int, T>
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        protected CrudControllerForIndexDetailsDelete(TRepository repository, ILogger logger)
            : base(repository, logger)
        {
        }

        protected CrudControllerForIndexDetailsDelete(IMediator mediator, TRepository repository, ILogger logger)
            : base(mediator, repository, logger)
        {
        }

        protected override LayoutContentsCreateEditViewModel<int, T> GetLayoutContentsViewModelForCreateEdit(T item, string title1, string title2, string title3, int pageSize, int labelClassColumnCount, int parentId, string param)
        {
            var contentsVm = new LayoutContentsCreateEditViewModel<int, T>(item, title1, title2, title3, pageSize, parentId: parentId);

            return contentsVm;
        }
    }

    /// <inheritdoc />
    public abstract class CrudControllerForIndexDetailsDelete<T, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        : CrudControllerForIndexDetailsDelete<T, IDataRepositoryBase<int, T>, TLayoutContentsIndexViewModel, TLayoutContentsDetailsDeleteViewModel>
        where T : class, IEntityBase<int>, new()
        where TLayoutContentsIndexViewModel : class, ILayoutContentsIndexViewModel<int, T>
        where TLayoutContentsDetailsDeleteViewModel : class, ILayoutContentsDetailsDeleteViewModel<int, T>
    {
        protected CrudControllerForIndexDetailsDelete(IDataRepositoryBase<int, T> repository, ILogger logger) : base(repository, logger)
        {
        }

        protected CrudControllerForIndexDetailsDelete(IMediator mediator, IDataRepositoryBase<int, T> repository, ILogger logger) : base(mediator, repository, logger)
        {
        }
    }
}