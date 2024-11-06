
using System.Security.Principal;
using Common.Application.Models;
using Common.Mappers.AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace AutoMappers
{
    public class FavoriteProfile : BaseProfile
    {
        protected override void CreateMaps()
        {
            CreateMap<FavoriteFilmDto, favoriteFilm>();

            CreateMap<favoriteFilm, FavoriteFilmDto>();
        }
    }
}
