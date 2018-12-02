using Clipp3r.Core.Common;
using Clipp3r.Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Clipp3r.Infrastructure.EntityFramework
{
    [RegisterService(typeof(IDatabaseConnectionSettings), ServiceLifetime.Singleton)]
    class DatabaseConnectionSettings : IDatabaseConnectionSettings
    {
        public string ConnectionString { get; }

        public DatabaseConnectionSettings()
        {
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Clipp3r.sqlite");
            ConnectionString = $"Data Source={databasePath};";
        }
    }
}
