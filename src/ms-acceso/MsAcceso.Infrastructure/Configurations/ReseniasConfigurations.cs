using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Resenias;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class ReseniaConfiguration : IEntityTypeConfiguration<Resenia>
{
    public void Configure(EntityTypeBuilder<Resenia> builder)
    {
        builder.ToTable("resenias");
        builder.HasKey(resenia => resenia.Id);

        builder.Property(resenia => resenia.Id)
        .HasConversion(reseniaId => reseniaId!.Value, value => new ReseniaId(value));

        builder.Property(resenia => resenia.Comentario)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(resenia => resenia.Calificacion)
        .IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder
        .HasOne<Producto>()
        .WithMany(p => p.Resenias)
        .HasForeignKey(p => p.ProductoId);

    }
}