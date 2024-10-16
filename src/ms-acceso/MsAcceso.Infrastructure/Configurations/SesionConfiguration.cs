using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;
internal sealed class SesionConfiguration : IEntityTypeConfiguration<Sesion>
{
    public void Configure(EntityTypeBuilder<Sesion> builder)
    {
        builder.ToTable("sesiones");
        builder.HasKey(sesion => sesion.Id);

        builder.Property(sesion => sesion.Id)
        .HasConversion(auditoriaId => auditoriaId!.Value, value => new SesionId(value));

        builder.Property(sesion => sesion.LastActivity);

        builder.Property(sesion => sesion.JwtToken);

        builder.Property(sesion => sesion.UserId);

        builder.Property(sesion => sesion.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}
