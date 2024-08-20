using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsAcceso.Domain.Root.EmpresasSistemas;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Infrastructure.Configurations;

internal sealed class EmpresaSistemaConfiguration : IEntityTypeConfiguration<EmpresaSistema>
{
    public void Configure(
        EntityTypeBuilder<EmpresaSistema> builder
        )
    {
        builder.ToTable("empresas_sistemas");
        builder.HasKey(empresaSistema => empresaSistema.Id);

        builder.Property(empresaSistema => empresaSistema.Id)
        .HasConversion(empresaSistemaId => empresaSistemaId!.Value, value => new EmpresaSistemaId(value));

         builder.Property(empresaSistema => empresaSistema.Activo)
        .IsRequired()
        .HasConversion(estado => estado!.Value, value => new Activo(value));

        builder.HasOne<Persona>()
                .WithMany()
                .HasForeignKey(empresaSistema => empresaSistema.EmpresaId);

        builder.HasOne<Sistema>()
                .WithMany()
                .HasForeignKey(empresaSistema => empresaSistema.SistemaId);


    }
}