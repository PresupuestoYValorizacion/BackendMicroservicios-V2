

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Entity;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.Tenant;

public sealed class TenantProvider : ITenantProvider
{
    private readonly TenantDbContext _context; // database context
    private readonly IConfiguration _configuration;

    private readonly IServiceProvider _serviceProvider;


    public TenantProvider(TenantDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _context = context;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> Create(bool isolated, Guid id)
    {

        if (isolated)
        {
            // Genera una cadena de conexión para la nueva base de datos del tenant
            string dbName = id.ToString();
            string defaultConnectionString = _configuration.GetConnectionString("ConnectionString")!;
            string newConnectionString = defaultConnectionString.Replace("Acceso", dbName);

            // Crea una nueva base de datos para el tenant y aplica las migraciones pendientes de ApplicationDbContext
            try
            {
                using IServiceScope scopeTenant = _serviceProvider.CreateScope();
                ApplicationDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(newConnectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Applying ApplicationDB Migrations for New '{id}' tenant.");
                    Console.ResetColor();
                    await dbContext.Database.MigrateAsync(); // Nota: Se usa await para llamadas async
                }

                return newConnectionString; // Retorna la cadena de conexión creada
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la base de datos para el tenant '{id}': {ex.Message}");
            }
        }
        else
        {
            return "No se creó una base de datos nueva porque 'isolated' es false.";
        }
    }
}