using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Application.Middlewares;
using Common.Application.Settings;
using Common.Loggers.Interfaces;
using Common.Loggers.SeriLog;
using Common.Mappers.AutoMapper;
using Common.Security;
using Common.Services;
using Kpmg.Blue.Common.Services;
using Kpmg.Blue.Common.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Common.Application.Configurations
{
    public abstract class BaseAppConfiguration<S> where S : BaseAppSetting
    {
		protected List<Type> _middlewareList = new()
		{
			typeof(GlobalExceptionHandlerMiddleware)
		};
        protected readonly IWebHostEnvironment _environment;

		protected abstract void LoadAdditionalSetting(S setting, IConfiguration configuration);

		protected abstract void ConfigBackgroundServices(IServiceCollection services);

        public BaseAppConfiguration(IWebHostEnvironment? environment)
        {
            _environment = environment ?? throw new Exception("Cannot get evironment of application....");

        }

        public virtual void ConfigApi<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(IServiceCollection services, string xmlPath)
					where TImplementation : BaseConfigureSwaggerOptions
		{
			ConfigWebApi.Config<TImplementation>(services, xmlPath);
		}

		public virtual void ConfigApp(WebApplication app)
		{
			//Swagger setting
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					var descriptions = app.DescribeApiVersions();

					// Build a swagger endpoint for each discovered API version
					foreach (var description in descriptions)
					{
						var url = $"/swagger/{description.GroupName}/swagger.json";
						var name = description.GroupName.ToUpperInvariant();
						options.SwaggerEndpoint(url, name);
					}
				});
			}
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

			//Add middlewares
			foreach (var middleware in _middlewareList)
			{
				app.UseMiddleware(middleware);
			}
		}

		public virtual void ConfigServices(IServiceCollection services, Assembly assembly, BaseAppSetting setting)
        {
			//Generic services need to inject
			services.AddSingleton<ILogManager>(new LogManager(_environment.EnvironmentName));
			services.AddScoped<IUnitsOfWork, UnitsOfWork>();
            services.AddScoped<AuthUserService<S>>();


            //Add Authentication
            services.AddAuthentication("Jwt").AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler<S>>("Jwt", null);

            //Auto configure by service attribute
            ConfigService.RegisterByServiceAttribute(services, assembly);

			//Configure background services
			ConfigBackgroundServices(services);

            //Configure client
            //services.AddHttpClient();

            //Configure validation error response
            services.PostConfigure<ApiBehaviorOptions>(options =>
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var message = string.Join(",", actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
					throw new ValidationException(message);
					//return new BadRequestObjectResult(ResponseModel.Error(message, System.Net.HttpStatusCode.BadRequest, "ValidationFailed"));
				}
			);

			//Configure AutoMapper
			var mapperConfig = new MapperConfiguration(mc =>
			{
				var profileTypes = assembly.GetTypes().Where(x =>x.IsSubclassOf(typeof(BaseProfile)));
				foreach (var profileType in profileTypes)
				{
					Profile? profile = (Profile?)Activator.CreateInstance(profileType);
					if (profile != null)
					{
						mc.AddProfile(profile);
					}
				}
			});
			services.AddSingleton(mapperConfig.CreateMapper());
		}

		public virtual S ConfigSettings(IServiceCollection services) {
			var (setting, configuration) = ConfigAppSetting.LoadSetting<S>(services, _environment);
            
			setting.JwtTokenSetting = ConfigAppSetting.LoadToObject<JwtTokenSetting>(configuration, "JwtTokenSetting");
            setting.FolderGenerateSqlScript = ConfigAppSetting.LoadToObjectAllowNull<string>(configuration, "FolderGenerateSqlScript");
			setting.ExternalServicesSetting = ConfigAppSetting.LoadToObjectAllowNull<ExternalServicesSetting>(configuration, "ExternalServicesSetting");

            //Add more specific setting here
            LoadAdditionalSetting(setting, configuration);

            services.AddSingleton(setting);

			return setting;
        }
    }
}
