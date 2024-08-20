
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Root.Personas;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(
        EntityTypeBuilder<Persona> builder
        )
    {
        builder.ToTable("personas");
        builder.HasKey(persona => persona.Id);

        builder.Property(persona => persona.Id)
        .HasConversion(personaId => personaId!.Value, value => new PersonaId(value));

        builder.Property(persona => persona.NumeroDocumento)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(persona => persona.RazonSocial)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(persona => persona.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne(p => p.Tipo)
                .WithMany()
                .HasForeignKey(persona => persona.TipoId);

        builder.HasOne(p => p.TipoDocumento)
                .WithMany()
                .HasForeignKey(persona => persona.TipoDocumentoId);
    }
}