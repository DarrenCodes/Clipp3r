using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Clipp3r.Infrastructure.EntityFramework
{
    public class EntityFrameworkSetup
    {
        public void Setup(IRegisterServices registerServices)
        {
            registerServices.RegisterGenericServices( typeof(IManageRespository<>), typeof(GenericRespository<>), ServiceLifetime.Scoped);
        }
    }
}
