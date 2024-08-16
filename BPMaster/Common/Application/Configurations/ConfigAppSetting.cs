using Common.Application.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Configurations
{
    public static class ConfigAppSetting
    {
        public static (T, IConfiguration) LoadSetting<T>(IServiceCollection services, IWebHostEnvironment _environment) where T : BaseAppSetting
        {
            var configuration = CreateConfigurationBuilder(_environment).Build();
            var type = typeof(T);
            T setting = Activator.CreateInstance(type) as T ?? throw new Exception($"Cannot configure app setting with type {nameof(type)}");
                    
            return (setting, configuration);
        }

        private static IConfigurationBuilder CreateConfigurationBuilder(IWebHostEnvironment env, bool isLoadSystemEnvironment = false, string settingFileName = "appsettings{0}.json")
        {
            var genericSettingFileName = string.Format(settingFileName, "");
            var environmentSettingFileName = string.Format(settingFileName, $".{env.EnvironmentName}");
            var config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(genericSettingFileName,
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile(environmentSettingFileName,
                    optional: true);
            if (isLoadSystemEnvironment)
            {
                config.AddEnvironmentVariables();
            }

            return config;
        }

        public static T LoadToObject<T>(IConfiguration configuration, string propertyName) where T : class
        {
            return configuration.GetSection(propertyName).Get<T>() ?? throw new Exception($" Cannot map configuration section {propertyName} to Application Setting");
        }

        public static T? LoadToObjectAllowNull<T>(IConfiguration configuration, string propertyName) where T : class
        {
            return configuration.GetSection(propertyName).Get<T>();
        }
    }
}
