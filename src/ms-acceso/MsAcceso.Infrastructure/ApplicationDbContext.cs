using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;

namespace MsAcceso.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWorkApplication
{
    private readonly ICurrentTenantService _currentTenantService;
    public Guid? CurrentTenantId { get; set; }
    public string CurrentTenantConnectionString { get; set; }


    // Constructor 
    public ApplicationDbContext(ICurrentTenantService currentTenantService, DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString!;

    }

    // Application DbSets -- create for entity types to be applied to all databases
    public DbSet<Product> Products { get; set; }

    // On Model Creating - multitenancy query filter, fires once on app start
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Product>().HasKey(producto => producto.Id);

        builder.Entity<Product>().Property(producto => producto.Id)
        .HasConversion(productoId => productoId!.Value, value => new ProductId(value));

        builder.Entity<Product>().Property(producto => producto.Activo)
            .IsRequired()
            .HasConversion(producto => producto!.Value, value => new Domain.Shared.Activo(value));
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
        CancellationToken cancellationToken=default
        )
    {
        try{

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch(DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
        }
    }
}