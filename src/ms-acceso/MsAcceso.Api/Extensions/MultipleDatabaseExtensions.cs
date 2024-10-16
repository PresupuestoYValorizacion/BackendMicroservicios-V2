using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure;


namespace MsAcceso.Api.Extensions
{
    public static class MultipleDatabaseExtensions
    {
        public static IServiceCollection AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration configuration)
        {

            // Tenant Db Context (reference context) - get a list of tenants
            using IServiceScope scopeTenant = services.BuildServiceProvider().CreateScope();
            ApplicationDbContext ApplicationDbContext = scopeTenant.ServiceProvider.GetRequiredService<ApplicationDbContext>();
   
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Applying BaseDb Migrations.");
                Console.ResetColor();
                ApplicationDbContext.Database.Migrate(); // apply migrations on baseDbContext
            }

            //TODO : REHACER LA MIGRACION CUANDO YA ESTA CREADO ESTO LO QUE HACES ES REMIGRAR
            // List<User> tenantsInDb = ApplicationDbContext.Users.ToList();

            // string defaultConnectionString = configuration.GetConnectionString("ConnectionString")!; // read default connection string from appsettings.json

            // foreach (User tenant in tenantsInDb) // loop through all tenants, apply migrations on applicationDbContext
            // {
            //     string connectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnectionString : tenant.ConnectionString;

            //     // Application Db Context (app - per tenant)
            //     using IServiceScope scopeApplication = services.BuildServiceProvider().CreateScope();
            //     EnterpriseDbContext dbContext = scopeApplication.ServiceProvider.GetRequiredService<EnterpriseDbContext>();
            //     dbContext.Database.SetConnectionString(connectionString);
            //     if (dbContext.Database.GetPendingMigrations().Any())
            //     {
            //         Console.ForegroundColor = ConsoleColor.Blue;
            //         Console.WriteLine($"Applying Migrations for '{tenant.Id}' tenant.");
            //         Console.ResetColor();
            //         dbContext.Database.Migrate();
            //     }
            // }

            return services;
        }

    }
}
