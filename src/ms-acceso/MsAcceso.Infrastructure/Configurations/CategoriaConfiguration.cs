using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Categorias;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categorias");
        builder.HasKey(categoria => categoria.Id);

        builder.Property(categoria => categoria.Id)
        .HasConversion(categoriaId => categoriaId!.Value, value => new CategoriaId(value));

        builder.Property(categoria => categoria.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}
