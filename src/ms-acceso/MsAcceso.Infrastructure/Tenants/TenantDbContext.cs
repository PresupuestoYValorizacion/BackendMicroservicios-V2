using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Exceptions;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Tenants
{


    public class TenantDbContext : DbContext, IUnitOfWorkTenant
    {
        // This context is for looking up the tenant when a request comes in.
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Persona> Personas { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("user");
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

            builder.Entity<User>()
                   .HasOne(p => p.Empresa)
                   .WithMany()
                   .HasForeignKey(user => user.EmpresaId);

            builder.Entity<Parametro>().ToTable("parametros");
            builder.Entity<Parametro>().HasKey(parametro => parametro.Id);

            builder.Entity<Parametro>().Property(parametro => parametro.Id)
            .HasConversion(parametroId => parametroId!.Value, value => new ParametroId(value));

            builder.Entity<Parametro>().Property(parametro => parametro.Nombre)
            .IsRequired()
            .HasMaxLength(50);

            builder.Entity<Parametro>().Property(parametro => parametro.Abreviatura)
            .HasMaxLength(10);

            builder.Entity<Parametro>().Property(parametro => parametro.Descripcion)
            .HasMaxLength(300);

            builder.Entity<Parametro>().Property(parametro => parametro.Nivel)
            .IsRequired();

            builder.Entity<Parametro>().Property(parametro => parametro.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));

            builder.Entity<Parametro>().Property(parametro => parametro.Valor)
            .HasMaxLength(2);

            builder.Entity<Parametro>().HasOne<Parametro>()
                .WithMany()
                .HasForeignKey(parametro => parametro.Dependencia);


            builder.Entity<Parametro>().HasData(
                Parametro.Create(new ParametroId(1), "TIPOS DE DOCUMENTO", null, null, null, 0, null),
                Parametro.Create(new ParametroId(2), "DOCUMENTO NACIONAL DE IDENTIDAD", "DNI", null, new ParametroId(1), 1, "1"),
                Parametro.Create(new ParametroId(3), "REGISTRO UNICO DE CONTRIBUYENTES", "RUC", null, new ParametroId(1), 1, "2"),
                Parametro.Create(new ParametroId(4), "CARNET DE EXTRANJERIA", "CE", null, new ParametroId(1), 1, "3"),
                Parametro.Create(new ParametroId(5), "ESTADO DE SOLICITUDES", null, null, null, 0, null),
                Parametro.Create(new ParametroId(6), "TIPO DE PERSONA", null, null, null, 0, null),
                Parametro.Create(new ParametroId(7), "NATURAL", null, null, new ParametroId(6), 1, "1"),
                Parametro.Create(new ParametroId(8), "JURIDICA", null, null, new ParametroId(6), 1, "2"),
                Parametro.Create(new ParametroId(9), "TIPO DE ASUNTO", null, null, null, 0, null)

            );

            builder.Entity<Persona>().ToTable("personas");
            builder.Entity<Persona>().HasKey(persona => persona.Id);

            builder.Entity<Persona>().Property(persona => persona.Id)
            .HasConversion(personaId => personaId!.Value, value => new PersonaId(value));

            builder.Entity<Persona>().Property(persona => persona.NumeroDocumento)
            .IsRequired()
            .HasMaxLength(100);

            builder.Entity<Persona>().Property(persona => persona.RazonSocial)
            .IsRequired()
            .HasMaxLength(100);

            builder.Entity<Persona>().Property(persona => persona.Activo)
            .IsRequired()
            .HasConversion(estado => estado!.Value, value => new Activo(value));

            builder.Entity<Persona>().HasOne(p => p.Tipo)
                    .WithMany()
                    .HasForeignKey(persona => persona.TipoId);

            builder.Entity<Persona>().HasOne(p => p.TipoDocumento)
                    .WithMany()
                    .HasForeignKey(persona => persona.TipoDocumentoId);
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
}
