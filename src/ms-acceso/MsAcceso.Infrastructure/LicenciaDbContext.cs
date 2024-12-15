using Microsoft.EntityFrameworkCore;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Domain.Tenant.UbigeosTenant;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant;

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

    public DbSet<EspecialidadTenant> Especialidades { get; set; }
    public DbSet<TituloTenant> Titulos { get; set; }
    public DbSet<UbigeoTenant> Ubigeos { get; set; }
    public DbSet<CarpetaPresupuestalTenant> CarpetasPresupuestales {get; set;}
    public DbSet<PartidaTenant> Partidas {get; set;}
    public DbSet<RecursoTenant> Recursos { get; set; }
    public DbSet<PartidaRecursoTenant> PartidasRecursos { get; set; }
    public DbSet<PresupuestoTenant> Presupuestos { get; set; }


    // On Model Creating - multitenancy query filter, fires once on app start
    protected override void OnModelCreating(ModelBuilder builder)
    { 
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
        builder.Entity<EspecialidadTenant>().HasMany(especialidad => especialidad.Presupuestos)
            .WithMany()
            .UsingEntity<PresupuestoEspecialidadTenant>(
                p => p.HasOne<PresupuestoTenant>(p => p.Presupuesto).WithMany().HasForeignKey(e => e.PresupuestoId),
                e => e.HasOne<EspecialidadTenant>(p => p.Especialidad).WithMany(p => p.PresupuestosEspecialidades).HasForeignKey(e => e.EspecialidadId)
            );

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
        builder.Entity<TituloTenant>().HasMany(titulos => titulos.PresupuestosEspecialidades)
            .WithMany()
            .UsingEntity<PresupuestoEspecialidadTituloTenant>(
                pe => pe.HasOne<PresupuestoEspecialidadTenant>(pe => pe.PresupuestoEspecialidad).WithMany().HasForeignKey(e => e.PresupuestoEspecialidadId),
                t => t.HasOne<TituloTenant>(p => p.Titulo).WithMany(t => t.PresupuestosEspecialidadesTitulos).HasForeignKey(t => t.TituloId)
            );

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
        builder.Entity<PartidaTenant>().HasMany(partida => partida.PresupuestosEspecialidadesTitulos)
            .WithMany()
            .UsingEntity<PresupuestoEspecialidadTituloPartidaTenant>(
                pet => pet.HasOne<PresupuestoEspecialidadTituloTenant>(pet => pet.PresupuestoEspecialidadTitulo).WithMany().HasForeignKey(e => e.PresupuestoEspecialidadTituloId),
                p => p.HasOne<PartidaTenant>(p => p.Partida).WithMany(p => p.PresupuestosEspecialidadesTitulosPartidas).HasForeignKey(e => e.PartidaId)
            );

        builder.Entity<RecursoTenant>().ToTable("recursos");
        builder.Entity<RecursoTenant>().HasKey(recurso => recurso.Id);
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Id)
            .HasConversion(recursoId => recursoId!.Value, value => new RecursoTenantId(value));
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Nombre)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<RecursoTenant>().Property(recurso => recurso.TipoRecursoId)
            .IsRequired();
        builder.Entity<RecursoTenant>().Property(recurso => recurso.UnidadMedidaId)
            .IsRequired();
        builder.Entity<RecursoTenant>().Property(recurso => recurso.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));  
        builder.Entity<RecursoTenant>().HasMany(partida => partida.PresupuestosEspecialidadesTitulosPartidas)
            .WithMany()
            .UsingEntity<PresupuestoEspecialidadTituloPartidaRecursoTenant>(
                petp => petp.HasOne<PresupuestoEspecialidadTituloPartidaTenant>(petp => petp.PresupuestoEspecialidadTituloPartida).WithMany().HasForeignKey(e => e.PresupuestoEspecialidadTituloPartidaId),
                r => r.HasOne<RecursoTenant>(p => p.Recurso).WithMany(p => p.PresupuestosEspecialidadesTitulosPartidasRecursos).HasForeignKey(e => e.RecursoId)
            );

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

        builder.Entity<PresupuestoTenant>().ToTable("presupuestos");
        builder.Entity<PresupuestoTenant>().HasKey(presupuesto => presupuesto.Id);
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.Id)
            .HasConversion(presupuestoId => presupuestoId!.Value, value => new PresupuestoTenantId(value));
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.Codigo)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.Descripcion)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<PresupuestoTenant>()
            .HasOne(presupuesto => presupuesto.Cliente)
            .WithMany()
            .HasForeignKey(presupuesto => presupuesto.ClienteId);
        // builder.Entity<PresupuestoTenant>().HasOne(presupuesto => presupuesto.Ubigeo)
        //     .WithOne()
        //     .HasForeignKey<PresupuestoTenant>(e => e.UbigeoId!.Value)
        //     .IsRequired();
        builder.Entity<PresupuestoTenant>()
            .HasOne(presupuesto => presupuesto.Ubigeo)
            .WithMany()
            .HasForeignKey(presupuesto => presupuesto.UbigeoId);
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.Fecha)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.Plazodias)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.JornadaDiariaId)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.MonedaId)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.PresupuestoBaseCD)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.PresupuestoBaseDI)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.TotalPresupuestoBase)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.PresupuestoOfertaCD)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.PresupuestoOfertaDI)
            .IsRequired();
        builder.Entity<PresupuestoTenant>().Property(presupuesto => presupuesto.TotalPresupuestoOferta)
            .IsRequired();
        // builder.Entity<UserTenant>()
        //        .HasOne(p => p.Persona)
        //        .WithMany()
        //        .HasForeignKey(user => user.PersonaId);
        builder.Entity<PresupuestoTenant>()
            .HasOne(presupuesto => presupuesto.CarpetaPresupuestal)
            .WithMany()
            .HasForeignKey(presupuesto => presupuesto.CarpetaPresupuestalId);
        builder.Entity<PresupuestoTenant>().Property(recurso => recurso.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Entity<PresupuestoEspecialidadTenant>().ToTable("presupuesto_especialidad");
        builder.Entity<PresupuestoEspecialidadTenant>().HasKey(pEspecialidad => pEspecialidad.Id);
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspecialidad => pEspecialidad.Id)
            .HasConversion(pEspecialidad => pEspecialidad!.Value, value => new PresupuestoEspecialidadTenantId(value));
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspecialidad => pEspecialidad.PresupuestoId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspecialidad => pEspecialidad.EspecialidadId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspecialidad => pEspecialidad.Correlativo)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspecialidad => pEspecialidad.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   

        builder.Entity<PresupuestoEspecialidadTituloTenant>().ToTable("presupuestos_especialidad_titulos");
        builder.Entity<PresupuestoEspecialidadTituloTenant>().HasKey(pEspTitulos => pEspTitulos.Id);
        builder.Entity<PresupuestoEspecialidadTituloTenant>().Property(pEspTitulos => pEspTitulos.Id)
            .HasConversion(pEspTitulosId => pEspTitulosId!.Value, value => new PresupuestoEspecialidadTituloTenantId(value));
        builder.Entity<PresupuestoEspecialidadTituloTenant>().Property(pEspTitulos => pEspTitulos.PresupuestoEspecialidadId)
            .IsRequired();
        // builder.Entity<PresupuestoEspecialidadTituloTenant>().Property(pEspTitulos => pEspTitulos.TituloId)
        //     .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloTenant>()
            .HasOne(pEspTitulos => pEspTitulos.Titulo)
            .WithMany() 
            .HasForeignKey(pEspTitulos => pEspTitulos.TituloId);
        builder.Entity<PresupuestoEspecialidadTenant>().Property(pEspTitulos => pEspTitulos.Correlativo)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PresupuestoEspecialidadTituloTenant>().Property(pEspTitulos => pEspTitulos.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));   
        builder.Entity<PresupuestoEspecialidadTituloTenant>().HasOne(pEspTitulos => pEspTitulos.DependenciaModel)
            .WithMany(pEspTitulos => pEspTitulos.PresupuestosEspecialidadTitulos)
            .HasForeignKey(pEspTitulos => pEspTitulos.Dependencia);

        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().ToTable("presupuestos_especialidad_titulos_partidas");
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().HasKey(pEspTitulosPartidas => pEspTitulosPartidas.Id);
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().Property(pEspTitulosPartidas => pEspTitulosPartidas.Id)
            .HasConversion(pEspTitulosPartidasId => pEspTitulosPartidasId!.Value, value => new PresupuestoEspecialidadTituloPartidaTenantId(value));
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().Property(pEspTitulosPartidas => pEspTitulosPartidas.PresupuestoEspecialidadTituloId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().Property(pEspTitulosPartidas => pEspTitulosPartidas.PartidaId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().Property(pEspTitulos => pEspTitulos.Correlativo)
            .IsRequired()
            .HasMaxLength(100);
        builder.Entity<PresupuestoEspecialidadTituloPartidaTenant>().Property(pEspTitulos => pEspTitulos.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value)); 

        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().ToTable("presupuestos_especialidad_titulos_partidas_recursos");
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().HasKey(pEspTPartidasRecursos => pEspTPartidasRecursos.Id);
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Id)
            .HasConversion(pEspTPartidasRecursosId => pEspTPartidasRecursosId!.Value, value => new PresupuestoEspecialidadTituloPartidaRecursoTenantId(value));
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.PresupuestoEspecialidadTituloPartidaId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.RecursoId)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Cantidad)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Cuadrilla)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Precio)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Parcial)
            .IsRequired();
        builder.Entity<PresupuestoEspecialidadTituloPartidaRecursoTenant>().Property(pEspTPartidasRecursos => pEspTPartidasRecursos.Activo)
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