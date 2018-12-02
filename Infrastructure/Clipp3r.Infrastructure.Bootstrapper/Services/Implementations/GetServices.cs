using Autofac;

namespace Clipp3r.Core.Common
{
    class GetServices : IGetServices
    {
        private readonly ILifetimeScope lifetimeScope;

        public GetServices(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public TService GetRequiredService<TService>()
        {
            return lifetimeScope.Resolve<TService>();
        }
    }
}
