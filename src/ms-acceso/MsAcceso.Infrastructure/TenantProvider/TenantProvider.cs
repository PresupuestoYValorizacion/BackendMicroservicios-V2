

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Infrastructure.Tenant;

public sealed class TenantProvider : ITenantProvider
{
    private readonly ApplicationDbContext _context; // database context
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

        // Genera una cadena de conexión para la nueva base de datos del tenant
        string dbName = id.ToString();
        string defaultConnectionString = _configuration.GetConnectionString("ConnectionString")!;
        string newConnectionString = defaultConnectionString.Replace("Acceso", dbName);

        // Crea una nueva base de datos para el tenant y aplica las migraciones pendientes de ApplicationDbContext
        try
        {
            // var user = await _context.Users.Include(x => x.UsuarioLicencias).Where(x => x.Id == new UserId(id)).FirstOrDefaultAsync();

            // var usuarioLicencia = user!.UsuarioLicencias!.First();

            var licencia = await _context.Licencias.Where(x => x.Id == licenciaId).FirstOrDefaultAsync();

            // TODO : CAMBIAR EL IF QUE SEA A UN CAMPO DE LICENCIA COMO TIENE ACCESO A USUARIOS PERSONALIZADOS 
            // TODO : Y SI TIENE ACCESO A ESTO QUE ENTRE 

            if(licencia!.Id == new LicenciaId(new Guid("ECBDEBFF-CB86-4E74-BD12-F7FBFC165DFB")))
            {

                using IServiceScope scopeTenant = _serviceProvider.CreateScope();
                EnterpriseDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<EnterpriseDbContext>();
                dbContext.Database.SetConnectionString(newConnectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Applying ApplicationDB Migrations for New '{id}' tenant.");
                    Console.ResetColor();
                    await dbContext.Database.MigrateAsync(); // Nota: Se usa await para llamadas async
                }


            }else{
                using IServiceScope scopeTenant = _serviceProvider.CreateScope();
                LicenciaDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<LicenciaDbContext>();
                dbContext.Database.SetConnectionString(newConnectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Applying ApplicationDB Migrations for New '{id}' tenant.");
                    Console.ResetColor();
                    await dbContext.Database.MigrateAsync(); // Nota: Se usa await para llamadas async
                }
            }

            return newConnectionString; 
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear la base de datos para el tenant '{id}': {ex.Message}");
        }

    }

    public async Task<bool> Delete(Guid id)
    {
        // Genera el nombre de la base de datos basada en el ID del tenant
        string dbName = id.ToString();
        string defaultConnectionString = _configuration.GetConnectionString("ConnectionString")!;
        string targetConnectionString = defaultConnectionString.Replace("Acceso", dbName);

        try
        {
            // Crea un nuevo alcance para el contexto del tenant
            using IServiceScope scopeTenant = _serviceProvider.CreateScope();
            EnterpriseDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<EnterpriseDbContext>();
            dbContext.Database.SetConnectionString(targetConnectionString);

            // Verifica si la base de datos existe
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
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar la base de datos para el tenant '{id}': {ex.Message}");
        }
    }

}