//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using Microsoft.AspNetCore.Mvc;
using QnSTradingCompany.Transfer.InvokeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.WebApi.Controllers
{
    public abstract class GenericController<I, M> : ApiControllerBase
        where I : Contracts.IIdentifiable
        where M : Transfer.IdentityModel, I, Contracts.ICopyable<I>, new()
    {
        protected async Task<Contracts.Client.IControllerAccess<I>> CreateControllerAsync()
        {
            var result = Logic.Factory.Create<I>();
            var sessionToken = await GetSessionTokenAsync().ConfigureAwait(false);

            if (sessionToken.HasContent())
            {
                result.SessionToken = sessionToken;
            }
            return result;
        }
        protected M ToModel(I entity)
        {
            var result = new M();

            result.CopyProperties(entity);
            return result;
        }
        protected IQueryable<M> ToModel(IEnumerable<I> entities)
        {
            var result = new List<M>();

            foreach (var item in entities)
            {
                result.Add(ToModel(item));
            }
            return result.AsQueryable();
        }

        [HttpGet("/api/[controller]/MaxPageSize")]
        public Task<int> GetMaxPageAsync()
        {
            return GetMaxPageSizeAsync();
        }
        [HttpGet("/api/[controller]/Count")]
        public Task<int> GetCountAsync()
        {
            return CountModelsAsync();
        }
        [HttpGet("/api/[controller]/Count/{predicate}")]
        public Task<int> GetCountByAsync(string predicate)
        {
            return CountModelsByAsync(predicate);
        }
        [HttpGet("/api/[controller]/{id}")]
        public Task<M> GetByIdAsync(int id)
        {
            return GetModelByIdAsync(id);
        }
        [HttpGet("/api/[controller]/{index}/{size}")]
        public Task<IEnumerable<M>> GetPageListAsync(int index, int size)
        {
            return GetModelPageListAsync(index, size);
        }
        [HttpGet("/api/[controller]/{predicate}/{index}/{size}")]
        public Task<IEnumerable<M>> QueryPageListAsync(string predicate, int index, int size)
        {
            return QueryModelPageListAsync(predicate, index, size);
        }
        [HttpGet("/api/[controller]/Create")]
        public Task<M> CreateAsync()
        {
            return CreateModelAsync();
        }

        [HttpPost("/api/[controller]")]
        public Task<M> PostAsync([FromBody] M model)
        {
            return InsertModelAsync(model);
        }
        [HttpPost("/api/[controller]/Array")]
        public Task<IQueryable<M>> PostArrayAsync(IEnumerable<M> models)
        {
            return InsertModelAsync(models);
        }

        [HttpPut("/api/[controller]")]
        public Task<M> PutAsync(M model)
        {
            return UpdateModelAsync(model);
        }
        [HttpPut("/api/[controller]/Array")]
        public Task<IQueryable<M>> PutArrayAsync(IEnumerable<M> models)
        {
            return UpdateModelAsync(models);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public Task DeleteAsync(int id)
        {
            return DeleteModelAsync(id);
        }

        [HttpPost("/api/[controller]/CallAction")]
        public Task CallActionAsync(InvokeParam invokeParam)
        {
            return InvokeActionAsync(invokeParam.MethodName, invokeParam.GetParameters());
        }
        [HttpPost("/api/[controller]/CallFunction")]
        public Task<InvokeReturnValue> CallFunctionAsync(Transfer.InvokeTypes.InvokeParam invokeParam)
        {
            return InvokeFunctionAsync(invokeParam.MethodName, invokeParam.GetParameters());
        }

        protected async Task<int> GetMaxPageSizeAsync()
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return ctrl.MaxPageSize;
        }
        protected async Task<int> CountModelsAsync()
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return await ctrl.CountAsync().ConfigureAwait(false);
        }
        protected async Task<int> CountModelsByAsync(string predicate)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return await ctrl.CountByAsync(predicate).ConfigureAwait(false);
        }

        protected async Task<M> GetModelByIdAsync(int id)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            var entity = (await ctrl.GetByIdAsync(id).ConfigureAwait(false));
            return ToModel(entity);
        }
        protected async Task<IEnumerable<M>> GetModelPageListAsync(int index, int size)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return (await ctrl.GetPageListAsync(index, size).ConfigureAwait(false)).ToList().Select(i => ToModel(i));
        }
        protected async Task<IEnumerable<M>> GetAllModelsAsync()
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return (await ctrl.GetAllAsync().ConfigureAwait(false)).ToList().Select(i => ToModel(i));
        }

        protected async Task<IEnumerable<M>> QueryModelPageListAsync(string predicate, int index, int size)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return (await ctrl.QueryPageListAsync(predicate, index, size).ConfigureAwait(false)).ToList().Select(i => ToModel(i));
        }
        protected async Task<IEnumerable<M>> QueryAllModelsAsync(string predicate)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            return (await ctrl.QueryAllAsync(predicate).ConfigureAwait(false)).ToList().Select(i => ToModel(i));
        }

        protected async Task<M> CreateModelAsync()
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            var entity = await ctrl.CreateAsync().ConfigureAwait(false);
            return ToModel(entity);
        }

        protected async Task<M> InsertModelAsync(M model)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);
            var result = await ctrl.InsertAsync(model).ConfigureAwait(false);

            await ctrl.SaveChangesAsync().ConfigureAwait(false);
            if (ctrl.IsTransient)
            {
                result = await ctrl.GetByIdAsync(result.Id).ConfigureAwait(false);
            }
            return ToModel(result);
        }
        protected async Task<IQueryable<M>> InsertModelAsync(IEnumerable<M> models)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);
            var entities = await ctrl.InsertAsync(models).ConfigureAwait(false);
            var result = new List<M>();

            await ctrl.SaveChangesAsync().ConfigureAwait(false);
            if (ctrl.IsTransient)
            {
                foreach (var item in entities)
                {
                    result.Add(ToModel(await ctrl.GetByIdAsync(item.Id).ConfigureAwait(false)));
                }
            }
            else
            {
                result.AddRange(ToModel(entities));
            }
            return result.AsQueryable();
        }
        protected async Task<M> UpdateModelAsync(M model)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);
            var result = await ctrl.UpdateAsync(model).ConfigureAwait(false);

            await ctrl.SaveChangesAsync().ConfigureAwait(false);
            if (ctrl.IsTransient)
            {
                result = await ctrl.GetByIdAsync(result.Id).ConfigureAwait(false);
            }
            return ToModel(result);
        }
        protected async Task<IQueryable<M>> UpdateModelAsync(IEnumerable<M> models)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);
            var entities = await ctrl.UpdateAsync(models).ConfigureAwait(false);
            var result = new List<M>();

            await ctrl.SaveChangesAsync().ConfigureAwait(false);
            if (ctrl.IsTransient)
            {
                foreach (var item in entities)
                {
                    result.Add(ToModel(await ctrl.GetByIdAsync(item.Id).ConfigureAwait(false)));
                }
            }
            else
            {
                result.AddRange(ToModel(entities));
            }
            return result.AsQueryable();
        }
        protected async Task DeleteModelAsync(int id)
        {
            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            await ctrl.DeleteAsync(id).ConfigureAwait(false);
            await ctrl.SaveChangesAsync().ConfigureAwait(false);
        }

        protected async Task InvokeActionAsync(string name, object[] parameters)
        {
            name.CheckArgument(nameof(name));
            parameters.CheckArgument(nameof(parameters));

            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);

            await ctrl.InvokeActionAsync(name, parameters).ConfigureAwait(false);
        }
        protected async Task<InvokeReturnValue> InvokeFunctionAsync(string name, object[] parameters)
        {
            name.CheckArgument(nameof(name));
            parameters.CheckArgument(nameof(parameters));
            static Type GetInterfaceType(Type t)
            {
                if (t != null && t.IsInterface)
                {
                    return t;
                }
                else if (t != null && t.GetInterfaces().Any())
                {
                    return t.GetInterfaces().Last();
                }
                return null;
            }

            using var ctrl = await CreateControllerAsync().ConfigureAwait(false);
            var result = new InvokeReturnValue();
            var retVal = await ctrl.InvokeFunctionAsync(name, parameters).ConfigureAwait(false);

            if (retVal != null)
            {
                Type serializeType;

                result.IsArray = retVal.GetType().IsArray;
                if (result.IsArray == false)
                {
                    serializeType = retVal.GetType();
                }
                else
                {
                    serializeType = retVal.GetType().GetElementType();
                }

                if (serializeType.FullName.StartsWith("System.") == false)
                {
                    serializeType = GetInterfaceType(serializeType);
                }

                if (serializeType != null)
                {
                    result.Type = serializeType.FullName;
                    result.JsonData = System.Text.Json.JsonSerializer.Serialize(retVal);
                }
            }
            return result;
        }
    }
}
//MdEnd
