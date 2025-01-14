using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MsAcceso.Infrastructure.Migrations.EnterpriseDb
{
    /// <inheritdoc />
    public partial class initialApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carpetas_presupuestales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dependencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpetas_presupuestales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carpetas_presupuestales_carpetas_presupuestales_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "carpetas_presupuestales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPersonaId = table.Column<int>(type: "int", nullable: true),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: true),
                    TipoClienteId = table.Column<int>(type: "int", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dependencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Correlativo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partidas_partidas_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "partidas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: true),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "recursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "titulos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "presupuestos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    ProvinciaId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plazodias = table.Column<int>(type: "int", nullable: false),
                    JornadaDiariaId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    PresupuestoBaseCD = table.Column<double>(type: "float", nullable: true),
                    PresupuestoBaseDI = table.Column<double>(type: "float", nullable: true),
                    TotalPresupuestoBase = table.Column<double>(type: "float", nullable: true),
                    PresupuestoOfertaCD = table.Column<double>(type: "float", nullable: true),
                    PresupuestoOfertaDI = table.Column<double>(type: "float", nullable: true),
                    TotalPresupuestoOferta = table.Column<double>(type: "float", nullable: true),
                    CarpetaPresupuestalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presupuestos_carpetas_presupuestales_CarpetaPresupuestalId",
                        column: x => x.CarpetaPresupuestalId,
                        principalTable: "carpetas_presupuestales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_presupuestos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "personas_juridicas",
                columns: table => new
                {
                    PersonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas_juridicas", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_personas_juridicas_personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personas_naturales",
                columns: table => new
                {
                    PersonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas_naturales", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_personas_naturales_personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partida_recurso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Cuadrilla = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Parcial = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partida_recurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partida_recurso_partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partida_recurso_recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rols_permisos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols_permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_permisos_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PersonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "personas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_users_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "proyectos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correlativo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresupuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_proyectos_presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "presupuestos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rols_permisos_opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolPermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpcionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols_permisos_opciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_permisos_opciones_rols_permisos_RolPermisoId",
                        column: x => x.RolPermisoId,
                        principalTable: "rols_permisos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "especialidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProyectoTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_especialidades_proyectos_ProyectoTenantId",
                        column: x => x.ProyectoTenantId,
                        principalTable: "proyectos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "presupuestos_especialidad_titulos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EspecialidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TituloId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Dependencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: true),
                    Correlativo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuestos_especialidad_titulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "especialidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_presupuestos_especialidad_titulos_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "presupuestos_especialidad_titulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_titulos_TituloId",
                        column: x => x.TituloId,
                        principalTable: "titulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "presupuestos_especialidad_titulos_partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresupuestoEspecialidadTituloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuestos_especialidad_titulos_partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_partidas_partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_partidas_presupuestos_especialidad_titulos_PresupuestoEspecialidadTituloId",
                        column: x => x.PresupuestoEspecialidadTituloId,
                        principalTable: "presupuestos_especialidad_titulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "presupuestos_especialidad_titulos_partidas_recursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresupuestoEspecialidadTituloPartidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Cuadrilla = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Parcial = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuestos_especialidad_titulos_partidas_recursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_partidas_recursos_presupuestos_especialidad_titulos_partidas_PresupuestoEspecialidadTitulo~",
                        column: x => x.PresupuestoEspecialidadTituloPartidaId,
                        principalTable: "presupuestos_especialidad_titulos_partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_presupuestos_especialidad_titulos_partidas_recursos_recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "recursos",
                columns: new[] { "Id", "Activo", "Nombre", "UnidadMedidaId" },
                values: new object[,]
                {
                    { new Guid("04d246a3-9f06-450c-a12c-4dc99d09ee0e"), true, "Grava", 2131 },
                    { new Guid("067232ff-7a1f-460c-87a8-edc21f38c35d"), true, "Excavadora", 2135 },
                    { new Guid("07e1236f-f8ad-4f27-b61c-44a5a0b050ac"), true, "Madera para cimbra", 2132 },
                    { new Guid("0f54c6e1-edea-46b2-b122-70e6f136740e"), true, "Bloques de concreto", 2125 },
                    { new Guid("1cecf065-11e9-4bb6-8636-84baa70314f9"), true, "Yeso", 2126 },
                    { new Guid("1ebca2c8-93cf-43b9-a4f0-19b220039a50"), true, "Llaves inglesas", 2125 },
                    { new Guid("2160cad6-aae5-4878-ab1d-f6a9a6f0b235"), true, "Renta de grúa", 2135 },
                    { new Guid("2dfaac81-2c85-4372-b939-3a1861a16aae"), true, "Arena", 2131 },
                    { new Guid("2ea60fcd-641e-4137-9ee1-66c893bac324"), true, "Supervisor de obra", 2136 },
                    { new Guid("322f473d-7872-4ea2-bebd-acdfca641742"), true, "Andamios", 2125 },
                    { new Guid("34e8e903-4f21-4521-b6e2-de42802cfc77"), true, "Martillos", 2125 },
                    { new Guid("3750dfd6-7e49-4dd0-9440-201b3aba82dc"), true, "Compactadora", 2135 },
                    { new Guid("447c328d-824d-4681-9eb7-69dc1f3ce097"), true, "Albañil", 2136 },
                    { new Guid("50bb98ed-15a0-41c7-908b-61b515d64eac"), true, "Ayudante general", 2136 },
                    { new Guid("56c6056e-16d6-4983-bd03-bf293ff8e28e"), true, "Varillas de acero", 2132 },
                    { new Guid("5990618a-fb26-429a-95cb-4c6f33767f66"), true, "Tubos de PVC", 2132 },
                    { new Guid("5a8c4259-34c7-49bb-ae79-5213bfd99db5"), true, "Capataz", 2125 },
                    { new Guid("655d8fca-8b41-4b4e-9ed7-0dbbf320389b"), true, "Operador de maquinaria", 2136 },
                    { new Guid("6c158400-762a-4e4e-af37-b8f8b5b08eec"), true, "Polvo de mármol", 2126 },
                    { new Guid("6d64d687-580d-493a-934a-4d8e78ec8051"), true, "Consultoría técnica", 2136 },
                    { new Guid("7bd32b3e-a947-453c-99a8-154787eb7a63"), true, "Seguridad en obra", 2136 },
                    { new Guid("866953f5-f664-4859-b120-e3f8052d9f19"), true, "Equipo de protección personal", 2125 },
                    { new Guid("8b564c03-291c-4236-a7eb-67d098d4bfb2"), true, "Técnico eléctrico", 2134 },
                    { new Guid("904d2736-246d-491a-9e6c-783522e0c89d"), true, "Sierra eléctrica", 2135 },
                    { new Guid("9207b4c7-8a35-44d2-95fd-f20b7da2394c"), true, "Ladrillos", 2125 },
                    { new Guid("96befe60-fe49-4762-bab3-46d895c1d58e"), true, "Supervisión de obra externa", 2136 },
                    { new Guid("a615ba72-0cd5-4f54-9918-f95366e337f0"), true, "Flete de materiales", 2139 },
                    { new Guid("b3843a3f-87d3-40dc-aee0-6402e856c2e9"), true, "Cables eléctricos", 2132 },
                    { new Guid("b89841d2-8d78-49c3-a176-122410712a57"), true, "Transporte personal", 2137 },
                    { new Guid("bb18f871-c4e3-4b76-837b-35513eacf703"), true, "Agua potable", 2136 },
                    { new Guid("bf90017e-1509-463e-a6db-5d1621c80aa5"), true, "Cemento", 2126 },
                    { new Guid("c71daa9a-6b53-402b-babd-92fcdc83987f"), true, "Electricidad", 2136 },
                    { new Guid("d13f4e68-ab7a-46ca-a83a-19869d793329"), true, "Renta de camión", 2139 },
                    { new Guid("df009e1f-e45f-4619-95c1-13e4dd5e0b38"), true, "Taladro eléctrico", 2135 },
                    { new Guid("e3ab9aee-e3de-49cb-a6bf-192422d0247f"), true, "Pintura", 2129 },
                    { new Guid("e9019bd4-603d-41ef-94cf-feb1d2ef5c6e"), true, "Clavos", 2125 },
                    { new Guid("ef10754f-41e1-42f4-a8f1-15d173ea3ab2"), true, "Camión de volteo", 2139 },
                    { new Guid("f6cd0f43-7acf-43e9-b8d5-6fccaa52cbeb"), true, "Seguro de maquinaria", 2137 },
                    { new Guid("f7d8a2e1-b48e-4b1e-8152-53bbf78cb1a5"), true, "Alquiler de oficinas", 2137 },
                    { new Guid("fab16ec0-ea10-4ae0-91b9-cbe18aefa715"), true, "Lonas protectoras", 2125 },
                    { new Guid("fd2b9f15-1b4c-42d5-a880-e05be7ea5ba4"), true, "Gas", 2129 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_presupuestales_Dependencia",
                table: "carpetas_presupuestales",
                column: "Dependencia");

            migrationBuilder.CreateIndex(
                name: "IX_especialidades_ProyectoTenantId",
                table: "especialidades",
                column: "ProyectoTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_partida_recurso_PartidaId",
                table: "partida_recurso",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_partida_recurso_RecursoId",
                table: "partida_recurso",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_partidas_Dependencia",
                table: "partidas",
                column: "Dependencia");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_CarpetaPresupuestalId",
                table: "presupuestos",
                column: "CarpetaPresupuestalId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_ClienteId",
                table: "presupuestos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_Dependencia",
                table: "presupuestos_especialidad_titulos",
                column: "Dependencia");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_EspecialidadId",
                table: "presupuestos_especialidad_titulos",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_TituloId",
                table: "presupuestos_especialidad_titulos",
                column: "TituloId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_partidas_PartidaId",
                table: "presupuestos_especialidad_titulos_partidas",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_partidas_PresupuestoEspecialidadTituloId",
                table: "presupuestos_especialidad_titulos_partidas",
                column: "PresupuestoEspecialidadTituloId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_partidas_recursos_PresupuestoEspecialidadTituloPartidaId",
                table: "presupuestos_especialidad_titulos_partidas_recursos",
                column: "PresupuestoEspecialidadTituloPartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_especialidad_titulos_partidas_recursos_RecursoId",
                table: "presupuestos_especialidad_titulos_partidas_recursos",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_proyectos_PresupuestoId",
                table: "proyectos",
                column: "PresupuestoId",
                unique: true,
                filter: "[PresupuestoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_RolId",
                table: "rols_permisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_opciones_RolPermisoId",
                table: "rols_permisos_opciones",
                column: "RolPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PersonaId",
                table: "users",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RolId",
                table: "users",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partida_recurso");

            migrationBuilder.DropTable(
                name: "personas_juridicas");

            migrationBuilder.DropTable(
                name: "personas_naturales");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos_partidas_recursos");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos_partidas");

            migrationBuilder.DropTable(
                name: "recursos");

            migrationBuilder.DropTable(
                name: "rols_permisos");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "partidas");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos");

            migrationBuilder.DropTable(
                name: "rols");

            migrationBuilder.DropTable(
                name: "especialidades");

            migrationBuilder.DropTable(
                name: "titulos");

            migrationBuilder.DropTable(
                name: "proyectos");

            migrationBuilder.DropTable(
                name: "presupuestos");

            migrationBuilder.DropTable(
                name: "carpetas_presupuestales");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
