using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("rols");
        builder.HasKey(rol => rol.Id);

        builder.Property(rol => rol.Id)
        .HasConversion(rolId => rolId!.Value, value => new RolId(value));

        builder.Property(rol => rol.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(rol => rol.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder
              .HasOne(p => p.TipoRol)
              .WithMany()
              .HasForeignKey(rol => rol.TipoRolId);

        builder.HasOne(p => p.Licencia)
                  .WithMany()
                  .HasForeignKey(rol => rol.LicenciaId);
    }
}