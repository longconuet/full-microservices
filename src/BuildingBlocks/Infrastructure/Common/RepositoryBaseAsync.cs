using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : RepositoryQueryBaseAsync<T, K, TContext>, IRepositoryBaseAsync<T, K, TContext> 
        where T : EntityBase<K>
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext context, IUnitOfWork<TContext> unitOfWork) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<K> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
            return entities.Select(x => x.Id).ToList();
        }

        public async Task UpdateAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Unchanged) return;

            T exist = _context.Set<T>().Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);

            await SaveChangesAsync();
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IList<K> CreateList(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities.Select(x => x.Id).ToList();
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Unchanged) return;

            T exist = _context.Set<T>().Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);

        }

        public void UpdateList(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteList(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
