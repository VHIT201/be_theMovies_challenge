using System.Data;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Domain.Entities;
using Common.Application.CustomAttributes;


namespace Repositories
{
    [ScopedService]
    public class FavoriteFilmRepository(IDbConnection connection) : SimpleCrudRepository<favoriteFilm, Guid>(connection)
    {
        public async Task<List<favoriteFilm>> GetAllfavoriteFilm()
        {
            var sql = SqlCommandHelper.GetSelectSql<favoriteFilm>();
            var result = await connection.QueryAsync<favoriteFilm>(sql);
            return result.ToList();
        }
        public async Task<favoriteFilm?> GetByIDfavoriteFilm(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<favoriteFilm>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<favoriteFilm> UpdatefavoriteFilmAsyncRP(favoriteFilm favoriteFilm)
        {
            return await UpdateAsync(favoriteFilm);
        }
        public async Task DeletefavoriteFilmAsyncRP(favoriteFilm favoriteFilm)
        {
            await DeleteAsync(favoriteFilm);
        }
        public async Task<List<favoriteFilm>> GetByUserID(Guid id)
        {
            var param = new { userid = id };
            var sql = "SELECT * FROM favoriteFilm WHERE userid = @userid"; // Truy vấn để lấy tất cả bản ghi có userid khớp với id

            var result = await connection.QueryAsync<favoriteFilm>(sql, param); // Sử dụng QueryAsync để lấy danh sách các bản ghi
            return result.ToList();
        }

        
    }
}