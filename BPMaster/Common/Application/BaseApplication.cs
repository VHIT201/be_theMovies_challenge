using System.Data;
using System.IO;
using System.Reflection;
using Common.Application.Configurations;
using Common.Application.Settings;
using Common.Databases;
using Common.Loggers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application
{
    public abstract class BaseApplication<Config, Setting, Swagger> where Config : BaseAppConfiguration<Setting> where Setting : BaseAppSetting where Swagger : BaseConfigureSwaggerOptions
    {
        protected readonly WebApplicationBuilder _builder;
        protected Config _appConfig;

        protected string _xmlPath;

        protected  Assembly _assembly;

        protected abstract Config GetConfiguration();

        protected abstract void GenerateSqlScripts(Setting setting);
        protected abstract void AdditionalExecute(Setting setting);

        public BaseApplication(WebApplicationBuilder? builder, string xmlPath, Assembly assembly) 
        {
            _builder = builder ?? throw new Exception("Cannot setup web application Builder");
            _xmlPath = xmlPath;
            _assembly = assembly;
            _appConfig = GetConfiguration();

        }     

        public void Start()
        {
            var services = _builder.Services;
        
            //1. Configure App Settings
            var appSetting = _appConfig.ConfigSettings(services);

            //2. Configure services
            _appConfig.ConfigServices(services, _assembly, appSetting);

            //3.Setup Api configuration/documentation
            _appConfig.ConfigApi<Swagger>(services, _xmlPath);

            var sp = services.BuildServiceProvider();
            var logger = sp.GetRequiredService<ILogManager>();

            //4. Setup DB Connection, if setting
            if (appSetting.DatabaseSetting != null)
            {              
                services.AddTransient<IDbConnection>(sp =>
                {
                    IDbConnection connection = DbConnectionFactory.GetConnection(appSetting.DatabaseSetting);
                    return connection;
                });
            }

            //5. Generate SQL script, if setting
            if (!string.IsNullOrEmpty(appSetting.FolderGenerateSqlScript))
            {
                if (Directory.Exists(appSetting.FolderGenerateSqlScript))
                {
                    GenerateSqlScripts(appSetting);
                }
                else
                {
                    var fullPath = Path.GetFullPath(appSetting.FolderGenerateSqlScript);
                    logger.Error($"Missing folder {fullPath} to generate sql script");
                }
            }

            //6. Additional setting
            AdditionalExecute(appSetting);

            //7. Start application
            RunApp();

        }

        private void RunApp()
        {
            var app = _builder.Build();
            _appConfig.ConfigApp(app);
            app.Run();
        }

    }
}
