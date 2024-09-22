
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
            Parametro.Create(new ParametroId(1), "ESTADO DE SOLICITUDES", null, null, null, 0, null),
            Parametro.Create(new ParametroId(2), "TIPO DE PERSONA", null, null, null, 0, null),
            Parametro.Create(new ParametroId(3), "NATURAL", null, null, new ParametroId(2), 1, "1"),
            Parametro.Create(new ParametroId(4), "JURIDICA", null, null, new ParametroId(2), 1, "2"),
            Parametro.Create(new ParametroId(5), "TIPO DE ASUNTO", null, null, null, 0, null),
            Parametro.Create(new ParametroId(6), "DOCUMENTO NACIONAL DE IDENTIDAD", "DNI", null, new ParametroId(3), 2, "1"),
            Parametro.Create(new ParametroId(7), "REGISTRO UNICO DE CONTRIBUYENTES", "RUC", null, new ParametroId(4), 2, "1"),
            Parametro.Create(new ParametroId(8), "CARNET DE EXTRANJERIA", "CE", null, new ParametroId(3), 2, "2"),
            Parametro.Create(new ParametroId(9), "TIPO DE ROL", null, null, null, 0, null),
            Parametro.Create(new ParametroId(10), "LICENCIA", null, null, new ParametroId(9), 1, "1"),
            Parametro.Create(new ParametroId(11), "ADMINISTRADOR", null, null, new ParametroId(9),1, "2"),
            Parametro.Create(new ParametroId(12), "PERIODO DE LICENCIAS", null, null, null,0, null),
            Parametro.Create(new ParametroId(13), "1 MES", null, null, new ParametroId(12),1, "1"),
            Parametro.Create(new ParametroId(14), "6 MESES", null, null, new ParametroId(12),1, "6"),
            Parametro.Create(new ParametroId(15), "12 MESES", null, null, new ParametroId(12),1, "12")



        );
    }
}