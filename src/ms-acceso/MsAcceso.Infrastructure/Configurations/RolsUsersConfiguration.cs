using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.RolUsers;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class RolsUsersConfiguration : IEntityTypeConfiguration<RolUser>
{
    public void Configure(EntityTypeBuilder<RolUser> builder)
    {
        builder.ToTable("rols_usuarios");
        builder.HasKey(rolsUser => rolsUser.Id);

        builder.Property(rolUser => rolUser.Id)
        .HasConversion(rolUserId => rolUserId!.Value, value => new RolUserId(value));

        builder.Property(rolUser => rolUser.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne<Rol>()
                .WithMany()
                .HasForeignKey(rolUser => rolUser.RolId);

        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(rolUser => rolUser.UserId);
    }
}
