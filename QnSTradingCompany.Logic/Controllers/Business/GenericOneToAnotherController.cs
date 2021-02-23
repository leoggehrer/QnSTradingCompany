//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Helpers;
using QnSTradingCompany.Logic.Modules.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Business
{
    internal abstract partial class GenericOneToAnotherController<I, E, TOne, TOneEntity, TAnother, TAnotherEntity> : GenericController<I, E>
        where I : Contracts.IOneToAnother<TOne, TAnother>
        where E : Entities.OneToAnotherEntity<TOne, TOneEntity, TAnother, TAnotherEntity>, I, Contracts.ICopyable<I>, new()
        where TOne : Contracts.IIdentifiable, Contracts.ICopyable<TOne>
        where TOneEntity : Entities.IdentityEntity, TOne, Contracts.ICopyable<TOne>, new()
        where TAnother : Contracts.IIdentifiable, Contracts.ICopyable<TAnother>
        where TAnotherEntity : Entities.IdentityEntity, TAnother, Contracts.ICopyable<TAnother>, new()
    {
        static GenericOneToAnotherController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        protected abstract GenericController<TOne, TOneEntity> OneEntityController { get; set; }
        protected abstract GenericController<TAnother, TAnotherEntity> AnotherEntityController { get; set; }

        public override bool IsTransient => true;
        public override int MaxPageSize => OneEntityController.MaxPageSize;

        public GenericOneToAnotherController(DataContext.IContext context) : base(context)
        {
            Constructing();
            ChangedSessionToken += GenericOneToAnotherController_ChangedSessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public GenericOneToAnotherController(ControllerObject controller) : base(controller)
        {
            Constructing();
            ChangedSessionToken += GenericOneToAnotherController_ChangedSessionToken;
            Constructed();
        }

        protected virtual void GenericOneToAnotherController_ChangedSessionToken(object sender, EventArgs e)
        {
            OneEntityController.SessionToken = SessionToken;
            AnotherEntityController.SessionToken = SessionToken;
        }

        #region Async-Methods
        public override Task<int> CountAsync()
        {
            return OneEntityController.CountAsync();
        }
        public override Task<int> CountByAsync(string predicate)
        {
            return OneEntityController.CountByAsync(predicate);
        }

        protected virtual PropertyInfo GetNavigationToOne()
        {
            return typeof(TAnotherEntity).GetProperty(typeof(TOneEntity).Name);
        }
        protected virtual PropertyInfo GetForeignKeyToOne()
        {
            return typeof(TAnother).GetProperty($"{typeof(TOneEntity).Name}Id");
        }
        protected virtual async Task LoadAnotherAsync(E entity, int masterId)
        {
            var predicate = $"{typeof(TOneEntity).Name}Id == {masterId}";
            var qyr = await AnotherEntityController.QueryAllAsync(predicate).ConfigureAwait(false);

            if (qyr.Any())
            {
                if (AnotherEntityController.IsTransient)
                {
                    var another = await AnotherEntityController.GetByIdAsync(qyr.ElementAt(0).Id).ConfigureAwait(false);

                    entity.AnotherEntity.CopyProperties(another);
                }
                else
                {
                    entity.AnotherEntity.CopyProperties(qyr.ElementAt(0));
                }
            }
            else
            {
                entity.AnotherEntity.CopyProperties(new TAnotherEntity());
            }
        }
        protected virtual async Task<IEnumerable<TAnotherEntity>> QueryDetailsAsync(int masterId)
        {
            var result = new List<TAnotherEntity>();
            var predicate = $"{typeof(TOneEntity).Name}Id == {masterId}";
            var query = await AnotherEntityController.QueryAllAsync(predicate).ConfigureAwait(false);

            foreach (var item in query)
            {
                var e = new TAnotherEntity();

                e.CopyProperties(item);
                result.Add(e);
            }
            return result;
        }

        public override async Task<I> GetByIdAsync(int id)
        {
            E result;
            var oneEntity = await OneEntityController.GetByIdAsync(id).ConfigureAwait(false);

            if (oneEntity != null)
            {
                result = new E();
                result.OneItem.CopyProperties(oneEntity);
                await LoadAnotherAsync(result, oneEntity.Id).ConfigureAwait(false);
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            var result = new List<E>();
            var query = await OneEntityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.OneItem.CopyProperties(item);
                await LoadAnotherAsync(entity, item.Id).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> GetAllAsync()
        {
            var result = new List<E>();
            var query = await OneEntityController.GetAllAsync().ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.OneItem.CopyProperties(item);
                await LoadAnotherAsync(entity, item.Id).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            var result = new List<E>();
            var query = await OneEntityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.OneItem.CopyProperties(item);
                await LoadAnotherAsync(entity, item.Id).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> QueryAllAsync(string predicate)
        {
            var result = new List<E>();
            var query = await OneEntityController.QueryAllAsync(predicate).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.OneItem.CopyProperties(item);
                await LoadAnotherAsync(entity, item.Id).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<I> CreateAsync()
        {
            E entity = new E();

            AfterCreate(entity);
            return await BeforeReturnAsync(entity).ConfigureAwait(false);
        }
        protected virtual void AfterCreate(E entity)
        {
        }

        public override async Task<I> InsertAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.OneItem.CheckArgument(nameof(entity.OneItem));
            entity.AnotherItem.CheckArgument(nameof(entity.AnotherItem));

            var result = new E();

            result.OneEntity.CopyProperties(entity.OneItem);
            await OneEntityController.InsertAsync(result.OneEntity).ConfigureAwait(false);

            result.AnotherEntity.CopyProperties(entity.AnotherItem);
            var pi = GetNavigationToOne();

            if (pi != null)
            {
                pi.SetValue(result.AnotherEntity, result.OneEntity);
            }
            await AnotherEntityController.InsertAsync(result.AnotherEntity).ConfigureAwait(false);
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal override Task<E> InsertAsync(E entity)
        {
            throw new NotImplementedException();
        }
        public override async Task<I> UpdateAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.OneItem.CheckArgument(nameof(entity.OneItem));
            entity.AnotherItem.CheckArgument(nameof(entity.AnotherItem));

            var result = new E();
            var updOne = await OneEntityController.UpdateAsync(entity.OneItem).ConfigureAwait(false);

            result.OneEntity.CopyProperties(updOne);
            if (entity.AnotherItem.Id == 0)
            {
                var pi = GetForeignKeyToOne();

                if (pi != null)
                {
                    pi.SetValue(entity.AnotherItem, result.OneEntity.Id);
                }
                var insAnother = await AnotherEntityController.InsertAsync(entity.AnotherItem).ConfigureAwait(false);

                result.AnotherEntity.CopyProperties(insAnother);
            }
            else
            {
                var updAnother = await AnotherEntityController.UpdateAsync(entity.AnotherItem).ConfigureAwait(false);

                result.AnotherEntity.CopyProperties(updAnother);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        internal override Task<E> UpdateAsync(E entity)
        {
            throw new NotImplementedException();
        }
        public override async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id).ConfigureAwait(false);

            if (entity != null)
            {
                if (entity.AnotherItem.Id > 0)
                {
                    await AnotherEntityController.DeleteAsync(entity.AnotherItem.Id).ConfigureAwait(false);
                }
                await OneEntityController.DeleteAsync(entity.Id).ConfigureAwait(false);
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId);
            }
        }
        #endregion Async-Methods

        #region Invoke handler
        public override Task InvokeActionAsync(string name, params object[] parameters)
        {
            var helper = new InvokeHelper();

            return InvokeHelper.InvokeActionAsync(this, name, parameters);
        }
        public override Task<object> InvokeFunctionAsync(string name, params object[] parameters)
        {
            var helper = new InvokeHelper();

            return InvokeHelper.InvokeFunctionAsync(this, name, parameters);
        }
        #endregion Invoke handler

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                OneEntityController.Dispose();
                AnotherEntityController.Dispose();

                OneEntityController = null;
                AnotherEntityController = null;
            }
        }
    }
}
//MdEnd
