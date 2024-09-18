using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Ciudadanos;
using MsAcceso.Domain.Root.Pasaportes;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class CiudadanoConfiguration : IEntityTypeConfiguration<Ciudadano>
{
    public void Configure(EntityTypeBuilder<Ciudadano> builder)
    {
        builder.ToTable("ciudadanos");
        builder.HasKey(ciudadano => ciudadano.Id);

        builder.Property(ciudadano => ciudadano.Id)
        .HasConversion(ciudadanoId => ciudadanoId!.Value, value => new CiudadanoId(value));

        builder.Property(ciudadano => ciudadano.Apellido)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(ciudadano => ciudadano.Nacionalidad)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder
        .HasOne(c => c.Pasaporte)
        .WithOne()
        .HasForeignKey<Pasaporte>(p => p.Id);
    }
}
