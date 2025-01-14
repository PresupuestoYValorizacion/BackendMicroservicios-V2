using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MsAcceso.Infrastructure.Migrations.LicenciaDb
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
                    { new Guid("181bd9ce-1edb-4e84-959a-1eb23c2f19a4"), true, "Madera para cimbra", 2132 },
                    { new Guid("27aebd14-6b04-4368-8d21-dfbb6c32be37"), true, "Llaves inglesas", 2125 },
                    { new Guid("29ff03d7-c367-49dc-99de-f8194a954e9f"), true, "Polvo de mármol", 2126 },
                    { new Guid("2b9c348d-0f95-49ac-9fd4-50687fedb30c"), true, "Compactadora", 2135 },
                    { new Guid("2ba93433-d164-47c1-95cc-44f0fea1f76c"), true, "Clavos", 2125 },
                    { new Guid("32ab1623-b9fe-4352-8826-df2becefda31"), true, "Martillos", 2125 },
                    { new Guid("3de59329-8cdd-4484-a782-133353a58868"), true, "Flete de materiales", 2139 },
                    { new Guid("4c8622c8-738d-4018-b21d-ce7f92f8285a"), true, "Arena", 2131 },
                    { new Guid("4ca1e7b4-d6e0-4eef-aa39-af70dc63b62b"), true, "Consultoría técnica", 2136 },
                    { new Guid("52f0dc91-4a8c-4406-b715-c4c91629c70a"), true, "Seguridad en obra", 2136 },
                    { new Guid("591d827f-a6d3-427a-b563-f2ce1e2e4316"), true, "Cables eléctricos", 2132 },
                    { new Guid("5d2b240e-41bb-4010-81b0-e26a12e13fe7"), true, "Grava", 2131 },
                    { new Guid("63e8892a-0d3a-4643-bcc6-348f8ce4422b"), true, "Transporte personal", 2137 },
                    { new Guid("6f4d15a7-28fa-4f30-8e70-473b6f2b8d55"), true, "Yeso", 2126 },
                    { new Guid("72b7ba5e-a82e-464c-bb0b-c472fd0dc929"), true, "Agua potable", 2136 },
                    { new Guid("7b11675e-d66a-4975-8af4-ff7f2cc51e7c"), true, "Bloques de concreto", 2125 },
                    { new Guid("876061f6-2116-422a-8dae-81780fc0c6c9"), true, "Supervisión de obra externa", 2136 },
                    { new Guid("8cb9f863-88a4-48a5-9b61-bde441eedbdb"), true, "Ayudante general", 2136 },
                    { new Guid("8e1fe177-ac6c-4822-8494-66966a28991b"), true, "Camión de volteo", 2139 },
                    { new Guid("9255f3cd-9035-4dc4-9648-162b76708e21"), true, "Varillas de acero", 2132 },
                    { new Guid("97778eb3-f658-485b-a434-d97d1aca52b0"), true, "Renta de grúa", 2135 },
                    { new Guid("9b102b9e-fd1b-4510-a5f5-9c8418caebca"), true, "Ladrillos", 2125 },
                    { new Guid("acfbebd7-4efb-4ebd-b000-8c9e93ed1d10"), true, "Andamios", 2125 },
                    { new Guid("b70043fa-dbdc-4e08-a5bb-a66b85766f77"), true, "Equipo de protección personal", 2125 },
                    { new Guid("b8cf5bdb-8664-45cd-94ca-8b4f32836ac9"), true, "Albañil", 2136 },
                    { new Guid("bda725e2-8fdb-47fd-981c-c5aaaed7e10b"), true, "Excavadora", 2135 },
                    { new Guid("c01427d9-3e20-4e97-94f1-6731a521d8af"), true, "Sierra eléctrica", 2135 },
                    { new Guid("c48b85f4-137e-45b7-bbbb-f7deee44b60e"), true, "Cemento", 2126 },
                    { new Guid("c4b91d25-4d10-4d4e-96f3-f33b5bcc6e33"), true, "Lonas protectoras", 2125 },
                    { new Guid("d2f71cc4-d0b4-41bf-a8b1-5049ea4b24ed"), true, "Técnico eléctrico", 2134 },
                    { new Guid("d3b9cc39-3942-45b4-8aab-02160c2867ab"), true, "Capataz", 2125 },
                    { new Guid("d8520aed-4613-4b81-9826-3f620291347a"), true, "Tubos de PVC", 2132 },
                    { new Guid("d939ef8c-975f-4006-9061-f61b883ec407"), true, "Seguro de maquinaria", 2137 },
                    { new Guid("e42627c8-918b-4446-98fa-503ee1e18468"), true, "Gas", 2129 },
                    { new Guid("e5879232-7e13-441b-8428-e63c6dff0964"), true, "Electricidad", 2136 },
                    { new Guid("e5c17785-c01f-4049-8056-1ff4d71bcd9d"), true, "Alquiler de oficinas", 2137 },
                    { new Guid("eaa5355b-cd55-4f16-91d8-ed6e15e2696e"), true, "Taladro eléctrico", 2135 },
                    { new Guid("eb582231-35ff-4851-9ffc-fbbb7817819b"), true, "Operador de maquinaria", 2136 },
                    { new Guid("f1df947e-039d-4a57-b095-b7f1cae3a878"), true, "Renta de camión", 2139 },
                    { new Guid("f8d324c2-5796-4468-9699-9dffe2fc96b2"), true, "Supervisor de obra", 2136 },
                    { new Guid("fc35c5f1-bad1-4b20-b3a6-3a805017c1d1"), true, "Pintura", 2129 }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partida_recurso");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos_partidas_recursos");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos_partidas");

            migrationBuilder.DropTable(
                name: "recursos");

            migrationBuilder.DropTable(
                name: "partidas");

            migrationBuilder.DropTable(
                name: "presupuestos_especialidad_titulos");

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
