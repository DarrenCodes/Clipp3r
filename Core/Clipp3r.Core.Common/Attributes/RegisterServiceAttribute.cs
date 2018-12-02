using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clipp3r.Core.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterServiceAttribute : Attribute
    {
        public Type ServiceType { get; }
        public ServiceLifetime ServiceLifetime { get; }

        public RegisterServiceAttribute(Type serviceType = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceType = serviceType;
            ServiceLifetime = serviceLifetime;
        }
    }
}
