﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MsAcceso.Infrastructure;

#nullable disable

namespace MsAcceso.Infrastructure.Migrations.EnterpriseDb
{
    [DbContext(typeof(EnterpriseDbContext))]
    [Migration("20240922040831_initialApp")]
    partial class initialApp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.PersonaTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TipoDocumentoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("personas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasJuridicasTenant.PersonaJuridicaTenant", b =>
                {
                    b.Property<Guid>("PersonaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PersonaId");

                    b.ToTable("personas_juridicas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasNaturalesTenant.PersonaNaturalTenant", b =>
                {
                    b.Property<Guid>("PersonaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("PersonaId");

                    b.ToTable("personas_naturales", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant.RolPermisoOpcionTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("OpcionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RolPermisoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolPermisoId");

                    b.ToTable("rols_permisos_opciones", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosTenant.RolPermisoTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("MenuId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("rols_permisos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("rols", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UsersTenant.UserTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid?>("PersonaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PersonaId");

                    b.HasIndex("RolId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasJuridicasTenant.PersonaJuridicaTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.PersonaTenant", null)
                        .WithOne("PersonaJuridica")
                        .HasForeignKey("MsAcceso.Domain.Tenant.PersonasJuridicasTenant.PersonaJuridicaTenant", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasNaturalesTenant.PersonaNaturalTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.PersonaTenant", null)
                        .WithOne("PersonaNatural")
                        .HasForeignKey("MsAcceso.Domain.Tenant.PersonasNaturalesTenant.PersonaNaturalTenant", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant.RolPermisoOpcionTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.RolPermisosTenant.RolPermisoTenant", "RolPermiso")
                        .WithMany("RolPermisoOpcions")
                        .HasForeignKey("RolPermisoId");

                    b.Navigation("RolPermiso");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosTenant.RolPermisoTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", "Rol")
                        .WithMany("RolPermisos")
                        .HasForeignKey("RolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UsersTenant.UserTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.PersonaTenant", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId");

                    b.HasOne("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Persona");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.PersonaTenant", b =>
                {
                    b.Navigation("PersonaJuridica");

                    b.Navigation("PersonaNatural");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosTenant.RolPermisoTenant", b =>
                {
                    b.Navigation("RolPermisoOpcions");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", b =>
                {
                    b.Navigation("RolPermisos");
                });
#pragma warning restore 612, 618
        }
    }
}
