using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Configurations
{
    public static class ConfigApiVersion
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });          
        }
    }
}
