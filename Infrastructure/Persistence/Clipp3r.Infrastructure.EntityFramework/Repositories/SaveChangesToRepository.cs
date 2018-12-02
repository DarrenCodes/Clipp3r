using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using System;
using System.Threading.Tasks;

namespace Clipp3r.Infrastructure.EntityFramework
{
    [RegisterService(typeof(ISaveChangesToRepository))]
    class SaveChangesToRepository : ISaveChangesToRepository
    {
        private readonly Clipp3rDatabaseContext context;

        public event Func<Task> SaveChangesEvent;

        public SaveChangesToRepository(Clipp3rDatabaseContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync(bool publishSaveChangesEvent = true)
        {
            int rowsAffected = await context.SaveChangesAsync();
            if (SaveChangesEvent != null && publishSaveChangesEvent)
                await SaveChangesEvent();

            return rowsAffected;
        }
    }
}
