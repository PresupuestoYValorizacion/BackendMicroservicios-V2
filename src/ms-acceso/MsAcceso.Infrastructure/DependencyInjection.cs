
using Asp.Versioning;
using CleanArchitecture.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Clock;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Application.Tenant.Paginations;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Infrastructure.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Infrastructure.RepositoriesApplication;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;

namespace MsAcceso.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {

        services.AddApiVersioning(options => 
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc()
        .AddApiExplorer(options => 
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });


        services.AddScoped<ICurrentTenantService, CurrentTenantService>();

        services.AddTransient<ITenantProvider, TenantProvider>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var connectionString = configuration.GetConnectionString("ConnectionString") 
             ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<EnterpriseDbContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddDbContext<LicenciaDbContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IDbContextFactory, DbContextFactory>();

        //BD POR CADA CLIENTE
        services.AddScoped<IRolTenantRepository, RolTenantRepository>();
        services.AddScoped<IPaginationRolesTenantRepository, RolTenantRepository>();
        services.AddScoped<IUserTenantRepository, UserTenantRepository>();
        services.AddScoped<IPaginationUsersTenantRepository, UserTenantRepository>();
        services.AddScoped<IRolPermisoTenantRepository, RolPermisoTenantRepository>();
        services.AddScoped<IRolPermisoOpcionTenantRepository, RolPermisoOpcionTenantRepository>();
        services.AddScoped<IPersonaTenantRepository, PersonaTenantRepository>();
        services.AddScoped<IPersonaNaturalTenantRepository, PersonaNaturalTenantRepository>();
        services.AddScoped<IPersonaJuridicaTenantRepository, PersonaJuridicaTenantRepository>();
        services.AddScoped<IClienteTenantRepository, ClienteTenantRepository>();
        services.AddScoped<IPaginationClientesRepository, ClienteTenantRepository>();

        services.AddScoped<IEspecialidadTenantRepository, EspecialidadTenantRepository>();
        services.AddScoped<ITituloTenantRepository, TituloTenantRepository>();
        services.AddScoped<ICarpetaPresupuestalTenantRepository, CarpetaPresupuestalTenantRepository>();
        services.AddScoped<IPartidaTenantRepository, PartidaTenantRepository>();
        services.AddScoped<IRecursoTenantRepository, RecursoTenantRepository>();
        services.AddScoped<IProyectoTenantRepository, ProyectoTenantRepository>();
        services.AddScoped<IPresupuestoTenantRepository, PresupuestoTenantRepository>();

        //BD GENERAL
        services.AddScoped<IParametroRepository, ParametroRepository>();
        services.AddScoped<IPaginationParametrosRepository, ParametroRepository>();

        services.AddScoped<IOpcionRepository, OpcionRepository>();
        services.AddScoped<IPaginationOpcionRepository, OpcionRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaginationUserRepository, UserRepository>();

        services.AddScoped<IPersonaJuridicaRepository, PersonaJuridicaRepository>();
        services.AddScoped<IPersonaNaturalRepository, PersonaNaturalRepository>();


        services.AddScoped<IPersonaRepository, PersonaRepository>();
        services.AddScoped<ISistemaRepository, SistemaRepository>();

        
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IPaginationRolesRepository, RolRepository>();

        services.AddScoped<IRolPermisoOpcionRepository, RolPermisoOpcionRepository>();
        services.AddScoped<IRolPermisoRepository, RolPermisoRepository>();

        services.AddScoped<ILicenciaRepository, LicenciaRepository>();
        services.AddScoped<IUsuarioLicenciaRepository, UsuarioLicenciaRepository>();

        services.AddScoped<IMenuOpcionRepository, MenuOpcionRepository>();
        services.AddScoped<ISesionRepository, SesionRepository>();

        services.AddScoped<IUnitOfWorkTenant>(sp => sp.GetRequiredService<EnterpriseDbContext>());
        
        services.AddScoped<IUnitOfWorkTenant>(sp => sp.GetRequiredService<LicenciaDbContext>());

        services.AddScoped<IUnitOfWorkApplication>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

}