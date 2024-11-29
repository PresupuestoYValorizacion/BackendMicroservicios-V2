﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MsAcceso.Infrastructure;

#nullable disable

namespace MsAcceso.Infrastructure.Migrations.EnterpriseDb
{
    [DbContext(typeof(EnterpriseDbContext))]
    partial class EnterpriseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant.CarpetaPresupuestalTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Dependencia")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.ToTable("carpetas_presupuestales", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.EspecialidadesTenant.EspecialidadTenant", b =>
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

                    b.ToTable("especialidades", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PartidasRecursosTenant.PartidaRecursoTenant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int?>("Cantidad")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Cuadrilla")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double?>("Parcial")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid>("PartidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Precio")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid>("RecursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.HasIndex("RecursoId");

                    b.ToTable("partida_recurso", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Dependencia")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.ToTable("partidas", (string)null);
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

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", b =>
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

                    b.ToTable("personas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant.PresupuestoEspecialidadTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Correlativo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("EspecialidadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PresupuestoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.HasIndex("PresupuestoId");

                    b.ToTable("presupuesto_especialidad", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant.PresupuestoEspecialidadTituloPartidaRecursoTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int?>("Cantidad")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Cuadrilla")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double?>("Parcial")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("Precio")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid>("PresupuestoEspecialidadTituloPartidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecursoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PresupuestoEspecialidadTituloPartidaId");

                    b.HasIndex("RecursoId");

                    b.ToTable("presupuestos_especialidad_titulos_partidas_recursos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant.PresupuestoEspecialidadTituloPartidaTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Correlativo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PartidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PresupuestoEspecialidadTituloId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.HasIndex("PresupuestoEspecialidadTituloId");

                    b.ToTable("presupuestos_especialidad_titulos_partidas", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant.PresupuestoEspecialidadTituloTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Correlativo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("Dependencia")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int");

                    b.Property<Guid>("PresupuestoEspecialidadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TituloId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TituloTenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.HasIndex("PresupuestoEspecialidadId");

                    b.HasIndex("TituloId");

                    b.HasIndex("TituloTenantId");

                    b.ToTable("presupuestos_especialidad_titulos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosTenant.PresupuestoTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CarpetaPresupuestalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Fecha")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int?>("JornadaDiariaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("MonedaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Plazodias")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double?>("PresupuestoBaseCD")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("PresupuestoBaseDI")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("PresupuestoOfertaCD")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("PresupuestoOfertaDI")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("TotalPresupuestoBase")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("TotalPresupuestoOferta")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid?>("UbigeoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CarpetaPresupuestalId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UbigeoId");

                    b.ToTable("presupuestos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RecursosTenant.RecursoTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TipoRecursoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("UnidadMedidaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("recursos", (string)null);
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

            modelBuilder.Entity("MsAcceso.Domain.Tenant.TitulosTenant.TituloTenant", b =>
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

                    b.ToTable("titulos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UbigeosTenant.UbigeoTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Dependencia")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Dependencia");

                    b.ToTable("ubigeos", (string)null);
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UsersTenant.UserTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant.CarpetaPresupuestalTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant.CarpetaPresupuestalTenant", "DependenciaModel")
                        .WithMany("CarpetasPresupuestales")
                        .HasForeignKey("Dependencia");

                    b.Navigation("DependenciaModel");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PartidasRecursosTenant.PartidaRecursoTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", "Partida")
                        .WithMany("PartidasRecursos")
                        .HasForeignKey("PartidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.RecursosTenant.RecursoTenant", "Recurso")
                        .WithMany()
                        .HasForeignKey("RecursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partida");

                    b.Navigation("Recurso");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", "DependenciaModel")
                        .WithMany("Partidas")
                        .HasForeignKey("Dependencia");

                    b.Navigation("DependenciaModel");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasJuridicasTenant.PersonaJuridicaTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", null)
                        .WithOne("PersonaJuridica")
                        .HasForeignKey("MsAcceso.Domain.Tenant.PersonasJuridicasTenant.PersonaJuridicaTenant", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasNaturalesTenant.PersonaNaturalTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", null)
                        .WithOne("PersonaNatural")
                        .HasForeignKey("MsAcceso.Domain.Tenant.PersonasNaturalesTenant.PersonaNaturalTenant", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant.PresupuestoEspecialidadTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.EspecialidadesTenant.EspecialidadTenant", "Especialidad")
                        .WithMany("PresupuestosEspecialidades")
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.PresupuestosTenant.PresupuestoTenant", "Presupuesto")
                        .WithMany()
                        .HasForeignKey("PresupuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");

                    b.Navigation("Presupuesto");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant.PresupuestoEspecialidadTituloPartidaRecursoTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant.PresupuestoEspecialidadTituloPartidaTenant", "PresupuestoEspecialidadTituloPartida")
                        .WithMany()
                        .HasForeignKey("PresupuestoEspecialidadTituloPartidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.RecursosTenant.RecursoTenant", "Recurso")
                        .WithMany("PresupuestosEspecialidadesTitulosPartidasRecursos")
                        .HasForeignKey("RecursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PresupuestoEspecialidadTituloPartida");

                    b.Navigation("Recurso");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant.PresupuestoEspecialidadTituloPartidaTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", "Partida")
                        .WithMany("PresupuestosEspecialidadesTitulosPartidas")
                        .HasForeignKey("PartidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant.PresupuestoEspecialidadTituloTenant", "PresupuestoEspecialidadTitulo")
                        .WithMany()
                        .HasForeignKey("PresupuestoEspecialidadTituloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partida");

                    b.Navigation("PresupuestoEspecialidadTitulo");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant.PresupuestoEspecialidadTituloTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant.PresupuestoEspecialidadTituloTenant", "DependenciaModel")
                        .WithMany("PresupuestosEspecialidadTitulos")
                        .HasForeignKey("Dependencia");

                    b.HasOne("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant.PresupuestoEspecialidadTenant", "PresupuestoEspecialidad")
                        .WithMany()
                        .HasForeignKey("PresupuestoEspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.TitulosTenant.TituloTenant", "Titulo")
                        .WithMany()
                        .HasForeignKey("TituloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MsAcceso.Domain.Tenant.TitulosTenant.TituloTenant", null)
                        .WithMany("PresupuestosEspecialidadesTitulos")
                        .HasForeignKey("TituloTenantId");

                    b.Navigation("DependenciaModel");

                    b.Navigation("PresupuestoEspecialidad");

                    b.Navigation("Titulo");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosTenant.PresupuestoTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant.CarpetaPresupuestalTenant", "CarpetaPresupuestal")
                        .WithMany()
                        .HasForeignKey("CarpetaPresupuestalId");

                    b.HasOne("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("MsAcceso.Domain.Tenant.UbigeosTenant.UbigeoTenant", "Ubigeo")
                        .WithMany()
                        .HasForeignKey("UbigeoId");

                    b.Navigation("CarpetaPresupuestal");

                    b.Navigation("Cliente");

                    b.Navigation("Ubigeo");
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

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UbigeosTenant.UbigeoTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.UbigeosTenant.UbigeoTenant", "DependenciaModel")
                        .WithMany("Ubigeos")
                        .HasForeignKey("Dependencia");

                    b.Navigation("DependenciaModel");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UsersTenant.UserTenant", b =>
                {
                    b.HasOne("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId");

                    b.HasOne("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Persona");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant.CarpetaPresupuestalTenant", b =>
                {
                    b.Navigation("CarpetasPresupuestales");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.EspecialidadesTenant.EspecialidadTenant", b =>
                {
                    b.Navigation("PresupuestosEspecialidades");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PartidasTenant.PartidaTenant", b =>
                {
                    b.Navigation("Partidas");

                    b.Navigation("PartidasRecursos");

                    b.Navigation("PresupuestosEspecialidadesTitulosPartidas");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PersonasTenant.PersonaTenant", b =>
                {
                    b.Navigation("PersonaJuridica");

                    b.Navigation("PersonaNatural");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant.PresupuestoEspecialidadTituloTenant", b =>
                {
                    b.Navigation("PresupuestosEspecialidadTitulos");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RecursosTenant.RecursoTenant", b =>
                {
                    b.Navigation("PresupuestosEspecialidadesTitulosPartidasRecursos");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolPermisosTenant.RolPermisoTenant", b =>
                {
                    b.Navigation("RolPermisoOpcions");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.RolsTenant.RolTenant", b =>
                {
                    b.Navigation("RolPermisos");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.TitulosTenant.TituloTenant", b =>
                {
                    b.Navigation("PresupuestosEspecialidadesTitulos");
                });

            modelBuilder.Entity("MsAcceso.Domain.Tenant.UbigeosTenant.UbigeoTenant", b =>
                {
                    b.Navigation("Ubigeos");
                });
#pragma warning restore 612, 618
        }
    }
}
