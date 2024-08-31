using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Root.DetalleProductos;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class DetalleProductoConfiguration : IEntityTypeConfiguration<DetalleProducto>
{
    public void Configure(EntityTypeBuilder<DetalleProducto> builder)
    {
        builder.ToTable("detalle-productos");
        builder.HasKey(detalleProducto => detalleProducto.Id);

        builder.Property(detalleProducto => detalleProducto.Id)
        .HasConversion(productoId => productoId!.Value, value => new ProductoId(value));

        builder.Property(detalleProducto => detalleProducto.Descripcion)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(detalleProducto => detalleProducto.FechaCreacion)
        .IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}
