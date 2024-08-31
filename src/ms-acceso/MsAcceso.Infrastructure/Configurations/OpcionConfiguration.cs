using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class OpcionConfiguration : IEntityTypeConfiguration<Opcion>
{
    public void Configure(EntityTypeBuilder<Opcion> builder)
    {
        builder.ToTable("opciones");
        builder.HasKey(opcion => opcion.Id);

        builder.Property(opcion => opcion.Id)
        .HasConversion(opcionId => opcionId!.Value, value => new OpcionId(value));

        builder.Property(opcion => opcion.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(opcion => opcion.Logo)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(opcion => opcion.Abreviatura)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));
    }
}
