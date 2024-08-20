using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Auditorias;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;
internal sealed class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("auditorias");
        builder.HasKey(auditoria => auditoria.Id);

        builder.Property(auditoria => auditoria.Id)
        .HasConversion(auditoriaId => auditoriaId!.Value, value => new AuditoriaId(value));

        builder.Property(auditoria => auditoria.Tabla)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(auditoria => auditoria.Campo)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(auditoria => auditoria.Accion)
        .IsRequired();

        builder.Property(auditoria => auditoria.ValorAnterior);

        builder.Property(auditoria => auditoria.ValorActual);

        builder.Property(auditoria => auditoria.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Property(auditoria => auditoria.Fecha);

        builder.Property(auditoria => auditoria.UserId)
        .HasConversion(userId => userId!.Value, value => new UserId(value));
    }
}
