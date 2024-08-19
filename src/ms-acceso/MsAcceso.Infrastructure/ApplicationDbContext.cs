using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Shared;

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
    // public DbSet<Product> Products { get; set; }
    public DbSet<User> UsersTenants { get; set; }

    // On Model Creating - multitenancy query filter, fires once on app start
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("user-tenant");
        builder.Entity<User>().HasKey(user => user.Id);

        builder.Entity<User>().Property(user => user.Id)
        .HasConversion(userId => userId!.Value, value => new UserId(value));

        builder.Entity<User>().Property(user => user.Username)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<User>().Property(user => user.Email)
        .IsRequired()
        .HasMaxLength(400);

        builder.Entity<User>().Property(user => user.Password)
        .IsRequired()
        .HasMaxLength(2000);

        builder.Entity<User>().Property(user => user.Activo)
        .IsRequired()
        .HasConversion(user => user!.Value, value => new Activo(value));


        builder.Entity<User>().HasIndex(user => user.Email).IsUnique();
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