using System;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface ISaveChangesToRepository
    {
        event Func<Task> SaveChangesEvent;

        Task<int> SaveChangesAsync(bool publishSaveChangesEvent = true);
    }
}
