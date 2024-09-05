using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class RolPermisoConfiguration : IEntityTypeConfiguration<RolPermiso>
{
    public void Configure(EntityTypeBuilder<RolPermiso> builder)
    {
        builder.ToTable("rols_permisos");
        builder.HasKey(rolPermiso => rolPermiso.Id);

        builder.Property(rolPermiso => rolPermiso.Id)
        .HasConversion(rolPermisoId => rolPermisoId!.Value, value => new RolPermisoId(value));

        builder.Property(rolPermiso => rolPermiso.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne(rp=> rp.Rol)
                .WithMany(r=>r.RolPermisos)
                .HasForeignKey(rolPermiso => rolPermiso.RolId);

        builder.HasOne(rp => rp.Menu)
                .WithMany(s => s.RolPermisos)
                .HasForeignKey(rolPermiso => rolPermiso.MenuId);
    }
}
