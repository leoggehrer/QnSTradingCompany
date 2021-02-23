//@QnSCodeCopy
//MdStart
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QnSTradingCompany.Contracts;
using QnSTradingCompany.Logic.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.DataContext
{
    internal interface IContext : IDisposable
    {
        DbSet<E> ContextSet<I, E>()
            where I : IIdentifiable
            where E : IdentityEntity, I;

        IQueryable<E> QueryableSet<I, E>()
            where I : IIdentifiable
            where E : IdentityEntity, I;

        #region Async-Methods
        Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityEntity, I;

        Task<int> CountByAsync<I, E>(string predicate)
            where I : IIdentifiable
            where E : IdentityEntity, I;

        Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityEntity, ICopyable<I>, I, new();

        Task<E> InsertAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityEntity, ICopyable<I>, I, new();

        Task<E> UpdateAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityEntity, ICopyable<I>, I, new();

        Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityEntity, I;

        Task<int> SaveChangesAsync();

        Task<int> RejectChangesAsync();
        #endregion Async-Methods
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
//MdEnd
