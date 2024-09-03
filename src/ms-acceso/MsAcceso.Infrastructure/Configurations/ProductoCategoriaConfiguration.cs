using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Resenias;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Root.ProductoProductoCategorias;
using MsAcceso.Domain.Root.ProductoCategorias;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class ProductoCategoriaConfiguration : IEntityTypeConfiguration<ProductoCategoria>
{
    public void Configure(EntityTypeBuilder<ProductoCategoria> builder)
    {
        builder.ToTable("producto-categorias");
        builder.HasKey(pCategoria => pCategoria.Id);

        builder.Property(pCategoria => pCategoria.Id)
        .HasConversion(pCategoriaId => pCategoriaId!.Value, value => new ProductoCategoriaId(value));

        builder.Property(pCategoria => pCategoria.ProductoId)
        .IsRequired();

        builder.Property(pCategoria => pCategoria.CategoriaId)
        .IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}