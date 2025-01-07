using CleanArchitecture.Application;
using Microsoft.OpenApi.Models;
using MsAcceso.Api.Extensions;
using MsAcceso.Api.Middleware;
using MsAcceso.Api.OptionsSetup;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Documentation;
// using MsAcceso.Extensions;
using MsAcceso.Infrastructure;
using MsAcceso.Infrastructure.Authentication;
using MsAcceso.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration)=> configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddControllers();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
//Esta configuracion es si el modelo como User es primitivo
// builder.Services.AddSwaggerGen();

//Esta configuracion es para como lo tenemos ahora con object values
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds( type => type.ToString());

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//QuestPdf
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
//QuestPDF.Settings.EnableDebugging = true;

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name); 
        }

    });
}

// await app.ApplyMigration();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseSessionValidationHandler();
app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantResolver>();

app.MapControllers();


app.Run();

