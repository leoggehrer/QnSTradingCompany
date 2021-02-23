//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Business
{
    internal abstract partial class BusinessControllerAdapter<I, E> : GenericController<I, E>
        where I : Contracts.IIdentifiable
        where E : Entities.IdentityEntity, I, Contracts.ICopyable<I>, new()
    {
        static BusinessControllerAdapter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public override bool IsTransient => true;
        public override int MaxPageSize => throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");

        public BusinessControllerAdapter(DataContext.IContext context) : base(context)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public BusinessControllerAdapter(ControllerObject controller) : base(controller)
        {
            Constructing();
            Constructed();
        }

        public override Task<int> CountAsync()
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        public override Task<int> CountByAsync(string predicate)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        #region Async-Methods
        public override Task<I> GetByIdAsync(int id)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        public override Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        public override Task<IEnumerable<I>> GetAllAsync()
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }

        public override Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        public override Task<IEnumerable<I>> QueryAllAsync(string predicate)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }

        public override Task<I> CreateAsync()
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }

        public override Task<I> InsertAsync(I entity)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        internal override Task<E> InsertAsync(E entity)
        {
            throw new NotImplementedException();
        }

        public override Task<I> UpdateAsync(I entity)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }
        internal override Task<E> UpdateAsync(E entity)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
        }

        public override Task DeleteAsync(int id)
        {
            throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().GetAsyncOriginal()}!");
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
    }
}
//MdEnd
