using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.Pruebas;

namespace MsAcceso.Infrastructure;

public class LicenciaDbContext : DbContext, IUnitOfWorkApplication
{
    private readonly ICurrentTenantService _currentTenantService;
    public Guid? CurrentTenantId { get; set; }
    public string CurrentTenantConnectionString { get; set; }


    // Constructor 
    public LicenciaDbContext(ICurrentTenantService currentTenantService, DbContextOptions<LicenciaDbContext> options) : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString!;

    }

    public DbSet<Prueba> Pruebas { get; set; }

    // On Model Creating - multitenancy query filter, fires once on app start
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Prueba>().ToTable("pruebas");
        builder.Entity<Prueba>().HasKey(prueba => prueba.Id);

        builder.Entity<Prueba>().Property(prueba => prueba.Id)
        .HasConversion(pruebaId => pruebaId!.Value, value => new PruebaId(value));

        builder.Entity<Prueba>().Property(prueba => prueba.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<Prueba>().Property(prueba => prueba.Activo)
        .IsRequired()
        .HasConversion(prueba => prueba!.Value, value => new Activo(value));


    }

    // On Configuring -- dynamic connection string, fires on every request
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string tenantConnectionString = CurrentTenantConnectionString;
        if (!string.IsNullOrEmpty(tenantConnectionString)) // use tenant db if one is specified
        {
            _ = optionsBuilder.UseSqlServer(tenantConnectionString);
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
        }
    }
}