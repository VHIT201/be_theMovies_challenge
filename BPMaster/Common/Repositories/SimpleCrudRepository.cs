using System.Data;
using System.Reflection;
using Common.Application.Settings;
using Common.Databases;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Common.Repositories
{
    public class SimpleCrudRepository<T, ID>(IDbConnection connection) where T : class{

		protected IDbConnection _connection = connection;
		protected string _dbTableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name ?? typeof(T).Name;

        public async Task<T> CreateAsync(T entity)
		{
			await _connection.InsertAsync(entity);
			return entity;
		}
		public async Task DeleteAsync(T entity)
		{
			await _connection.DeleteAsync(entity);
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _connection.GetAllAsync<T>();
		}
		public async Task<T> GetByIdAsync(ID id)
		{
            throw new NotImplementedException();
        }
		public async Task<IEnumerable<T>> GetListByCondition(string sql, object? param = null, object? paging = null, object? sorting = null)
		{
            throw new NotImplementedException();
        }
		public async Task<T?> GetOneByConditionAsync(string sql, object? param = null)
		{
			return await _connection.QuerySingleOrDefaultAsync<T>(sql, param);
		}
		public async Task<T> UpdateAsync(T entity)
		{
			await _connection.UpdateAsync(entity);
			return entity;
		}
	}
}
