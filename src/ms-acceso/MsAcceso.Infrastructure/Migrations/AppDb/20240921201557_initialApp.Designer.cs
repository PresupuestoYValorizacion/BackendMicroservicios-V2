﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MsAcceso.Infrastructure;

#nullable disable

namespace MsAcceso.Infrastructure.Migrations.AppDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240921201557_initialApp")]
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

            modelBuilder.Entity("MsAcceso.Domain.Root.Auditorias.Auditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Accion")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Campo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tabla")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ValorActual")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorAnterior")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("auditorias", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Licencias.Licencia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PermiteCrearUsuarios")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("licencias", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ecbdebff-cb86-4e74-bd12-f7fbfc165dfb"),
                            Activo = true,
                            Nombre = "ENTERPRISE",
                            PermiteCrearUsuarios = true
                        },
                        new
                        {
                            Id = new Guid("1a9e887b-aa55-49b8-b9bc-4d7ba609d065"),
                            Activo = true,
                            Nombre = "PROFESIONAL",
                            PermiteCrearUsuarios = false
                        },
                        new
                        {
                            Id = new Guid("e88a6456-3941-4136-b172-7a0d5167c7fc"),
                            Activo = true,
                            Nombre = "ESTUDIANTE",
                            PermiteCrearUsuarios = false
                        });
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.MenuOpciones.MenuOpcion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MenusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OpcionesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<bool>("TieneUrl")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenusId");

                    b.HasIndex("OpcionesId");

                    b.ToTable("menus_opciones", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Opciones.Opcion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Icono")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tooltip")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("opciones", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Parametros.Parametro", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Abreviatura")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int?>("Dependencia")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("Nivel")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Valor")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.ToTable("parametros", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activo = true,
                            Nivel = 0,
                            Nombre = "ESTADO DE SOLICITUDES"
                        },
                        new
                        {
                            Id = 2,
                            Activo = true,
                            Nivel = 0,
                            Nombre = "TIPO DE PERSONA"
                        },
                        new
                        {
                            Id = 3,
                            Activo = true,
                            Dependencia = 2,
                            Nivel = 1,
                            Nombre = "NATURAL",
                            Valor = "1"
                        },
                        new
                        {
                            Id = 4,
                            Activo = true,
                            Dependencia = 2,
                            Nivel = 1,
                            Nombre = "JURIDICA",
                            Valor = "2"
                        },
                        new
                        {
                            Id = 5,
                            Activo = true,
                            Nivel = 0,
                            Nombre = "TIPO DE ASUNTO"
                        },
                        new
                        {
                            Id = 6,
                            Abreviatura = "DNI",
                            Activo = true,
                            Dependencia = 3,
                            Nivel = 2,
                            Nombre = "DOCUMENTO NACIONAL DE IDENTIDAD",
                            Valor = "1"
                        },
                        new
                        {
                            Id = 7,
                            Abreviatura = "RUC",
                            Activo = true,
                            Dependencia = 4,
                            Nivel = 2,
                            Nombre = "REGISTRO UNICO DE CONTRIBUYENTES",
                            Valor = "1"
                        },
                        new
                        {
                            Id = 8,
                            Abreviatura = "CE",
                            Activo = true,
                            Dependencia = 3,
                            Nivel = 2,
                            Nombre = "CARNET DE EXTRANJERIA",
                            Valor = "2"
                        },
                        new
                        {
                            Id = 9,
                            Activo = true,
                            Nivel = 0,
                            Nombre = "TIPO DE ROL"
                        },
                        new
                        {
                            Id = 10,
                            Activo = true,
                            Dependencia = 9,
                            Nivel = 1,
                            Nombre = "LICENCIA",
                            Valor = "1"
                        },
                        new
                        {
                            Id = 11,
                            Activo = true,
                            Dependencia = 9,
                            Nivel = 1,
                            Nombre = "ADMINISTRADOR",
                            Valor = "2"
                        },
                        new
                        {
                            Id = 12,
                            Activo = true,
                            Nivel = 0,
                            Nombre = "PERIODO DE LICENCIAS"
                        },
                        new
                        {
                            Id = 13,
                            Activo = true,
                            Dependencia = 12,
                            Nivel = 1,
                            Nombre = "1 MES",
                            Valor = "1"
                        },
                        new
                        {
                            Id = 14,
                            Activo = true,
                            Dependencia = 12,
                            Nivel = 1,
                            Nombre = "6 MESES",
                            Valor = "2"
                        },
                        new
                        {
                            Id = 15,
                            Activo = true,
                            Dependencia = 12,
                            Nivel = 1,
                            Nombre = "12 MESES",
                            Valor = "3"
                        });
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.Persona", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TipoDocumentoId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoDocumentoId");

                    b.HasIndex("TipoId");

                    b.ToTable("personas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.PersonasJuridicas.PersonaJuridica", b =>
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

            modelBuilder.Entity("MsAcceso.Domain.Root.PersonasNaturales.PersonaNatural", b =>
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

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisos.RolPermiso", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RolId");

                    b.ToTable("rols_permisos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisosOpciones.RolPermisoOpcion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OpcionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RolPermisoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OpcionId");

                    b.HasIndex("RolPermisoId");

                    b.ToTable("rols_permisos_opciones", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Rols.Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LicenciaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TipoRolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LicenciaId");

                    b.HasIndex("TipoRolId");

                    b.ToTable("rols", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Sistemas.Sistema", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Dependencia")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Orden")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.ToTable("sistemas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Users.User", b =>
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

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid?>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmpresaId");

                    b.HasIndex("RolId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.UsuarioLicencias.UsuarioLicencia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LicenciaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LicenciaId");

                    b.HasIndex("UserId");

                    b.ToTable("usuario_licencia", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.MenuOpciones.MenuOpcion", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", "Menu")
                        .WithMany("MenuOpcions")
                        .HasForeignKey("MenusId");

                    b.HasOne("MsAcceso.Domain.Root.Opciones.Opcion", "Opcion")
                        .WithMany()
                        .HasForeignKey("OpcionesId");

                    b.Navigation("Menu");

                    b.Navigation("Opcion");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Parametros.Parametro", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Parametros.Parametro", null)
                        .WithMany()
                        .HasForeignKey("Dependencia");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.Persona", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Parametros.Parametro", "TipoDocumento")
                        .WithMany()
                        .HasForeignKey("TipoDocumentoId");

                    b.HasOne("MsAcceso.Domain.Root.Parametros.Parametro", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId");

                    b.Navigation("Tipo");

                    b.Navigation("TipoDocumento");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.PersonasJuridicas.PersonaJuridica", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.Persona", null)
                        .WithOne("PersonaJuridica")
                        .HasForeignKey("MsAcceso.Domain.Root.PersonasJuridicas.PersonaJuridica", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.PersonasNaturales.PersonaNatural", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.Persona", null)
                        .WithOne("PersonaNatural")
                        .HasForeignKey("MsAcceso.Domain.Root.PersonasNaturales.PersonaNatural", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisos.RolPermiso", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", "Menu")
                        .WithMany("RolPermisos")
                        .HasForeignKey("MenuId");

                    b.HasOne("MsAcceso.Domain.Root.Rols.Rol", "Rol")
                        .WithMany("RolPermisos")
                        .HasForeignKey("RolId");

                    b.Navigation("Menu");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisosOpciones.RolPermisoOpcion", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Opciones.Opcion", "Opcion")
                        .WithMany("RolPermisoOpcions")
                        .HasForeignKey("OpcionId");

                    b.HasOne("MsAcceso.Domain.Root.RolPermisos.RolPermiso", "RolPermiso")
                        .WithMany("RolPermisoOpcions")
                        .HasForeignKey("RolPermisoId");

                    b.Navigation("Opcion");

                    b.Navigation("RolPermiso");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Rols.Rol", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Licencias.Licencia", "Licencia")
                        .WithMany()
                        .HasForeignKey("LicenciaId");

                    b.HasOne("MsAcceso.Domain.Root.Parametros.Parametro", "TipoRol")
                        .WithMany()
                        .HasForeignKey("TipoRolId");

                    b.Navigation("Licencia");

                    b.Navigation("TipoRol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Sistemas.Sistema", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", "DependenciaModel")
                        .WithMany("Sistemas")
                        .HasForeignKey("Dependencia");

                    b.Navigation("DependenciaModel");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Users.User", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.Persona", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.HasOne("MsAcceso.Domain.Root.Rols.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Empresa");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.UsuarioLicencias.UsuarioLicencia", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Licencias.Licencia", "Licencia")
                        .WithMany()
                        .HasForeignKey("LicenciaId");

                    b.HasOne("MsAcceso.Domain.Root.Users.User", "User")
                        .WithMany("UsuarioLicencias")
                        .HasForeignKey("UserId");

                    b.Navigation("Licencia");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Opciones.Opcion", b =>
                {
                    b.Navigation("RolPermisoOpcions");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.Persona", b =>
                {
                    b.Navigation("PersonaJuridica");

                    b.Navigation("PersonaNatural");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisos.RolPermiso", b =>
                {
                    b.Navigation("RolPermisoOpcions");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Rols.Rol", b =>
                {
                    b.Navigation("RolPermisos");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Sistemas.Sistema", b =>
                {
                    b.Navigation("MenuOpcions");

                    b.Navigation("RolPermisos");

                    b.Navigation("Sistemas");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Users.User", b =>
                {
                    b.Navigation("UsuarioLicencias");
                });
#pragma warning restore 612, 618
        }
    }
}
