using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Clipp3r.Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clipp3r.Infrastructure.Bootstrapper
{
    public class ApplicationBootstrap
    {
        private readonly ServicesContainer servicesContainer;

        public ApplicationBootstrap()
        {
            servicesContainer = new ServicesContainer();

            servicesContainer.RegisterServices<IGetServicesScope, ServicesContainer>(servicesContainer, ServiceLifetime.Singleton);

            servicesContainer.RegisterServices(Assembly.Load("Clipp3r.Core.DomainLogic"));
            new DomainLogicSetup().Setup(servicesContainer);
            servicesContainer.RegisterServices(Assembly.Load("Clipp3r.Infrastructure.EntityFramework"));
            new EntityFrameworkSetup().Setup(servicesContainer);
            servicesContainer.RegisterServices(Assembly.Load("Clipp3r.Infrastructure.Bootstrapper"));
            servicesContainer.RegisterServices(Assembly.Load("Clipp3r.Consumers.WpfClient"));

            servicesContainer.Build();
        }

        public IGetServicesScope GetServicesScope()
        {
            return servicesContainer;
        }
    }
}
