using Clipp3r.Core.Common;
using Clipp3r.Infrastructure.Bootstrapper;
using Clipp3r.ViewModels;

namespace Clipp3r.Consumers.WpfClient
{
    class ServiceLocator
    {
        private readonly IGetServicesScope getServicesScope;

        public ServiceLocator()
        {
            ApplicationBootstrap applicationBootstrap = new ApplicationBootstrap();

            getServicesScope = applicationBootstrap.GetServicesScope();
        }

        public MainWindowViewModel MainWindowViewModel { get { return getServicesScope.BeginScope().GetRequiredService<MainWindowViewModel>(); } }
    }
}
