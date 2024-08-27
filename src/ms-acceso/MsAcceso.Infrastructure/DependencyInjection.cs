
using Asp.Versioning;
using CleanArchitecture.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Clock;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Infrastructure.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Infrastructure.RepositoriesApplication;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Infrastructure.Repositories;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.UsuarioLicencias;

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

        //BD POR CADA CLIENTE
        services.AddScoped<IUserTenantRepository, UserTenantRepository>();


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

        services.AddScoped<ILicenciaRepository, LicenciaRepository>();
        services.AddScoped<IUsuarioLicenciaRepository, UsuarioLicenciaRepository>();

        services.AddScoped<IMenuOpcionRepository, MenuOpcionRepository>();

        services.AddScoped<IUnitOfWorkApplication>(sp => sp.GetRequiredService<EnterpriseDbContext>());
        
        services.AddScoped<IUnitOfWorkApplication>(sp => sp.GetRequiredService<LicenciaDbContext>());

        services.AddScoped<IUnitOfWorkTenant>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

// services.AddSingleton<ISqlConnectionFactory>( _ => new SqlConnectionFactory(connectionString));

        // SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }

}