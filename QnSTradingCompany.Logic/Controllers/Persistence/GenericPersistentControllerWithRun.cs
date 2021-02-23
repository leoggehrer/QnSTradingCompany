//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Helpers;
using Microsoft.EntityFrameworkCore;
using QnSTradingCompany.Contracts.Client;
using QnSTradingCompany.Logic.DataContext;
using QnSTradingCompany.Logic.Modules.Exception;
using QnSTradingCompany.Logic.Modules.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence
{
    /// <inheritdoc cref="IControllerAccess{T}"/>
    /// <summary>
    /// This generic class implements the base properties and operations defined in the interface. 
    /// </summary>
    /// <typeparam name="E">The entity type of element in the controller.</typeparam>
    /// <typeparam name="I">The interface type which implements the entity.</typeparam>
    [Authorize]
    internal abstract partial class GenericPersistenceControllerWithRun<I, E> : GenericController<I, E>
        where I : Contracts.IIdentifiable
        where E : Entities.IdentityEntity, I, Contracts.ICopyable<I>, new()
    {
        static GenericPersistenceControllerWithRun()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public override bool IsTransient => false;
        public override int MaxPageSize => 500;

        internal IQueryable<E> QueryableSet() => Context.QueryableSet<I, E>();

        protected GenericPersistenceControllerWithRun(IContext context)
            : base(context)
        {
            Constructing();
            Constructed();
        }
        protected GenericPersistenceControllerWithRun(ControllerObject controllerObject)
            : base(controllerObject)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        #region Async-Methods
        public override async Task<int> CountAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteCountAsync().ConfigureAwait(false);
        }
        internal virtual Task<int> ExecuteCountAsync()
        {
            return Context.CountAsync<I, E>();
        }
        public override async Task<int> CountByAsync(string predicat)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteCountByAsync(predicat).ConfigureAwait(false);
        }
        internal virtual Task<int> ExecuteCountByAsync(string predicate)
        {
            return Context.CountByAsync<I, E>(predicate);
        }

        public override async Task<I> GetByIdAsync(int id)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteGetByIdAsync(id).ConfigureAwait(false);
        }
        internal virtual async Task<I> ExecuteGetByIdAsync(int id)
        {
            var entity = await Task.Run(() => QueryableSet().SingleOrDefault(i => i.Id == id)).ConfigureAwait(false);

            if (entity == null)
                throw new LogicException(ErrorType.InvalidId);

            return await BeforeReturnAsync(entity).ConfigureAwait(false);
        }
        internal virtual async Task<E> ExecuteGetEntityByIdAsync(int id)
        {
            var entity = await QueryableSet().SingleOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

            if (entity == null)
                throw new LogicException(ErrorType.InvalidId);

            return await BeforeReturnAsync(entity).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteGetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<I>> ExecuteGetPageListAsync(int pageIndex, int pageSize)
        {
            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            var qryResult = await QueryableSet().Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToArrayAsync()
                                                .ConfigureAwait(false);

            return await BeforeReturnAsync(qryResult).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> GetAllAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteGetAllAsync().ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<I>> ExecuteGetAllAsync()
        {
            var qryResult = await Task.Run(() => QueryableSet().ToArray())
                                      .ConfigureAwait(false);

            return await BeforeReturnAsync(qryResult).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteQueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<I>> ExecuteQueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            var qryResult = await Task.Run(() => QueryableSet().Where(predicate)
                                                               .Skip(pageIndex * pageSize)
                                                               .Take(pageSize))
                                      .ConfigureAwait(false);

            return await BeforeReturnAsync(qryResult).ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<I>> ExecuteQueryPageListAsync(Expression<Func<E, bool>> predicate, int pageIndex, int pageSize)
        {
            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            var query = await Task.Run(() => QueryableSet().Where(predicate)
                                                           .Skip(pageIndex * pageSize)
                                                           .Take(pageSize))
                                  .ConfigureAwait(false);

            return await BeforeReturnAsync(query).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> QueryAllAsync(string predicate)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Query).ConfigureAwait(false);

            return await ExecuteQueryAllAsync(predicate).ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<I>> ExecuteQueryAllAsync(string predicate)
        {
            int idx = 0, qryDelta = 0;
            var qryResult = new List<E>();

            do
            {
                var qry = await Task.Run(() => QueryableSet().Where(predicate)
                                                             .Skip(idx++ * MaxPageSize)
                                                             .Take(MaxPageSize))
                                    .ConfigureAwait(false);

                qryResult.AddRange(qry);
                qryDelta = qryResult.Count - qryDelta;
            } while (qryDelta == MaxPageSize);

            return await BeforeReturnAsync(qryResult).ConfigureAwait(false);
        }
        internal virtual async Task<IEnumerable<E>> ExecuteQueryAllEntitiesAsync(Expression<Func<E, bool>> predicate)
        {
            int idx = 0, qryDelta = 0;
            var qryResult = new List<E>();

            do
            {
                var qry = await Task.Run(() => QueryableSet().Where(predicate)
                                                             .Skip(idx++ * MaxPageSize)
                                                             .Take(MaxPageSize))
                                    .ConfigureAwait(false);

                qryResult.AddRange(qry);
                qryDelta = qryResult.Count - qryDelta;
            } while (qryDelta == MaxPageSize);

            return await BeforeReturnAsync(qryResult.AsQueryable()).ConfigureAwait(false);
        }

        public override async Task<I> CreateAsync()
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Create).ConfigureAwait(false);

            return await ExecuteCreateAsync().ConfigureAwait(false);
        }
        internal virtual async Task<I> ExecuteCreateAsync()
        {
            E entity = new E();

            AfterCreate(entity);
            return await BeforeReturnAsync(entity).ConfigureAwait(false);
        }
        internal virtual async Task<E> ExecuteCreateEntityAsync()
        {
            E entity = new E();

            AfterCreate(entity);
            return await BeforeReturnAsync(entity).ConfigureAwait(false);
        }
        protected virtual void AfterCreate(E entity)
        {
        }

        protected virtual Task BeforeInsertingUpdateingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        protected virtual Task BeforeInsertingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public override async Task<I> InsertAsync(I entity)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Insert).ConfigureAwait(false);

            return await ExecuteInsertAsync(ConvertTo(entity)).ConfigureAwait(false);
        }
        internal override async Task<E> InsertAsync(E entity)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Insert).ConfigureAwait(false);

            return await ExecuteInsertEntityAsync(entity).ConfigureAwait(false);
        }
        internal virtual async Task<I> ExecuteInsertAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeInsertingAsync(entity).ConfigureAwait(false);
            var result = await Context.InsertAsync<I, E>(entity).ConfigureAwait(false);
            await AfterInsertedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal virtual async Task<E> ExecuteInsertEntityAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeInsertingAsync(entity).ConfigureAwait(false);
            var result = await Context.InsertAsync<I, E>(entity).ConfigureAwait(false);
            await AfterInsertedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return result;
        }
        protected virtual Task AfterInsertedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeUpdatingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public override async Task<I> UpdateAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Update).ConfigureAwait(false);

            var entityModel = await QueryableSet().SingleOrDefaultAsync(i => i.Id == entity.Id).ConfigureAwait(false);

            if (entityModel != null)
            {
                entityModel.CopyProperties(entity);
                return await ExecuteUpdateAsync(entityModel).ConfigureAwait(false);
            }
            else
                throw new LogicException(ErrorType.InvalidId);
        }
        internal override async Task<E> UpdateAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Update).ConfigureAwait(false);

            return await ExecuteUpdateEntityAsync(entity).ConfigureAwait(false);
        }
        internal virtual async Task<I> ExecuteUpdateAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeUpdatingAsync(entity).ConfigureAwait(false);
            var result = await Context.UpdateAsync<I, E>(entity).ConfigureAwait(false);
            await AfterUpdatedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal virtual async Task<E> ExecuteUpdateEntityAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeUpdatingAsync(entity).ConfigureAwait(false);
            var result = await Context.UpdateAsync<I, E>(entity).ConfigureAwait(false);
            await AfterUpdatedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return result;
        }
        protected virtual Task AfterUpdatedAsync(E entity)
        {
            return Task.FromResult(0);
        }
        protected virtual Task AfterInsertedUpdatedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeDeletingAsync(int id)
        {
            return Task.FromResult(0);
        }
        public override async Task DeleteAsync(int id)
        {
            await CheckAuthorizationAsync(GetType(), MethodBase.GetCurrentMethod(), AccessType.Delete).ConfigureAwait(false);

            await ExecuteDeleteAsync(id).ConfigureAwait(false);
        }
        internal async Task ExecuteDeleteAsync(int id)
        {
            await BeforeDeletingAsync(id).ConfigureAwait(false);
            var entity = await Context.DeleteAsync<I, E>(id).ConfigureAwait(false);

            if (entity != null)
            {
                await AfterDeletedAsync(entity).ConfigureAwait(false);
            }
        }
        protected virtual Task AfterDeletedAsync(E entity)
        {
            return Task.FromResult(0);
        }
        #endregion Async-Methods

        #region Invoke handler
        public override Task InvokeActionAsync(string name, params object[] parameters)
        {
            return InvokeHelper.InvokeActionAsync(this, name, parameters);
        }
        public override Task<object> InvokeFunctionAsync(string name, params object[] parameters)
        {
            return InvokeHelper.InvokeFunctionAsync(this, name, parameters);
        }
        #endregion Invoke handler

        #region Internal-Methods
        internal virtual Task<E> ExecuteEntityByIdAsync(int id)
        {
            return QueryableSet().SingleOrDefaultAsync(i => i.Id == id);
        }
        internal virtual Task<E[]> ExecuteQueryEntitiesAsync(Expression<Func<E, bool>> predicate)
        {
            return Task.Run(() => QueryableSet().Where(predicate)
                                                .ToArray());
        }
        internal virtual Task<E[]> ExecuteQueryEntities(string predicate, int pageIndex, int pageSize)
        {
            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            return Task.Run(() => QueryableSet().Where(predicate)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToArray());
        }

        //internal virtual async Task<IEnumerable<I>> QueryAsyncFacade(Expression<Func<I, bool>> predicate)
        //{
        //    var newPredicate = ExpressionConverter.ConvertToObject<I, bool, E, bool>(predicate);

        //    return await QueryAsync(newPredicate).ToArrayAsync().ConfigureAwait(false);
        //}
        #endregion Internal-Methods
    }
}
//MdEnd
