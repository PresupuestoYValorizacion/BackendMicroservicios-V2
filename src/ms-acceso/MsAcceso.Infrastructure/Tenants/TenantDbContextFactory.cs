using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MsAcceso.Infrastructure.Tenants

{
    public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args) // neccessary for EF migration designer to run on this context
        {

            // Build the configuration by reading from the appsettings.json file (requires Microsoft.Extensions.Configuration.Json Nuget Package)
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            // Retrieve the connection string from the configuration
            string connectionString = configuration.GetConnectionString("ConnectionString")!;


            DbContextOptionsBuilder<TenantDbContext> optionsBuilder = new();
            _ = optionsBuilder.UseSqlServer(connectionString);
            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
