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
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    { new Guid("0bf526c9-7aef-41c4-855f-9bdc2914fc53"), true, "Transporte personal", 2137 },
                    { new Guid("0c9c1250-1b25-42cf-8b4a-1472acb908a4"), true, "Cemento", 2126 },
                    { new Guid("116d904e-ca56-4e21-a57c-58cb13f01ca1"), true, "Consultoría técnica", 2136 },
                    { new Guid("12864c2c-86ed-42f7-a47e-de419d93410d"), true, "Camión de volteo", 2139 },
                    { new Guid("1812353c-c46e-490f-9456-dfe6c764a0c4"), true, "Albañil", 2136 },
                    { new Guid("190dd940-58cc-4abe-8482-2ca53d4cb510"), true, "Bloques de concreto", 2125 },
                    { new Guid("1b2ad3b5-1aaa-44ce-9537-ae7bce7aed05"), true, "Madera para cimbra", 2132 },
                    { new Guid("1c011f29-4784-4c92-b517-4fb9f9ead63a"), true, "Supervisión de obra externa", 2136 },
                    { new Guid("2018e9b6-8fe2-4b1c-8582-b93bcb7be381"), true, "Pintura", 2129 },
                    { new Guid("279a8d64-1967-4420-8c17-3a07d72e170d"), true, "Ladrillos", 2125 },
                    { new Guid("2c3a82ba-c35b-404f-b20a-d89a3a6abcea"), true, "Polvo de mármol", 2126 },
                    { new Guid("32480dba-bc17-4868-99dd-4c9510f4fb9f"), true, "Cables eléctricos", 2132 },
                    { new Guid("3cb15773-b803-4c2c-b3a7-f025223d587c"), true, "Yeso", 2126 },
                    { new Guid("3ea028f7-45cd-4f5d-82f3-ba56e5717c24"), true, "Varillas de acero", 2132 },
                    { new Guid("4c84bc89-5905-4c96-9223-77665bd5cc1c"), true, "Grava", 2131 },
                    { new Guid("511dd8bd-291b-4e99-83c9-1f8d9da0afdd"), true, "Seguridad en obra", 2136 },
                    { new Guid("561630df-f7cb-406d-8e69-a682f45b8915"), true, "Taladro eléctrico", 2135 },
                    { new Guid("60bef5cf-7cfd-411f-aa04-849984a3bd8b"), true, "Supervisor de obra", 2136 },
                    { new Guid("60f42d2e-731b-4a48-8c5c-6fd3f9c2aaa2"), true, "Capataz", 2125 },
                    { new Guid("64d34d40-6deb-4a69-91f4-40758eecfee5"), true, "Ayudante general", 2136 },
                    { new Guid("66866359-146f-4326-9396-dfb900967039"), true, "Equipo de protección personal", 2125 },
                    { new Guid("6db7b9cd-47d0-4fe8-a988-304113c44097"), true, "Agua potable", 2136 },
                    { new Guid("71407e44-372b-4707-8a78-53f38b7f052c"), true, "Arena", 2131 },
                    { new Guid("730fcba5-4f01-4ddb-b40a-de12a2cd3960"), true, "Electricidad", 2136 },
                    { new Guid("752e5343-71a9-45d6-b171-3dc689af396b"), true, "Flete de materiales", 2139 },
                    { new Guid("973b2d83-501f-4f78-a9e7-0e1284b2a4c6"), true, "Lonas protectoras", 2125 },
                    { new Guid("9af05731-02af-4727-8a79-8a11c34f0508"), true, "Martillos", 2125 },
                    { new Guid("9d650c8d-6cda-4a1b-a55a-0aae00f38994"), true, "Sierra eléctrica", 2135 },
                    { new Guid("9fe99a06-4787-42f2-b875-d981a3512cd9"), true, "Clavos", 2125 },
                    { new Guid("a2b7c2f0-8521-4731-806f-d6ae1070cbd2"), true, "Andamios", 2125 },
                    { new Guid("a4e6a78f-4b33-4388-b2f4-7eb172c82e5a"), true, "Operador de maquinaria", 2136 },
                    { new Guid("bc12556d-5195-4320-ac0b-105ad5f19016"), true, "Excavadora", 2135 },
                    { new Guid("bc8499f7-4619-4f74-8220-d61522a35a0d"), true, "Alquiler de oficinas", 2137 },
                    { new Guid("ccc3647f-b096-426c-a61a-e72d532ab23c"), true, "Renta de grúa", 2135 },
                    { new Guid("d8dd669c-67b3-476e-b0b0-5f3f607adac8"), true, "Llaves inglesas", 2125 },
                    { new Guid("de64fa6b-345a-43fb-ae8b-34d9b21f0deb"), true, "Gas", 2129 },
                    { new Guid("e1eef79d-bee1-414d-b4d2-658d2ad843d2"), true, "Técnico eléctrico", 2134 },
                    { new Guid("ea4bb32d-df86-4295-a4d6-1af9b9413b2b"), true, "Tubos de PVC", 2132 },
                    { new Guid("ececd90e-bce5-4b1f-8245-c37c1a9c3b90"), true, "Seguro de maquinaria", 2137 },
                    { new Guid("f735b650-1bad-4056-aeeb-01c4b98fe5b5"), true, "Compactadora", 2135 },
                    { new Guid("f8345dad-4f6c-4216-8e4c-030f1c24f063"), true, "Renta de camión", 2139 }
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
