﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MsAcceso.Infrastructure.Tenants;

#nullable disable

namespace MsAcceso.Infrastructure.Migrations.TenantDb
{
    [DbContext(typeof(TenantDbContext))]
    partial class TenantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("MsAcceso.Domain.Root.EmpresasSistemas.EmpresaSistema", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SistemaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("SistemaId");

                    b.ToTable("empresas_sistemas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.MenuOpciones.MenuOpcion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OpcionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("OpcionId");

                    b.ToTable("menus_opciones", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Opciones.Opcion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

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

            modelBuilder.Entity("MsAcceso.Domain.Root.RolUsers.RolUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.HasIndex("UserId");

                    b.ToTable("rols_usuarios", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Rols.Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("SistemaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SistemaId");

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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Nivel")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.HasIndex("Tipo");

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
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmpresaId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.EmpresasSistemas.EmpresaSistema", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.Persona", null)
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", null)
                        .WithMany()
                        .HasForeignKey("SistemaId");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.MenuOpciones.MenuOpcion", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", null)
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("MsAcceso.Domain.Root.Opciones.Opcion", null)
                        .WithMany()
                        .HasForeignKey("OpcionId");
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
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", null)
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("MsAcceso.Domain.Root.Rols.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolId");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolPermisosOpciones.RolPermisoOpcion", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Opciones.Opcion", null)
                        .WithMany()
                        .HasForeignKey("OpcionId");

                    b.HasOne("MsAcceso.Domain.Root.RolPermisos.RolPermiso", null)
                        .WithMany()
                        .HasForeignKey("RolPermisoId");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.RolUsers.RolUser", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Rols.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.HasOne("MsAcceso.Domain.Root.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Rols.Rol", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", null)
                        .WithMany()
                        .HasForeignKey("SistemaId");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Sistemas.Sistema", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Sistemas.Sistema", null)
                        .WithMany()
                        .HasForeignKey("Dependencia");

                    b.HasOne("MsAcceso.Domain.Root.Parametros.Parametro", null)
                        .WithMany()
                        .HasForeignKey("Tipo");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Users.User", b =>
                {
                    b.HasOne("MsAcceso.Domain.Root.Personas.Persona", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("MsAcceso.Domain.Root.Personas.Persona", b =>
                {
                    b.Navigation("PersonaJuridica");

                    b.Navigation("PersonaNatural");
                });
#pragma warning restore 612, 618
        }
    }
}
