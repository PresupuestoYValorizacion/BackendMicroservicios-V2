using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Domain.Tenant.UbigeosTenant;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;

namespace MsAcceso.Infrastructure;

public class EnterpriseDbContext : DbContext, IUnitOfWorkTenant
{
    private readonly ICurrentTenantService _currentTenantService;
    public Guid? CurrentTenantId { get; set; }
        public EnterpriseDbContext(string currentTenantConnectionString) 
        {
            this.CurrentTenantConnectionString = currentTenantConnectionString;
   
        }
            public string CurrentTenantConnectionString { get; set; }


    // Constructor 
    public EnterpriseDbContext(ICurrentTenantService currentTenantService, DbContextOptions<EnterpriseDbContext> options) : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString!;

    }

    public DbSet<RolTenant> Rols { get; set; }
    public DbSet<RolPermisoTenant> RolsPermisos { get; set; }
    public DbSet<RolPermisoOpcionTenant> RolsPermisoOpcions { get; set; }
    public DbSet<UserTenant> Users { get; set; }
    public DbSet<PersonaTenant> Personas { get; set; }
    public DbSet<PersonaJuridicaTenant> PersonaJurdicas { get; set; }
    public DbSet<PersonaNaturalTenant> PersonaNaturales { get; set; }

    public DbSet<EspecialidadTenant> Especialidades { get; set; }
    public DbSet<TituloTenant> Titulos { get; set; }
    public DbSet<UbigeoTenant> Ubigeos { get; set; }
    public DbSet<CarpetaPresupuestalTenant> CarpetasPresupuestales {get; set;}
    public DbSet<PartidaTenant> Partidas {get; set;}
    public DbSet<RecursoTenant> Recursos { get; set; }
    public DbSet<PartidaRecursoTenant> PartidasRecursos { get; set; }

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

        builder.Entity<TituloTenant>().ToTable("titulos");
        builder.Entity<TituloTenant>().HasKey(titulo => titulo.Id);

        builder.Entity<TituloTenant>().Property(titulo => titulo.Id)
            .HasConversion(tituloId => tituloId!.Value, value => new TituloTenantId(value));

        builder.Entity<TituloTenant>().Property(titulo => titulo.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<TituloTenant>().Property(titulo => titulo.Activo)
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