

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Tenant;

public sealed class TenantProvider : ITenantProvider
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    private readonly IServiceProvider _serviceProvider;


    public TenantProvider(ApplicationDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _context = context;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> Create(Guid id, LicenciaId licenciaId)
    {
        string dbName = id.ToString();
        string defaultConnectionString = _configuration.GetConnectionString("ConnectionString")!;
        string newConnectionString = defaultConnectionString.Replace("Acceso", dbName);

        try
        {
            var licencia = await _context.Licencias
                .Where(x => x.Id == licenciaId)
                .FirstOrDefaultAsync();

            if (licencia == null)
            {
                throw new Exception($"Licencia con ID '{licenciaId}' no encontrada.");
            }

            Type dbContextType = licencia.PermiteCrearUsuarios ? typeof(EnterpriseDbContext) : typeof(LicenciaDbContext);
            await ApplyMigrationsAsync(id, newConnectionString, dbContextType);

            return newConnectionString;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear la base de datos para el tenant '{id}': {ex.Message}", ex);
        }
    }

    private async Task ApplyMigrationsAsync(Guid id, string connectionString, Type dbContextType)
    {
        using var scopeTenant = _serviceProvider.CreateScope();
        var dbContext = (DbContext)scopeTenant.ServiceProvider.GetRequiredService(dbContextType);
        dbContext.Database.SetConnectionString(connectionString);

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Applying ApplicationDB Migrations for New '{id}' tenant.");
            Console.ResetColor();
            await dbContext.Database.MigrateAsync();
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        string dbName = id.ToString();
        string defaultConnectionString = _configuration.GetConnectionString("ConnectionString")!;
        string targetConnectionString = defaultConnectionString.Replace("Acceso", dbName);

        try
        {
            var usuario = await _context.Users
                .Include(x => x.Rol)
                .Where(x => x.Id == new UserId(id) && x.Activo == new Activo(true))
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return false;
            }

            var licencia = await _context.Licencias
                .Where(x => x.Id == usuario.Rol!.LicenciaId)
                .FirstOrDefaultAsync();

            if (licencia is null)
            {
                return false;
            }

            return await DeleteDatabaseAsync(id, targetConnectionString, licencia.PermiteCrearUsuarios ? typeof(EnterpriseDbContext) : typeof(LicenciaDbContext));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar la base de datos para el tenant '{id}': {ex.Message}", ex);
        }
    }

    private async Task<bool> DeleteDatabaseAsync(Guid id, string connectionString, Type dbContextType)
    {
        using var scopeTenant = _serviceProvider.CreateScope();
        var dbContext = (DbContext)scopeTenant.ServiceProvider.GetRequiredService(dbContextType);
        dbContext.Database.SetConnectionString(connectionString);

        if (await dbContext.Database.CanConnectAsync())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Deleting Database for Tenant '{id}'.");
            Console.ResetColor();
            await dbContext.Database.EnsureDeletedAsync(); // Elimina la base de datos
            return true; // Retorna true si la eliminación fue exitosa
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Database for Tenant '{id}' does not exist.");
            Console.ResetColor();
            return false; // Retorna false si la base de datos no existía
        }
    }


}