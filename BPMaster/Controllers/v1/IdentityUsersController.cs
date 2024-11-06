using Application.Settings;
using Common.Controllers;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers.v1
{
    public class IdentityUsersController(IServiceProvider services) : BaseV1Controller<IdentityUserService, ApplicationSetting>(services)
    {
        [HttpGet("test")]
        public Task<IActionResult> TestAsync()
        {
            return Task.FromResult(Success("Test"));
        }
        /// <summary>
        /// This API is for registering a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto dto)
        {
            return CreatedSuccess(await _service.RegisterUserAsync(dto));
        }
        /// <summary>
        /// This API is for authenticating a user
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginUserDto dto)
        {
            return Success(await _service.AuthenticateAsync(dto));
        }
        /// <summary>
        /// This API is for registering a new user
        /// </summary>
        [HttpGet("Information")]
        public async Task<IActionResult> GetInformationByUsername(string username)
        {
            return Success(await _service.Getinformation(username));
        }
    }
}
