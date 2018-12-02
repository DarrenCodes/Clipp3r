using Autofac;
using Autofac.Builder;
using Clipp3r.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Clipp3r.Infrastructure.Bootstrapper
{
    class ServicesContainer : IRegisterServices, IGetServicesScope
    {
        private readonly ContainerBuilder containerBuilder;
        private IContainer container;

        public ServicesContainer()
        {
            containerBuilder = new ContainerBuilder();
        }

        public IRegisterServices RegisterServices(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
        {
            BuildRegistration(serviceType, containerBuilder.RegisterType(implementationType), serviceLifetime);

            return this;
        }

        public IRegisterServices RegisterGenericServices(Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
        {
            BuildRegistration(serviceType, containerBuilder.RegisterGeneric(implementationType), serviceLifetime);

            return this;
        }

        public IRegisterServices RegisterServices<TService, TImplementation>(ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return RegisterServices(typeof(TService), typeof(TImplementation), serviceLifetime);
        }

        public IRegisterServices RegisterServices<TService>(Func<IGetServices, TService> implementationProvider, ServiceLifetime serviceLifetime)
            where TService : class
        {
            Func<IComponentContext, TService> implementationProviderWrapper = ccontext =>
            {
                IGetServicesScope getServicesScope = ccontext.Resolve<IGetServicesScope>();
                return implementationProvider(getServicesScope.BeginScope());
            };

            BuildRegistration(typeof(TService), containerBuilder.Register(implementationProviderWrapper), serviceLifetime);

            return this;
        }

        public IRegisterServices RegisterServices<TService, TImplementation>(TImplementation implementation, ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            BuildRegistration(typeof(TService), containerBuilder.RegisterInstance(implementation), serviceLifetime);

            return this;
        }

        public IRegisterServices RegisterServices(Assembly assembly)
        {
            foreach (Type typeToRegister in GetTypesWithRegisteredServiceAttribute(assembly))
            {
                CustomAttributeData registerServiceAttributeData = typeToRegister.CustomAttributes.First();

                Type serviceType = (Type)registerServiceAttributeData.ConstructorArguments[0].Value;

                if (serviceType == null)
                    serviceType = typeToRegister;

                ServiceLifetime serviceLifetime = (ServiceLifetime)registerServiceAttributeData.ConstructorArguments[1].Value;

                RegisterServices(serviceType, typeToRegister, serviceLifetime);
            }

            return this;
        }

        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> BuildRegistration<TLimit, TActivatorData, TRegistrationStyle>(Type serviceType,
            IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> implementation,
            ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    return implementation.As(serviceType).SingleInstance();
                case ServiceLifetime.Scoped:
                    return implementation.As(serviceType).InstancePerLifetimeScope();
                case ServiceLifetime.Transient:
                    return implementation.As(serviceType).InstancePerDependency();
            }

            return implementation;
        }

        IEnumerable<Type> GetTypesWithRegisteredServiceAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
                if (type.GetCustomAttributes<RegisterServiceAttribute>(true).Count() > 0)
                    yield return type;
        }

        public void Build()
        {
            container = containerBuilder.Build();
        }

        public IGetServices BeginScope()
        {
            return new GetServices(container.BeginLifetimeScope());
        }
    }
}
