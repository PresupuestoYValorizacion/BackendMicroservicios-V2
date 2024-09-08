
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Root.Users;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder
        )
    {
        builder.ToTable("users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
            .HasConversion(userId => userId!.Value, value => new UserId(value));

            builder.Property(user => user.Username)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(2000);

            builder.Property(user => user.Activo)
            .IsRequired()
            .HasConversion(user => user!.Value, value => new Activo(value));


            builder.HasIndex(user => user.Email).IsUnique();

            builder
                   .HasOne(p => p.Empresa)
                   .WithMany()
                   .HasForeignKey(user => user.EmpresaId);

             builder.HasOne(p => p.Rol)
                       .WithMany()
                       .HasForeignKey(user => user.RolId);

    }
}