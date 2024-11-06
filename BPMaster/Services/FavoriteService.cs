using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using Common.Application.Exceptions;
using Domain.Entities;
using Domain.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class FavoriteService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly FavoriteFilmRepository _ServiceRepository = new(connection);

        public async Task<List<favoriteFilm>> GetAllFavoriteFilm()
        {
            return await _ServiceRepository.GetAllfavoriteFilm();
        }

        public async Task<List<favoriteFilm>> GetByIDFavoriteFilm(Guid favoriteFilmId)
        {
            var favoriteFilm = await _ServiceRepository.GetByUserID(favoriteFilmId);

            if (favoriteFilm == null)
            {
                throw new NonAuthenticateException();
            }
            return favoriteFilm;
        }

        public async Task<favoriteFilm> CreatefavoriteAsync(FavoriteFilmDto dto)
        {
            var favoriteFilm = _mapper.Map<favoriteFilm>(dto);

            favoriteFilm.Id = Guid.NewGuid();

            await _ServiceRepository.CreateAsync(favoriteFilm);

            return favoriteFilm;
        }
        public async Task<favoriteFilm> UpdateFavoriteFilmAsync(Guid id, FavoriteFilmDto dto)
        {
            var existingService = await _ServiceRepository.GetByIDfavoriteFilm(id);

            if (existingService == null)
            {
                throw new Exception("Service not found !");
            }
            var Service = _mapper.Map(dto, existingService);

            await _ServiceRepository.UpdateAsync(Service);

            return Service;
        }
        public async Task DeleteFavoriteFilmAsync(Guid id)
        {
            var Service = await _ServiceRepository.GetByIDfavoriteFilm(id);
            if (Service == null)
            {
                throw new Exception("Service not found !");
            }
            await _ServiceRepository.DeleteAsync(Service);
        }

    }
}
