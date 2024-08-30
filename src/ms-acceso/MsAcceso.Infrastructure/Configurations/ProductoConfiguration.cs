using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("productos");
        builder.HasKey(producto => producto.Id);

        builder.Property(producto => producto.Id)
        .HasConversion(productoId => productoId!.Value, value => new ProductoId(value));

        builder.Property(producto => producto.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(producto => producto.Codigo)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(producto => producto.Cantidad)
        .IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));
    }
}
