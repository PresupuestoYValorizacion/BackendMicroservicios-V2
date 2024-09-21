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
    [Migration("20240921052142_initialApp")]
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
