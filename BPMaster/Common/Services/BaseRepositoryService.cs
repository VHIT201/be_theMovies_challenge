using System.Data.Common;
using Common.Domains.Entities;
using Common.Repositories;

namespace Common.Services
{
    public abstract class BaseRepositoryService<T, R>(IServiceProvider services) : BaseService(services) where T : BaseEntity<Guid> where R : SimpleCrudRepository<T, Guid>
    {
        protected abstract R Repository { get; }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await Repository.CreateAsync(entity);
            return entity;
        }
        public virtual async Task DeleteAsync(T entity)
        {
            await Repository.DeleteAsync(entity);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }
    }
}
