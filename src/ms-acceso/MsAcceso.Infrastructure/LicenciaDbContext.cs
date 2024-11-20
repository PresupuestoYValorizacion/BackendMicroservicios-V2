using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.Pruebas;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Domain.Tenant.UbigeosTenant;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;

namespace MsAcceso.Infrastructure;

public class LicenciaDbContext : DbContext, IUnitOfWorkTenant
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

    public DbSet<Presupuesto> Presupuestos { get; set; }


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

         builder.Entity<Presupuesto>().ToTable("presupuestos");
        builder.Entity<Presupuesto>().HasKey(presupuesto => presupuesto.Id);

        builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Id)
        .HasConversion(presupuestoId => presupuestoId!.Value, value => new PresupuestoId(value));

        builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Entity<Presupuesto>().Property(presupuesto => presupuesto.Activo)
        .IsRequired()
        .HasConversion(presupuesto => presupuesto!.Value, value => new Activo(value));

        
        builder.Entity<EspecialidadTenant>().ToTable("especialidades");
        builder.Entity<EspecialidadTenant>().HasKey(especialidad => especialidad.Id);

        builder.Entity<EspecialidadTenant>().Property(especialidad => especialidad.Id)
            .HasConversion(especialidadId => especialidadId!.Value, value => new EspecialidadTenantId(value));

        builder.Entity<EspecialidadTenant>().Property(especialidad => especialidad.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<EspecialidadTenant>().Property(especialidad => especialidad.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));


        builder.Entity<UbigeoTenant>().ToTable("ubigeos");
        builder.Entity<UbigeoTenant>().HasKey(ubigeo => ubigeo.Id);

        builder.Entity<UbigeoTenant>().Property(ubigeo => ubigeo.Id)
            .HasConversion(ubigeoId => ubigeoId!.Value, value => new UbigeoTenantId(value));

        builder.Entity<UbigeoTenant>().Property(ubigeo => ubigeo.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<UbigeoTenant>().Property(ubigeo => ubigeo.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   

        builder.Entity<UbigeoTenant>().HasOne(u => u.DependenciaModel)
            .WithMany(u => u.Ubigeos)
            .HasForeignKey(u => u.Dependencia);

        builder.Entity<CarpetaPresupuestalTenant>().ToTable("carpetas_presupuestales");
        builder.Entity<CarpetaPresupuestalTenant>().HasKey(carpetaPresupuestal => carpetaPresupuestal.Id);

        builder.Entity<CarpetaPresupuestalTenant>().Property(carpetaPresupuestal => carpetaPresupuestal.Id)
            .HasConversion(carpetaPresupuestalId => carpetaPresupuestalId!.Value, value => new CarpetaPresupuestalTenantId(value));

        builder.Entity<CarpetaPresupuestalTenant>().Property(carpetaPresupuestal => carpetaPresupuestal.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<CarpetaPresupuestalTenant>().Property(carpetaPresupuestal => carpetaPresupuestal.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   

        builder.Entity<CarpetaPresupuestalTenant>().HasOne(cPresupuestal => cPresupuestal.DependenciaModel)
            .WithMany(cPresupuestal => cPresupuestal.CarpetasPresupuestales)
            .HasForeignKey(cPresupuestal => cPresupuestal.Dependencia);

        builder.Entity<PartidaTenant>().ToTable("partidas");
        builder.Entity<PartidaTenant>().HasKey(partida => partida.Id);

        builder.Entity<PartidaTenant>().Property(partida => partida.Id)
            .HasConversion(partidaId => partidaId!.Value, value => new PartidaTenantId(value));

        builder.Entity<PartidaTenant>().Property(partida => partida.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<PartidaTenant>().Property(partida => partida.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   

        builder.Entity<PartidaTenant>().HasOne(partida => partida.DependenciaModel)
            .WithMany(partida => partida.Partidas)
            .HasForeignKey(partida => partida.Dependencia);
        builder.Entity<PartidaTenant>().HasMany(partida => partida.Recursos)
            .WithMany()
            .UsingEntity<PartidaRecursoTenant>(
                r => r.HasOne<RecursoTenant>(r => r.Recurso).WithMany().HasForeignKey(e => e.RecursoId),
                p => p.HasOne<PartidaTenant>(p => p.Partida).WithMany(p => p.PartidasRecursos).HasForeignKey(e => e.PartidaId)
            );


        builder.Entity<RecursoTenant>().ToTable("recursos");
        builder.Entity<RecursoTenant>().HasKey(recurso => recurso.Id);
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Id)
            .HasConversion(recursoId => recursoId!.Value, value => new RecursoTenantId(value));
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Nombre)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<RecursoTenant>().Property(recurso => recurso.IdTipoRecurso)
            .IsRequired();
        builder.Entity<RecursoTenant>().Property(recurso => recurso.IdUnidadMedida)
            .IsRequired();
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));    

        builder.Entity<PartidaRecursoTenant>().ToTable("partida_recurso");
        builder.Entity<PartidaRecursoTenant>().HasKey(pRecurso => pRecurso.Id);
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.Id)
            .HasConversion(pRecurso => pRecurso!.Value, value => new PartidaRecursoTenantId(value));
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.PartidaId)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.RecursoId)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.Cantidad)
            .IsRequired();
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.Cuadrilla)
            .IsRequired();
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.Precio)
            .IsRequired();
        builder.Entity<PartidaRecursoTenant>().Property(pRecurso => pRecurso.Parcial)
            .IsRequired();
        builder.Entity<PartidaRecursoTenant>().Property(recurso => recurso.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   
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