//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QnSTradingCompany.Adapters.Controller
{
    internal partial class GenericControllerAdapter<TContract> : Contracts.Client.IAdapterAccess<TContract>
        where TContract : Contracts.IIdentifiable
    {
        static GenericControllerAdapter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public Contracts.Client.IControllerAccess<TContract> controller;

        public GenericControllerAdapter()
        {
            Constructing();
            controller = Logic.Factory.Create<TContract>();
            Constructed();
        }
        public GenericControllerAdapter(string sessionToken)
        {
            Constructing();
            controller = Logic.Factory.Create<TContract>(sessionToken);
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        public string SessionToken { set => controller.SessionToken = value; }
        public int MaxPageSize => controller.MaxPageSize;

        #region Async-Methods
        public Task<int> CountAsync()
        {
            return controller.CountAsync();
        }
        public Task<int> CountByAsync(string predicate)
        {
            return controller.CountByAsync(predicate);
        }

        public Task<TContract> GetByIdAsync(int id)
        {
            return controller.GetByIdAsync(id);
        }
        public async Task<IEnumerable<TContract>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return await controller.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false);
        }
        public async Task<IEnumerable<TContract>> GetAllAsync()
        {
            return (await controller.GetAllAsync().ConfigureAwait(false)).ToArray();
        }

        public async Task<IEnumerable<TContract>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return (await controller.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToArray();
        }
        public async Task<IEnumerable<TContract>> QueryAllAsync(string predicate)
        {
            return (await controller.QueryAllAsync(predicate).ConfigureAwait(false)).ToArray();
        }

        public Task<TContract> CreateAsync()
        {
            return controller.CreateAsync();
        }

        public async Task<TContract> InsertAsync(TContract entity)
        {
            var result = await controller.InsertAsync(entity).ConfigureAwait(false);

            await SaveChangesAsync().ConfigureAwait(false);
            if (controller.IsTransient)
            {
                result = await GetByIdAsync(result.Id).ConfigureAwait(false);
            }
            return result;
        }
        public async Task<IQueryable<TContract>> InsertAsync(IEnumerable<TContract> entities)
        {
            var result = new List<TContract>();

            foreach (var item in entities)
            {
                result.Add(await controller.InsertAsync(item).ConfigureAwait(false));
            }
            await SaveChangesAsync().ConfigureAwait(false);
            if (controller.IsTransient)
            {
                foreach (var item in result.Eject())
                {
                    result.Add(await controller.GetByIdAsync(item.Id).ConfigureAwait(false));
                }
            }
            return result.AsQueryable();
        }
        public async Task<TContract> UpdateAsync(TContract entity)
        {
            var result = await controller.UpdateAsync(entity).ConfigureAwait(false);

            await SaveChangesAsync().ConfigureAwait(false);
            if (controller.IsTransient)
            {
                result = await GetByIdAsync(result.Id).ConfigureAwait(false);
            }
            return result;
        }
        public async Task<IQueryable<TContract>> UpdateAsync(IEnumerable<TContract> entities)
        {
            var result = new List<TContract>();

            foreach (var item in entities)
            {
                result.Add(await controller.UpdateAsync(item).ConfigureAwait(false));
            }
            await SaveChangesAsync().ConfigureAwait(false);
            if (controller.IsTransient)
            {
                foreach (var item in result.Eject())
                {
                    result.Add(await controller.GetByIdAsync(item.Id).ConfigureAwait(false));
                }
            }
            return result.AsQueryable();
        }

        public async Task DeleteAsync(int id)
        {
            await controller.DeleteAsync(id).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await controller.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await controller.RejectChangesAsync().ConfigureAwait(false);
                throw;
            }
        }

        public Task InvokeActionAsync(string name, params object[] parameters)
        {
            return controller.InvokeActionAsync(name, parameters);
        }

        public async Task<TResult> InvokeFunctionAsync<TResult>(string name, params object[] parameters)
        {
            var result = default(TResult);
            var invokeResult = await controller.InvokeFunctionAsync(name, parameters).ConfigureAwait(false);

            if (invokeResult != null)
            {
                var json = JsonSerializer.Serialize(invokeResult);

                result = JsonSerializer.Deserialize<TResult>(json);
            }
            return result;
        }
        #endregion Async-Methods

        public void Dispose()
        {
            controller?.Dispose();
            controller = null;
        }
    }
}
//MdEnd
