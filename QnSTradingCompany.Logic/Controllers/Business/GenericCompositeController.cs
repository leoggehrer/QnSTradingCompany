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
    internal abstract partial class GenericCompositeController<I, E, TConnector, TConnectorEntity, TOne, TOneEntity, TAnother, TAnotherEntity> : GenericController<I, E>
        where I : Contracts.IComposite<TConnector, TOne, TAnother>
        where E : Entities.CompositeEntity<TConnector, TConnectorEntity, TOne, TOneEntity, TAnother, TAnotherEntity>, I, Contracts.ICopyable<I>, new()
        where TConnector : Contracts.IIdentifiable, Contracts.ICopyable<TConnector>
        where TConnectorEntity : Entities.IdentityEntity, TConnector, Contracts.ICopyable<TConnector>, new()
        where TOne : Contracts.IIdentifiable, Contracts.ICopyable<TOne>
        where TOneEntity : Entities.IdentityEntity, TOne, Contracts.ICopyable<TOne>, new()
        where TAnother : Contracts.IIdentifiable, Contracts.ICopyable<TAnother>
        where TAnotherEntity : Entities.IdentityEntity, TAnother, Contracts.ICopyable<TAnother>, new()
    {
        static GenericCompositeController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        protected abstract GenericController<TConnector, TConnectorEntity> ConnectorEntityController { get; set; }
        protected abstract GenericController<TOne, TOneEntity> OneEntityController { get; set; }
        protected abstract GenericController<TAnother, TAnotherEntity> AnotherEntityController { get; set; }

        public override bool IsTransient => true;
        public override int MaxPageSize => OneEntityController.MaxPageSize;

        public GenericCompositeController(DataContext.IContext context) : base(context)
        {
            Constructing();
            ChangedSessionToken += HandleChangedSessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public GenericCompositeController(ControllerObject controller) : base(controller)
        {
            Constructing();
            ChangedSessionToken += HandleChangedSessionToken;
            Constructed();
        }

        private void HandleChangedSessionToken(object sender, EventArgs e)
        {
            ConnectorEntityController.SessionToken = SessionToken;
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
            return typeof(TConnectorEntity).GetProperty(typeof(TOneEntity).Name);
        }
        protected virtual PropertyInfo GetNavigationToAnother()
        {
            return typeof(TConnectorEntity).GetProperty(typeof(TAnotherEntity).Name);
        }
        protected virtual PropertyInfo GetForeignKeyToOne()
        {
            return typeof(TConnectorEntity).GetProperties().SingleOrDefault(pi => pi.Name.Equals($"{typeof(TOneEntity).Name}Id"));
        }
        protected virtual PropertyInfo GetForeignKeyToAnother()
        {
            return typeof(TConnectorEntity).GetProperties().SingleOrDefault(pi => pi.Name.Equals($"{typeof(TAnotherEntity).Name}Id"));
        }
        protected virtual async Task LoadChildsAsync(E entity)
        {
            var piOneFK = GetForeignKeyToOne();
            var piAnotherFK = GetForeignKeyToAnother();

            if (piOneFK != null)
            {
                var value = piOneFK.GetValue(entity.ConnectorEntity);

                if (value != null)
                {
                    var child = await OneEntityController.GetByIdAsync((int)Convert.ChangeType(value, piOneFK.PropertyType)).ConfigureAwait(false);

                    if (child != null)
                    {
                        entity.OneEntity.CopyProperties(child);
                    }
                }
            }
            if (piAnotherFK != null)
            {
                var value = piAnotherFK.GetValue(entity.ConnectorEntity);

                if (value != null)
                {
                    var child = await AnotherEntityController.GetByIdAsync((int)Convert.ChangeType(value, piAnotherFK.PropertyType)).ConfigureAwait(false);

                    if (child != null)
                    {
                        entity.AnotherEntity.CopyProperties(child);
                    }
                }
            }
        }

        public override async Task<I> GetByIdAsync(int id)
        {
            E result;
            var entity = await ConnectorEntityController.GetByIdAsync(id).ConfigureAwait(false);

            if (entity != null)
            {
                result = new E();
                result.ConnectorEntity.CopyProperties(entity);
                await LoadChildsAsync(result).ConfigureAwait(false);
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
            var query = await ConnectorEntityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.ConnectorEntity.CopyProperties(item);
                await LoadChildsAsync(entity).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> GetAllAsync()
        {
            var result = new List<E>();
            var query = await ConnectorEntityController.GetAllAsync().ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.ConnectorEntity.CopyProperties(item);
                await LoadChildsAsync(entity).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }

        public override async Task<IEnumerable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            var result = new List<E>();
            var query = await ConnectorEntityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.ConnectorEntity.CopyProperties(item);
                await LoadChildsAsync(entity).ConfigureAwait(false);

                result.Add(entity);
            }
            return await BeforeReturnAsync(result).ConfigureAwait(false);
        }
        public override async Task<IEnumerable<I>> QueryAllAsync(string predicate)
        {
            var result = new List<E>();
            var query = await ConnectorEntityController.QueryAllAsync(predicate).ConfigureAwait(false);

            foreach (var item in query)
            {
                E entity = new E();

                entity.ConnectorEntity.CopyProperties(item);
                await LoadChildsAsync(entity).ConfigureAwait(false);

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
            entity.ConnectorItem.CheckArgument(nameof(entity.ConnectorItem));
            entity.OneItem.CheckArgument(nameof(entity.OneItem));
            entity.AnotherItem.CheckArgument(nameof(entity.AnotherItem));

            var result = new E();

            result.OneEntity.CopyProperties(entity.OneItem);
            if (entity.OneItemIncludeSave)
            {
                if (result.OneEntity.Id == 0)
                {
                    await OneEntityController.InsertAsync(result.OneEntity).ConfigureAwait(false);

                    var piNav = GetNavigationToOne();

                    if (piNav != null)
                    {
                        piNav.SetValue(result.ConnectorEntity, result.OneEntity);
                    }
                }
                else
                {
                    await OneEntityController.UpdateAsync(result.OneEntity).ConfigureAwait(false);
                }
            }

            result.AnotherEntity.CopyProperties(entity.AnotherItem);
            if (entity.AnotherItemIncludeSave)
            {
                if (result.AnotherItem.Id == 0)
                {
                    await AnotherEntityController.InsertAsync(result.AnotherEntity).ConfigureAwait(false);

                    var piNav = GetNavigationToAnother();

                    if (piNav != null)
                    {
                        piNav.SetValue(result.ConnectorEntity, result.AnotherEntity);
                    }
                }
                else
                {
                    await AnotherEntityController.UpdateAsync(result.AnotherEntity).ConfigureAwait(false);
                }
            }
            result.ConnectorEntity.CopyProperties(entity.ConnectorItem);
            await ConnectorEntityController.InsertAsync(result.ConnectorEntity).ConfigureAwait(false);
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

            result.OneEntity.CopyProperties(entity.OneItem);
            if (entity.OneItemIncludeSave)
            {
                if (result.OneEntity.Id == 0)
                {
                    await OneEntityController.InsertAsync(result.OneEntity).ConfigureAwait(false);

                    var piNav = GetNavigationToOne();

                    if (piNav != null)
                    {
                        piNav.SetValue(result.ConnectorEntity, result.OneEntity);
                    }
                }
                else
                {
                    await OneEntityController.UpdateAsync(result.OneEntity).ConfigureAwait(false);
                }
            }

            result.AnotherEntity.CopyProperties(entity.AnotherItem);
            if (entity.AnotherItemIncludeSave)
            {
                if (result.AnotherItem.Id == 0)
                {
                    await AnotherEntityController.InsertAsync(result.AnotherItem).ConfigureAwait(false);

                    var piNav = GetNavigationToAnother();

                    if (piNav != null)
                    {
                        piNav.SetValue(result.ConnectorEntity, result.AnotherItem);
                    }
                }
                else
                {
                    await AnotherEntityController.UpdateAsync(result.AnotherItem).ConfigureAwait(false);
                }
            }
            result.ConnectorEntity.CopyProperties(entity.ConnectorItem);
            await ConnectorEntityController.UpdateAsync(result.ConnectorEntity).ConfigureAwait(false);
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
                await ConnectorEntityController.DeleteAsync(entity.Id).ConfigureAwait(false);
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
                ChangedSessionToken -= HandleChangedSessionToken;

                ConnectorEntityController.Dispose();
                OneEntityController.Dispose();
                AnotherEntityController.Dispose();

                ConnectorEntityController = null;
                OneEntityController = null;
                AnotherEntityController = null;
            }
        }
    }
}
//MdEnd
