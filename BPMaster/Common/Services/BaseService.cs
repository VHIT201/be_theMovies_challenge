using AutoMapper;
using Common.Loggers.Interfaces;
using Kpmg.Blue.Common.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services
{
	public abstract class BaseService(IServiceProvider services)
    {
        protected ILogManager _logger = services.GetRequiredService<ILogManager>();
        protected IServiceProvider _services = services;
        protected IMapper _mapper = services.GetRequiredService<IMapper>();
        protected IUnitsOfWork _unitsOfWork = services.GetRequiredService<IUnitsOfWork>();
    }
}
