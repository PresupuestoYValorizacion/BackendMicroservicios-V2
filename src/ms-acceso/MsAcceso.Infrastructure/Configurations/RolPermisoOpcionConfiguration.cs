using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.RolPermisosOpciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class RolPermisoOpcionConfiguration : IEntityTypeConfiguration<RolPermisoOpcion>
{
    public void Configure(EntityTypeBuilder<RolPermisoOpcion> builder)
    {
        builder.ToTable("rols_permisos_opciones");
        builder.HasKey(rolPermisoOpcion => rolPermisoOpcion.Id);

        builder.Property(rolPermisoOpcion => rolPermisoOpcion.Id)
        .HasConversion(rolPermisoOpcionId => rolPermisoOpcionId!.Value, value => new RolPermisoOpcionId(value));

         builder.Property(rolPermisoOpcion => rolPermisoOpcion.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne(rpo=>rpo.RolPermiso)
                .WithMany(rp => rp.RolPermisoOpcions)
                .HasForeignKey(rolPermisoOpcion => rolPermisoOpcion.RolPermisoId);

        builder.HasOne(rpo=>rpo.Opcion)
                .WithMany(o=>o.RolPermisoOpcions)
                .HasForeignKey(rolPermisoOpcion => rolPermisoOpcion.OpcionId);
    }
}