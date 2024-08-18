using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Infrastructure;
using MsAcceso.Infrastructure.Tenants;


namespace MsAcceso.Extensions
{
    public static class MultipleDatabaseExtensions
    {
        public static IServiceCollection AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration configuration)
        {

            // Tenant Db Context (reference context) - get a list of tenants
            using IServiceScope scopeTenant = services.BuildServiceProvider().CreateScope();
            TenantDbContext tenantDbContext = scopeTenant.ServiceProvider.GetRequiredService<TenantDbContext>();

            if (tenantDbContext.Database.GetPendingMigrations().Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Applying BaseDb Migrations.");
                Console.ResetColor();
                tenantDbContext.Database.Migrate(); // apply migrations on baseDbContext
            }


            List<User> tenantsInDb = tenantDbContext.Users.ToList();

            string defaultConnectionString = configuration.GetConnectionString("ConnectionString")!; // read default connection string from appsettings.json

            foreach (User tenant in tenantsInDb) // loop through all tenants, apply migrations on applicationDbContext
            {
                string connectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnectionString : tenant.ConnectionString;

                // Application Db Context (app - per tenant)
                using IServiceScope scopeApplication = services.BuildServiceProvider().CreateScope();
                ApplicationDbContext dbContext = scopeApplication.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Applying Migrations for '{tenant.Id}' tenant.");
                    Console.ResetColor();
                    dbContext.Database.Migrate();
                }
            }

            return services;
        }

    }
}
