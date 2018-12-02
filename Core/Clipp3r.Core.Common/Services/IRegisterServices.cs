using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Clipp3r.Core.Common
{
    public interface IRegisterServices
    {
        IRegisterServices RegisterServices(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime);
        IRegisterServices RegisterGenericServices(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime);
        IRegisterServices RegisterServices<TService, TImplementation>(ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService;
        IRegisterServices RegisterServices<TService>(Func<IGetServices, TService> implementationProvider, ServiceLifetime serviceLifetime)
            where TService : class;
        IRegisterServices RegisterServices<TService, TImplementation>(TImplementation implementation, ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService;
        IRegisterServices RegisterServices(Assembly assembly);
    }
}
