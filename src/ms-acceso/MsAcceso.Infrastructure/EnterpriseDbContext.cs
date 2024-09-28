using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;

namespace MsAcceso.Infrastructure;

public class EnterpriseDbContext : DbContext, IUnitOfWorkApplication
{
    private readonly ICurrentTenantService _currentTenantService;
    public Guid? CurrentTenantId { get; set; }
    public string CurrentTenantConnectionString { get; set; }


    // Constructor 
    public EnterpriseDbContext(ICurrentTenantService currentTenantService, DbContextOptions<EnterpriseDbContext> options) : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString!;

    }

    // Application DbSets -- create for entity types to be applied to all databases
    // public DbSet<Product> Products { get; set; }
    // public DbSet<User> UsersTenants { get; set; }
    // public DbSet<Presupuesto> Presupuestos { get; set; }
    public DbSet<RolTenant> Rols { get; set; }
    public DbSet<RolPermisoTenant> RolsPermisos { get; set; }
    public DbSet<RolPermisoOpcionTenant> RolsPermisoOpcions { get; set; }
    public DbSet<UserTenant> Users { get; set; }
    public DbSet<PersonaTenant> Personas { get; set; }
    public DbSet<PersonaJuridicaTenant> PersonaJurdicas { get; set; }
    public DbSet<PersonaNaturalTenant> PersonaNaturales { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.ApplyConfigurationsFromAssembly(typeof(EnterpriseDbContext).Assembly);
        // base.OnModelCreating(builder);

        builder.Entity<RolPermisoOpcionTenant>().ToTable("rols_permisos_opciones");
        builder.Entity<RolPermisoOpcionTenant>().HasKey(rolPermisoOpcion => rolPermisoOpcion.Id);

        builder.Entity<RolPermisoOpcionTenant>().Property(rolPermisoOpcion => rolPermisoOpcion.Id)
        .HasConversion(rolPermisoOpcionId => rolPermisoOpcionId!.Value, value => new RolPermisoOpcionTenantId(value));

        builder.Entity<RolPermisoOpcionTenant>().Property(rolPermisoOpcion => rolPermisoOpcion.Activo)
       .IsRequired()
       .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Entity<RolPermisoOpcionTenant>().HasOne(rpo => rpo.RolPermiso)
                .WithMany(rp => rp.RolPermisoOpcions)
                .HasForeignKey(rolPermisoOpcion => rolPermisoOpcion.RolPermisoId);

        builder.Entity<RolPermisoTenant>().ToTable("rols_permisos");
        builder.Entity<RolPermisoTenant>().HasKey(rolPermiso => rolPermiso.Id);

        builder.Entity<RolPermisoTenant>().Property(rolPermiso => rolPermiso.Id)
        .HasConversion(rolPermisoId => rolPermisoId!.Value, value => new RolPermisoTenantId(value));

        builder.Entity<RolPermisoTenant>().Property(rolPermiso => rolPermiso.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Entity<RolPermisoTenant>().HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rolPermiso => rolPermiso.RolId);



        builder.Entity<RolTenant>().ToTable("rols");
        builder.Entity<RolTenant>().HasKey(rol => rol.Id);

        builder.Entity<RolTenant>().Property(rol => rol.Id)
        .HasConversion(rolId => rolId!.Value, value => new RolTenantId(value));

        builder.Entity<RolTenant>().Property(rol => rol.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<RolTenant>().Property(rol => rol.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Entity<UserTenant>().ToTable("users");
        builder.Entity<UserTenant>().HasKey(user => user.Id);

        builder.Entity<UserTenant>().Property(user => user.Id)
        .HasConversion(userId => userId!.Value, value => new UserTenantId(value));

        builder.Entity<UserTenant>().Property(user => user.Username)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<UserTenant>().Property(user => user.Email)
        .IsRequired()
        .HasMaxLength(200);

        builder.Entity<UserTenant>().Property(user => user.Password)
        .IsRequired()
        .HasMaxLength(2000);

        builder.Entity<UserTenant>().Property(user => user.Activo)
        .IsRequired()
        .HasConversion(user => user!.Value, value => new Activo(value));


        builder.Entity<UserTenant>().HasIndex(user => user.Email).IsUnique();

        builder.Entity<UserTenant>()
               .HasOne(p => p.Persona)
               .WithMany()
               .HasForeignKey(user => user.PersonaId);

        builder.Entity<UserTenant>().HasOne(p => p.Rol)
                  .WithMany()
                  .HasForeignKey(user => user.RolId);

        builder.Entity<PersonaTenant>().ToTable("personas");
        builder.Entity<PersonaTenant>().HasKey(persona => persona.Id);

        builder.Entity<PersonaTenant>().Property(persona => persona.Id)
        .HasConversion(personaId => personaId!.Value, value => new PersonaTenantId(value));

        builder.Entity<PersonaTenant>().Property(persona => persona.NumeroDocumento)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<PersonaTenant>().Property(persona => persona.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        // builder.Entity<PersonaTenant>().HasOne(p => p.Tipo)
        //         .WithMany()
        //         .HasForeignKey(persona => persona.TipoId);

        // builder.Entity<PersonaTenant>().HasOne(p => p.TipoDocumento)
        //         .WithMany()
        //         .HasForeignKey(persona => persona.TipoDocumentoId);
      

        builder.Entity<PersonaTenant>().HasOne(p => p.PersonaNatural)
            .WithOne()
            .HasForeignKey<PersonaNaturalTenant>(pn => pn.PersonaId);

        builder.Entity<PersonaTenant>().HasOne(p => p.PersonaJuridica)
            .WithOne()
            .HasForeignKey<PersonaJuridicaTenant>(pj => pj.PersonaId);

        builder.Entity<PersonaJuridicaTenant>().ToTable("personas_juridicas");
        builder.Entity<PersonaJuridicaTenant>().HasKey(persona => persona.PersonaId);

        builder.Entity<PersonaJuridicaTenant>().Property(persona => persona.PersonaId)
        .HasConversion(personaId => personaId!.Value, value => new PersonaTenantId(value));

        builder.Entity<PersonaJuridicaTenant>().Property(persona => persona.RazonSocial)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<PersonaNaturalTenant>().ToTable("personas_naturales");
        builder.Entity<PersonaNaturalTenant>().HasKey(persona => persona.PersonaId);

        builder.Entity<PersonaNaturalTenant>().Property(persona => persona.PersonaId)
        .HasConversion(personaId => personaId!.Value, value => new PersonaTenantId(value));

        builder.Entity<PersonaNaturalTenant>().Property(persona => persona.NombreCompleto)
        .IsRequired()
        .HasMaxLength(400);

        // builder.Entity<User>().ToTable("user-tenant");
        // builder.Entity<User>().HasKey(user => user.Id);

        // builder.Entity<User>().Property(user => user.Id)
        // .HasConversion(userId => userId!.Value, value => new UserId(value));

        // builder.Entity<User>().Property(user => user.Username)
        // .IsRequired()
        // .HasMaxLength(100);

        // builder.Entity<User>().Property(user => user.Email)
        // .IsRequired()
        // .HasMaxLength(400);

        // builder.Entity<User>().Property(user => user.Password)
        // .IsRequired()
        // .HasMaxLength(2000);

        // builder.Entity<User>().Property(user => user.Activo)
        // .IsRequired()
        // .HasConversion(user => user!.Value, value => new Activo(value));


        // builder.Entity<User>().HasIndex(user => user.Email).IsUnique();

        // builder.Entity<Presupuesto>().ToTable("presupuestos");
        // builder.Entity<Presupuesto>().HasKey(presupuesto => presupuesto.Id);

        // builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Id)
        // .HasConversion(presupuestoId => presupuestoId!.Value, value => new PresupuestoId(value));

        // builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Nombre)
        // .IsRequired()
        // .HasMaxLength(100);

        // builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Activo)
        // .IsRequired()
        // .HasConversion(presupuesto => presupuesto!.Value, value => new Activo(value));
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