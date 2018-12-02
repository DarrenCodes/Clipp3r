using Clipp3r.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace Clipp3r.Infrastructure.EntityFramework
{
    [RegisterService]
    class Clipp3rDatabaseContext : DbContext
    {
        public Clipp3rDatabaseContext(IDatabaseConnectionSettings databaseConnectionSettings)
            : base(new DbContextOptionsBuilder<Clipp3rDatabaseContext>().UseSqlite(databaseConnectionSettings.ConnectionString).Options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VideoMetadataConfiguration());
            modelBuilder.ApplyConfiguration(new VideoMomentConfiguration());
            modelBuilder.ApplyConfiguration(new VideoMomentCaptureConfiguration());
        }
    }
}
