using Clipp3r.Core.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Clipp3r.Core.DomainLogic
{
    public class DomainLogicSetup
    {
        public void Setup(IRegisterServices registerServices)
        {
            registerServices.RegisterGenericServices(typeof(IGenericPersistenceHandler<>), typeof(GenericPersistenceHandler<>), ServiceLifetime.Scoped);
        }
    }
}
