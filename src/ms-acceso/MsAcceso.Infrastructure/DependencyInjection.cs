
using Asp.Versioning;
using CleanArchitecture.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsAcceso.Application.Abstractions.Clock;
using MsAcceso.Infrastructure.Tenants;
using MsAcceso.Infrastructure.Service;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Infrastructure.Tenant;
using MsAcceso.Domain.Repository;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Infrastructure.RepositoriesApplication;
using MsAcceso.Infrastructure.RepositoriesTenant;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Infrastructure.Repositories;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Application.Paginations;
namespace MsAcceso.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        // services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        // services.AddQuartz();
        // services.AddQuartzHostedService(
        //     options => options.WaitForJobsToComplete = true       
        // );
        // services.ConfigureOptions<ProcessOutboxMessageSetup>();

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
        // services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("ConnectionString") 
             ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddDbContext<TenantDbContext>(options => {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IProductRepository, ProductRepository>();


        services.AddScoped<IParametroRepository, ParametroRepository>();
        services.AddScoped<IPaginationParametrosRepository, ParametroRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaginationUserRepository, UserRepository>();

        services.AddScoped<IPersonaRepository, PersonaRepository>();

        services.AddScoped<IUnitOfWorkApplication>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWorkTenant>(sp => sp.GetRequiredService<TenantDbContext>());

        // services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

// services.AddSingleton<ISqlConnectionFactory>( _ => new SqlConnectionFactory(connectionString));

        // SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }

}