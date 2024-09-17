using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class MenusOpcionsConfiguration : IEntityTypeConfiguration<MenuOpcion>
{
    public void Configure(EntityTypeBuilder<MenuOpcion> builder)
    {
        builder.ToTable("menus_opciones");
        builder.HasKey(menuOpcion => menuOpcion.Id);

        builder.Property(menuOpcion => menuOpcion.Id)
        .HasConversion(menuOpcionId => menuOpcionId!.Value, value => new MenuOpcionId(value));

        builder.Property(menuOpcion => menuOpcion.Activo)
       .IsRequired()
       .HasConversion(estado => estado!.Value, value => new Activo(value));

       builder.Property(sistema => sistema.TieneUrl);

       builder.Property(sistema => sistema.Url);

        builder.Property(sistema => sistema.Orden);

        builder.HasOne(o=> o.Opcion)
                .WithMany()
                .HasForeignKey(menuOpcion => menuOpcion.OpcionesId);

        builder.HasOne<Sistema>()
                .WithMany()
                .HasForeignKey(menuOpcion => menuOpcion.MenusId);
    }
}
