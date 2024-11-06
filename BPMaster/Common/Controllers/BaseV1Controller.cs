using System.Net;
using Asp.Versioning;
using AutoMapper;
using Common.Application.Models;
using Common.Application.Settings;
using Common.Loggers.Interfaces;
using Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseV1Controller<T, S>(IServiceProvider services) : ControllerBase where S: BaseAppSetting where T : class
    {
        protected readonly T _service = services.GetRequiredService<T>();
        protected ILogManager _logger = services.GetRequiredService<ILogManager>();
        protected IMapper _mapper = services.GetRequiredService<IMapper>();

        protected AuthenticatedUserModel? _currentUser = services.GetRequiredService<AuthUserService<S>>().GetUser();

        protected IActionResult Success(object result)
        {
            return Ok(ResponseModel.Success(result));
        }

        protected IActionResult CreatedSuccess(object result)
        {
            return Created(Request.Path, ResponseModel.Success(result, HttpStatusCode.Created));
        }
    }
}
