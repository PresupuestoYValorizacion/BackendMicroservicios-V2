
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
            Parametro.Create(new ParametroId(11), "ADMINISTRADOR", null, null, new ParametroId(9), 1, "2"),
            Parametro.Create(new ParametroId(12), "PERIODO DE LICENCIAS", null, null, null, 0, null),
            Parametro.Create(new ParametroId(13), "1 MES", null, null, new ParametroId(12), 1, "1"),
            Parametro.Create(new ParametroId(14), "6 MESES", null, null, new ParametroId(12), 1, "6"),
            Parametro.Create(new ParametroId(15), "12 MESES", null, null, new ParametroId(12), 1, "12"),
            Parametro.Create(new ParametroId(16), "TIPO DE IDENTIFICADOR SGO", null, null, null, 0, null),
            Parametro.Create(new ParametroId(17), "CLIENTE", null, null, new ParametroId(16), 1, "0"),
            Parametro.Create(new ParametroId(18), "PROVEEDOR", null, null, new ParametroId(16), 1, "1"),
            Parametro.Create(new ParametroId(19), "TIPO DE RECURSO", null, null, null, 0, null),
            Parametro.Create(new ParametroId(20), "MANO DE OBRA", null, null, new ParametroId(19), 1, "0"),
            Parametro.Create(new ParametroId(21), "SERVICIO", null, null, new ParametroId(19), 1, "1"),
            Parametro.Create(new ParametroId(22), "MATERIALES", null, null, new ParametroId(19), 1, "2"),
            Parametro.Create(new ParametroId(23), "CONTRATO", null, null, new ParametroId(19), 1, "3"),
            Parametro.Create(new ParametroId(24), "UBIGEO", null, null, null, 0, null),
            Parametro.Create(new ParametroId(25), "Amazonas", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(26), "Áncash", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(27), "Apurímac", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(28), "Arequipa", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(29), "Ayacucho", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(30), "Cajamarca", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(31), "Callao", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(32), "Cusco", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(33), "Huancavelica", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(34), "Huánuco", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(35), "Ica", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(36), "Junín", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(37), "La Libertad", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(38), "Lambayeque", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(39), "Lima", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(40), "Loreto", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(41), "Madre de Dios", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(42), "Moquegua", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(43), "Pasco", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(44), "Piura", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(45), "Puno", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(46), "San Martín", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(47), "Tacna", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(48), "Tumbes", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(49), "Ucayali", null, null, new ParametroId(24), 1, null),
            Parametro.Create(new ParametroId(50), "Coronel Portillo", null, null, new ParametroId(49), 2, null),
            Parametro.Create(new ParametroId(51), "Atalaya", null, null, new ParametroId(49), 2, null),
            Parametro.Create(new ParametroId(52), "Padre Abad", null, null, new ParametroId(49), 2, null),
            Parametro.Create(new ParametroId(53), "Purús", null, null, new ParametroId(49), 2, null),
            Parametro.Create(new ParametroId(54), "Callería", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(55), "Campoverde", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(56), "Iparía", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(57), "Masisea", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(58), "Yarinacocha", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(59), "Nueva Requena", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(60), "Manantay", null, null, new ParametroId(50), 3, null),
            Parametro.Create(new ParametroId(61), "Raymondi", null, null, new ParametroId(51), 3, null),
            Parametro.Create(new ParametroId(62), "Sepahua", null, null, new ParametroId(51), 3, null),
            Parametro.Create(new ParametroId(63), "Tahuanía", null, null, new ParametroId(51), 3, null),
            Parametro.Create(new ParametroId(64), "Yurúa", null, null, new ParametroId(51), 3, null),
            Parametro.Create(new ParametroId(65), "Padre Abad", null, null, new ParametroId(52), 3, null),
            Parametro.Create(new ParametroId(66), "Irázola", null, null, new ParametroId(52), 3, null),
            Parametro.Create(new ParametroId(67), "Curimana", null, null, new ParametroId(52), 3, null),
            Parametro.Create(new ParametroId(68), "Purús", null, null, new ParametroId(53), 3, null)




        );
    }
}