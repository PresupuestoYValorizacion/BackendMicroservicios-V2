using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("Libroes");
        builder.HasKey(Libro => Libro.Id);

        builder.Property(Libro => Libro.Id)
        .HasConversion(LibroId => LibroId!.Value, value => new LibroId(value));

        builder.Property(Libro => Libro.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(Libro => Libro.Descripcion)
        .HasMaxLength(100);

        builder.Property(Libro => Libro.Precio).IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));
    }
}
