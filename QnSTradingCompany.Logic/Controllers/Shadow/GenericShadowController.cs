//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Shadow
{
    internal abstract partial class GenericShadowController<I, E, TSourceContract, TSourceEntity> : GenericController<I, E>
        where I : Contracts.IIdentifiable
        where E : Entities.ShadowEntity, I, Contracts.ICopyable<I>, new()
        where TSourceContract : Contracts.IIdentifiable, Contracts.ICopyable<TSourceContract>
        where TSourceEntity : Entities.IdentityEntity, TSourceContract, Contracts.ICopyable<TSourceContract>, new()
    {
        static GenericShadowController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        protected abstract GenericController<TSourceContract, TSourceEntity> SourceEntityController { get; set; }

        public override bool IsTransient => false;
        public override int MaxPageSize => SourceEntityController.MaxPageSize;

        public GenericShadowController(DataContext.IContext context) : base(context)
        {
            Constructing();
            ChangedSessionToken += GenericViewController_ChangedSessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public GenericShadowController(ControllerObject controller) : base(controller)
        {
            Constructing();
            ChangedSessionToken += GenericViewController_ChangedSessionToken;
            Constructed();
        }
        protected virtual void GenericViewController_ChangedSessionToken(object sender, EventArgs e)
        {
            SourceEntityController.SessionToken = SessionToken;
        }

        protected virtual E ConvertTo(TSourceContract contract)
        {
            contract.CheckArgument(nameof(contract));

            E result = new E();

            result.CopyFrom(contract);
            return result;
        }
        protected virtual IEnumerable<E> ConvertTo(IEnumerable<TSourceContract> contracts)
        {
            contracts.CheckArgument(nameof(contracts));

            List<E> result = new List<E>();

            foreach (var item in contracts)
            {
                result.Add(ConvertTo(item));
            }
            return result;
        }

        #region Async-Methods
        public override Task<int> CountAsync()
        {
            return SourceEntityController.CountAsync();
        }
        public override Task<int> CountByAsync(string predicate)
        {
            return SourceEntityController.CountByAsync(predicate);
        }

        public override async Task<I> GetByIdAsync(int id)
        {
            var entity = await SourceEntityController.GetByIdAsync(id).ConfigureAwait(false);
            var result = ConvertTo(entity);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            var entities = await SourceEntityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);
            var result = ConvertTo(entities);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> GetAllAsync()
        {
            var entities = await SourceEntityController.GetAllAsync().ConfigureAwait(false);
            var result = ConvertTo(entities);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            var entities = await SourceEntityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false);
            var result = ConvertTo(entities);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> QueryAllAsync(string predicate)
        {
            var entities = await SourceEntityController.QueryAllAsync(predicate).ConfigureAwait(false);
            var result = ConvertTo(entities);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<I> CreateAsync()
        {
            var entity = await SourceEntityController.CreateAsync().ConfigureAwait(false);
            var result = ConvertTo(entity);

            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<I> InsertAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));

            var sourceEntity = await SourceEntityController.CreateAsync().ConfigureAwait(false);

            sourceEntity.CopyFrom(entity);
            sourceEntity = await SourceEntityController.InsertAsync(sourceEntity).ConfigureAwait(false);

            var result = ConvertTo(sourceEntity);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal override async Task<E> InsertAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            var sourceEntity = new TSourceEntity();

            sourceEntity.CopyFrom(entity);
            sourceEntity = await SourceEntityController.InsertAsync(sourceEntity).ConfigureAwait(false);

            var result = ConvertTo(sourceEntity);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<I> UpdateAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));

            var sourceEntity = await SourceEntityController.GetByIdAsync(entity.Id).ConfigureAwait(false);

            sourceEntity.CopyFrom(entity);
            sourceEntity = await SourceEntityController.UpdateAsync(sourceEntity).ConfigureAwait(false);

            var result = ConvertTo(sourceEntity);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal override async Task<E> UpdateAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            var sourceEntity = new TSourceEntity();
            sourceEntity.CopyFrom(entity);
            var result = await SourceEntityController.UpdateAsync(sourceEntity).ConfigureAwait(false);

            return ConvertTo(result);
        }

        public override Task DeleteAsync(int id)
        {
            return SourceEntityController.DeleteAsync(id);
        }

        public override Task SaveChangesAsync()
        {
            return SourceEntityController.SaveChangesAsync(); ;
        }
        public override Task RejectChangesAsync()
        {
            return SourceEntityController.RejectChangesAsync();
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                SourceEntityController.Dispose();

                SourceEntityController = null;
            }
        }
    }
}
//MdEnd
