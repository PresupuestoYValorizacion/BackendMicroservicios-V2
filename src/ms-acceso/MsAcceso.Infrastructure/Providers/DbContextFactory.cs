using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Infrastructure;

public interface IDbContextFactory
{
    DbContext CreateDbContext(string licenciaId);
}

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DbContext CreateDbContext(string licenciaId)
    {
        licenciaId = licenciaId.ToUpper();
        
        return licenciaId switch
        {
            "ECBDEBFF-CB86-4E74-BD12-F7FBFC165DFB" => _serviceProvider.GetRequiredService<EnterpriseDbContext>(),
            "1A9E887B-AA55-49B8-B9BC-4D7BA609D065" => _serviceProvider.GetRequiredService<LicenciaDbContext>(),
            _ => throw new ArgumentException("LicenciaId no soportado", nameof(licenciaId))
        };
    }
}
