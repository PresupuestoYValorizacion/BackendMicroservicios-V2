
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class LicenciaConfiguration : IEntityTypeConfiguration<Licencia>
{
    public void Configure(
        EntityTypeBuilder<Licencia> builder
        )
    {
        builder.ToTable("licencias");
        builder.HasKey(licencia => licencia.Id);

        builder.Property(licencia => licencia.Id)
        .HasConversion(licenciaId => licenciaId!.Value, value => new LicenciaId(value));

        builder.Property(licencia => licencia.Nombre)
        .IsRequired()
        .HasMaxLength(50);

         builder.Property(licencia => licencia.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

       
        builder.HasData(
            Licencia.Create(new LicenciaId(new Guid("ecbdebff-cb86-4e74-bd12-f7fbfc165dfb")), "ENTERPRISE"),
            Licencia.Create(new LicenciaId(new Guid("1a9e887b-aa55-49b8-b9bc-4d7ba609d065")), "PROFESIONAL"),
            Licencia.Create(new LicenciaId(new Guid("e88a6456-3941-4136-b172-7a0d5167c7fc")), "EDUCACIONAL")
        );
    }
}