using Microsoft.EntityFrameworkCore.Design;

namespace Clipp3r.Infrastructure.EntityFramework
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Clipp3rDatabaseContext>
    {
        public Clipp3rDatabaseContext CreateDbContext(string[] args)
        {
            return new Clipp3rDatabaseContext(new DatabaseConnectionSettings());
        }
    }
}
