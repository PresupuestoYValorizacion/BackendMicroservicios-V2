
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class ParametroConfiguration : IEntityTypeConfiguration<Parametro>
{
    public void Configure(
        EntityTypeBuilder<Parametro> builder
        )
    {
        builder.ToTable("parametros");
        builder.HasKey(parametro => parametro.Id);

        builder.Property(parametro => parametro.Id)
        .HasConversion(parametroId => parametroId!.Value, value => new ParametroId(value));

        builder.Property(parametro => parametro.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(parametro => parametro.Abreviatura)
        .HasMaxLength(10);

        builder.Property(parametro => parametro.Descripcion)
        .HasMaxLength(300);

        builder.Property(parametro => parametro.Nivel)
        .IsRequired();

        builder.Property(parametro => parametro.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.Property(parametro => parametro.Valor)
        .HasMaxLength(2);

        builder.HasOne<Parametro>()
            .WithMany()
            .HasForeignKey(parametro => parametro.Dependencia);


        builder.HasData(
            Parametro.Create(new ParametroId(1), "TIPOS DE DOCUMENTO", null, null, null, 0, null),
            Parametro.Create(new ParametroId(2), "DOCUMENTO NACIONAL DE IDENTIDAD", "DNI", null, new ParametroId(1), 1, "1"),
            Parametro.Create(new ParametroId(3), "REGISTRO UNICO DE CONTRIBUYENTES", "RUC", null, new ParametroId(1), 1, "2"),
            Parametro.Create(new ParametroId(4), "CARNET DE EXTRANJERIA", "CE", null, new ParametroId(1), 1, "3"),
            Parametro.Create(new ParametroId(5), "ESTADO DE SOLICITUDES", null, null, null, 0, null),
            Parametro.Create(new ParametroId(6), "TIPO DE PERSONA", null, null, null, 0, null),
            Parametro.Create(new ParametroId(7), "NATURAL", null, null, new ParametroId(6), 1, "1"),
            Parametro.Create(new ParametroId(8), "JURIDICA", null, null, new ParametroId(6), 1, "2"),
            Parametro.Create(new ParametroId(9), "TIPO DE ASUNTO", null, null, null, 0, null)

        );
    }
}