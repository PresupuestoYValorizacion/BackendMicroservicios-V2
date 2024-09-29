using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("libros");
        builder.HasKey(libro => libro.Id);

        builder.Property(libro => libro.Id)
        .HasConversion(libroId => libroId!.Value, value => new LibroId(value));

        builder.Property(libro => libro.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(libro => libro.Descripcion)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(libro => libro.Precio)
        .IsRequired();

        builder.Property(libro => libro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

    }
}