using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasNaturales;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class PersonaNaturalConfiguration : IEntityTypeConfiguration<PersonaNatural>
{
    public void Configure(EntityTypeBuilder<PersonaNatural> builder)
    {
        builder.ToTable("personas_naturales");
        builder.HasKey(persona => persona.PersonaId);

        builder.Property(persona => persona.PersonaId)
        .HasConversion(personaId => personaId!.Value, value => new PersonaId(value));

        builder.Property(persona => persona.ApellidoMaterno)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(persona => persona.ApellidoPaterno)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(persona => persona.Nombres)
        .IsRequired()
        .HasMaxLength(200);
    }
}