using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class PersonaJuridicaConfiguration : IEntityTypeConfiguration<PersonaJuridica>
{
    public void Configure(EntityTypeBuilder<PersonaJuridica> builder)
    {
        builder.ToTable("personas_juridicas");
        builder.HasKey(persona => persona.PersonaId);

        builder.Property(persona => persona.PersonaId)
        .HasConversion(personaId => personaId!.Value, value => new PersonaId(value));

        builder.Property(persona => persona.RazonSocial)
        .IsRequired()
        .HasMaxLength(100);

    }
}