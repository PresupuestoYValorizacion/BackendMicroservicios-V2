using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Ciudadanos;
using MsAcceso.Domain.Root.Pasaportes;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class PasaporteConfiguration : IEntityTypeConfiguration<Pasaporte>
{
    public void Configure(EntityTypeBuilder<Pasaporte> builder)
    {
        builder.ToTable("pasaportes");
        builder.HasKey(pasaporte => pasaporte.Id);

        builder.Property(pasaporte => pasaporte.Id)
        .HasConversion(ciudadanoId => ciudadanoId!.Value, value => new CiudadanoId(value));

        builder.Property(pasaporte => pasaporte.NroSerie)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}
