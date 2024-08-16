using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Configurations
{
    public static class ConfigWebApi
    {
        public static void Config<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(IServiceCollection services, string xmlPath)
                     where TImplementation : BaseConfigureSwaggerOptions
        {
            services.AddControllers();

            //Add Swagger
            ConfigSwagger.Config<TImplementation>(services, xmlPath);

            //Add API version
            ConfigApiVersion.Config(services);

            //Config URL lower case
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }
    }
}
