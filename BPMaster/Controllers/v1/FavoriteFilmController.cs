using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;
using Domain.Entities;

namespace BPMaster.Controllers.v1
{
    public class FavoriteFilmController(IServiceProvider service) : BaseV1Controller<FavoriteService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Service
        /// </summary>
        [HttpGet("FavoriteFilmall")]
        public async Task<IActionResult> GetFavoriteFilm()
        {
            var FavoriteFilm = await _service.GetAllFavoriteFilm();
            return Success(FavoriteFilm);
        }
        /// <summary>
        /// this is api get by id Service
        /// </summary>
        [HttpGet("getFavoriteFilmbyid")]
        public async Task<IActionResult> GetFavoriteFilmById(Guid id)
        {
            return Success(await _service.GetByIDFavoriteFilm(id));
        }
        /// <summary>
        /// this is api create a new Service
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateFavoriteFilm([FromBody] FavoriteFilmDto dto)
        {
            return CreatedSuccess(await _service.CreatefavoriteAsync(dto));
        }
        /// <summary>
        /// this is api update Service
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] FavoriteFilmDto dto)
        {
            return Success(await _service.UpdateFavoriteFilmAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Service
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFavoriteFilm(Guid id)
        {
            await _service.DeleteFavoriteFilmAsync(id);
            return Success("delete Success");
        }
    }
}