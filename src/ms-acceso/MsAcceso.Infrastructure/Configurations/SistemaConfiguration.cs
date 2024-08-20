using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;
internal sealed class SistemaConfiguration : IEntityTypeConfiguration<Sistema>
{
    public void Configure(EntityTypeBuilder<Sistema> builder)
    {
        builder.ToTable("sistemas");
        builder.HasKey(sistema => sistema.Id);

        builder.Property(sistema => sistema.Id)
        .HasConversion(sistemaId => sistemaId!.Value, value => new SistemaId(value));

        builder.Property(sistema => sistema.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(sistema => sistema.Logo)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(sistema => sistema.Nivel)
        .IsRequired();

        builder.Property(sistema => sistema.Url)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(sistema => sistema.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne<Sistema>()
                .WithMany()
                .HasForeignKey(sistema => sistema.Dependencia);
                
        builder.HasOne<Parametro>()
                .WithMany()
                .HasForeignKey(sistema => sistema.Tipo);;
    }
}
