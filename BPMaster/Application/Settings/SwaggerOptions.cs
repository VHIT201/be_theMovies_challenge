using Asp.Versioning.ApiExplorer;
using Common.Application.Configurations;
using Microsoft.OpenApi.Models;

namespace Application.Settings
{
    public class SwaggerOptions(IApiVersionDescriptionProvider provider) : BaseConfigureSwaggerOptions(provider)
    {
        protected override OpenApiInfo GetApiInfo(ApiVersionDescription description)
        {
            var version = description.ApiVersion.ToString();
            return new OpenApiInfo
            {
                Title = "BP Master Service API Document",
                Version = version,
                Description = $"List of public APIs- Version {version} in BP Master Service",
                Contact = new OpenApiContact { Name = "Author name", Email = "developer@meu.com" },
                License = new OpenApiLicense { Name = "LSP License", Url = new Uri("http://localhost") }
            };
        }
    }
}
