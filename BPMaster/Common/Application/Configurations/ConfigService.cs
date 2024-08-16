using System.Reflection;
using Common.Application.CustomAttributes;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Configurations
{
    public static class ConfigService
    {
        public static void AddSingletonService(IServiceCollection services, Type serviceType, Type? interfaceType = null)
        {
            if (interfaceType == null)
            {
                services.AddSingleton(serviceType);
            }
            else
            {
                services.AddSingleton(interfaceType, serviceType);
            }
        }

        public static void AddScopedService(IServiceCollection services, Type serviceType, Type? interfaceType = null)
        {
            if (interfaceType == null)
            {
                services.AddScoped(serviceType);
            }
            else
            {
                services.AddScoped(interfaceType, serviceType);
            }
        }

        public static void AddTransientService(IServiceCollection services, Type serviceType, Type? interfaceType = null)
        {
            if (interfaceType == null)
            {
                services.AddTransient(serviceType);
            }
            else
            {
                services.AddTransient(interfaceType, serviceType);
            }
        }
        public static void RegisterByServiceAttribute(this IServiceCollection services, Assembly assembly)
        {
            foreach (var serviceType in assembly.GetTypes())
            {
                var value = GetServiceLifeTimeValue(serviceType);
                if (value > -1)
                {
                    ServiceLifetime lifeTime = (ServiceLifetime)value;
                    var serviceInterface = GetAssociateInterface(serviceType);
                    RegisterService(services, serviceInterface, serviceType, lifeTime);
                }
            }
        }

        private static Type? GetAssociateInterface(Type serviceType)
        {
            var allInterfaces = serviceType.GetInterfaces();
            var directInterfaces = allInterfaces.Except
                        (allInterfaces.SelectMany(t => t.GetInterfaces()));
            return directInterfaces != null && directInterfaces.Any() ? directInterfaces.Last() : null;
        }

        private static int GetServiceLifeTimeValue(Type serviceType)
        {
            if (serviceType.GetCustomAttribute<ScopedServiceAttribute>() != null)
            {
                return (int)ServiceLifetime.Scoped;
            }
            if (serviceType.GetCustomAttribute<TransientServiceAttribute>() != null)
            {
                return (int)ServiceLifetime.Transient;
            }
            if (serviceType.GetCustomAttribute<SingletonServiceAttribute>() != null)
            {
                return (int)ServiceLifetime.Singleton;
            }

            return -1;
        }
        private static void RegisterService(IServiceCollection serviceCollection, Type? interfaceType, Type serviceType, ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Transient:
                    AddTransientService(serviceCollection, serviceType, interfaceType);
                    break;
                case ServiceLifetime.Singleton:
                    AddSingletonService(serviceCollection, serviceType, interfaceType);
                    break;
                case ServiceLifetime.Scoped:
                    AddScopedService(serviceCollection, serviceType, interfaceType);
                    break;
                default:
                    break;

            }
        }
    }
}
