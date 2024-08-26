
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class UsuarioLicenciaConfiguration : IEntityTypeConfiguration<UsuarioLicencia>
{
    public void Configure(
        EntityTypeBuilder<UsuarioLicencia> builder
        )
    {
        builder.ToTable("usuario_licencia");
        builder.HasKey(usuarioLicencia => usuarioLicencia.Id);

        builder.Property(usuarioLicencia => usuarioLicencia.Id)
        .HasConversion(usuarioLicenciaId => usuarioLicenciaId!.Value, value => new UsuarioLicenciaId(value));

        builder.Property(usuarioLicencia => usuarioLicencia.FechaInicio);

        builder.Property(usuarioLicencia => usuarioLicencia.FechaFin);

        builder.Property(usuarioLicencia => usuarioLicencia.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne(ul => ul.User)
                       .WithMany(u=> u.UsuarioLicencias)
                       .HasForeignKey(usuarioLicencia => usuarioLicencia.UserId);

        builder.HasOne(ul => ul.Licencia)
                .WithMany()
                .HasForeignKey(usuarioLicencia => usuarioLicencia.LicenciaId);
    }
}