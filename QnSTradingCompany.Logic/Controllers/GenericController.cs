//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.Contracts.Client;
using QnSTradingCompany.Logic.DataContext;
using QnSTradingCompany.Logic.Modules.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers
{
    /// <inheritdoc cref="IControllerAccess{T}"/>
    /// <summary>
    /// This generic class implements the base properties and operations defined in the interface. 
    /// </summary>
    /// <typeparam name="E">The entity type of element in the controller.</typeparam>
    /// <typeparam name="I">The interface type which implements the entity.</typeparam>
    [Authorize]
    internal abstract partial class GenericController<I, E> : ControllerObject, IControllerAccess<I>
        where I : Contracts.IIdentifiable
        where E : Entities.IdentityEntity, I, Contracts.ICopyable<I>, new()
    {
        static GenericController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public abstract bool IsTransient { get; }
        public abstract int MaxPageSize { get; }

        protected GenericController(IContext context)
            : base(context)
        {
            Constructing();
            Constructed();
        }
        protected GenericController(ControllerObject controllerObject)
            : base(controllerObject)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        protected virtual E ConvertTo(I contract)
        {
            contract.CheckArgument(nameof(contract));

            E result = new E();

            result.CopyProperties(contract);
            return result;
        }
        protected virtual IQueryable<E> ConvertTo(IQueryable<I> contracts)
        {
            contracts.CheckArgument(nameof(contracts));

            List<E> result = new List<E>();

            foreach (var item in contracts)
            {
                result.Add(ConvertTo(item));
            }
            return result.AsQueryable();
        }

        #region Before-Returns
        protected virtual Task<E> BeforeReturnAsync(E entity) => Task.FromResult<E>(entity);
        protected virtual async Task<IEnumerable<E>> BeforeReturnAsync(IEnumerable<E> entities)
        {
            var result = new List<E>();

            foreach (var item in entities)
            {
                result.Add(await BeforeReturnAsync(item).ConfigureAwait(false));
            }
            return result;
        }
        #endregion Before-Returns

        #region Async-Methods
        public abstract Task<int> CountAsync();
        public abstract Task<int> CountByAsync(string predicat);

        public abstract Task<I> GetByIdAsync(int id);

        public abstract Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize);
        public abstract Task<IEnumerable<I>> GetAllAsync();

        public abstract Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize);
        public abstract Task<IEnumerable<I>> QueryAllAsync(string predicate);

        public abstract Task<I> CreateAsync();

        public abstract Task<I> InsertAsync(I entity);
        public virtual async Task<IEnumerable<I>> InsertAsync(IEnumerable<I> entities)
        {
            entities.CheckArgument(nameof(entities));

            List<I> result = new List<I>();

            foreach (var entity in entities)
            {
                result.Add(await InsertAsync(entity).ConfigureAwait(false));
            }
            return result.AsQueryable();
        }
        internal abstract Task<E> InsertAsync(E entity);

        public abstract Task<I> UpdateAsync(I entity);
        public virtual async Task<IEnumerable<I>> UpdateAsync(IEnumerable<I> entities)
        {
            entities.CheckArgument(nameof(entities));

            List<I> result = new List<I>();

            foreach (var entity in entities)
            {
                result.Add(await UpdateAsync(entity).ConfigureAwait(false));
            }
            return result.AsQueryable();
        }
        internal abstract Task<E> UpdateAsync(E entity);

        public abstract Task DeleteAsync(int id);
        #endregion Async-Methods

        #region Invoke handler
        public abstract Task InvokeActionAsync(string name, params object[] parameters);
        public abstract Task<object> InvokeFunctionAsync(string name, params object[] parameters);
        #endregion Invoke handler
    }
}
//MdEnd
