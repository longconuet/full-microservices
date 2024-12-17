using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contracts.Common.Interfaces
{
    public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryAsync<T, K>
        where T : EntityBase<K>
    {
        void Create(T entity);
        Task<K> CreateAsync(T entity);
        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);
        IList<K> CreateList(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void Update(T entity);
        Task UpdateListAsync(IEnumerable<T> entities);
        void UpdateList(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        void Delete(T entity);
        Task DeleteListAsync(IEnumerable<T> entities);
        void DeleteList(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task EndTransactionAsync();
        Task RollbackTransactionAsync();
    }

    public interface IRepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K>
        where T : EntityBase<K>
        where TContext : DbContext
    {
    }
}
