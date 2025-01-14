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
                    { new Guid("1228da04-b442-4ec5-8a60-df82a17418df"), true, "Supervisor de obra", 2136 },
                    { new Guid("1dbf744a-8237-4175-a060-6f16b15f68a4"), true, "Seguro de maquinaria", 2137 },
                    { new Guid("2209e5ef-4854-4646-978b-91b57af38efc"), true, "Camión de volteo", 2139 },
                    { new Guid("2529b3fa-1281-4654-825e-e9df7a472c32"), true, "Flete de materiales", 2139 },
                    { new Guid("320b28fe-2532-4abf-b2a8-61e886c815ca"), true, "Andamios", 2125 },
                    { new Guid("3370db65-9d12-4720-a0f4-95730f7dcf9b"), true, "Consultoría técnica", 2136 },
                    { new Guid("35dc82bd-28a0-45f8-aad4-d307e10d14ee"), true, "Bloques de concreto", 2125 },
                    { new Guid("3c26fbbf-8255-4135-8e0e-bd83817cdbb0"), true, "Renta de camión", 2139 },
                    { new Guid("3e579d06-ff7a-49f4-b318-51264b6ffa56"), true, "Equipo de protección personal", 2125 },
                    { new Guid("40e70a85-05f1-42a3-851f-142413e78f74"), true, "Cables eléctricos", 2132 },
                    { new Guid("414e612c-fa89-45ec-a714-2aa7f9a4bd58"), true, "Electricidad", 2136 },
                    { new Guid("4569ab24-bfb9-433e-8e34-c6d9a5c5091d"), true, "Operador de maquinaria", 2136 },
                    { new Guid("4cfc70f0-c5d0-461c-b822-de68a9d28388"), true, "Capataz", 2125 },
                    { new Guid("4d960426-bfb9-40da-8beb-f6c17353abed"), true, "Seguridad en obra", 2136 },
                    { new Guid("53765a42-1e1d-4011-9997-3a8588bf10ad"), true, "Gas", 2129 },
                    { new Guid("5f697e5a-02ac-4eba-b9ee-513035887b22"), true, "Albañil", 2136 },
                    { new Guid("67b388c4-d4c4-425e-8015-303a5899b311"), true, "Arena", 2131 },
                    { new Guid("691cddd2-b2ca-422a-8927-e885593f0afa"), true, "Alquiler de oficinas", 2137 },
                    { new Guid("75069a22-95fe-4460-aa89-229c0f53cce0"), true, "Polvo de mármol", 2126 },
                    { new Guid("87880d95-f5a0-4d0f-affc-db39fdc03af9"), true, "Pintura", 2129 },
                    { new Guid("896da2e7-8695-4e90-84f4-45887e1c9d9d"), true, "Varillas de acero", 2132 },
                    { new Guid("8e37f6be-142d-4d09-9886-a2a50e11cae2"), true, "Lonas protectoras", 2125 },
                    { new Guid("a459541c-f4e6-445d-bc44-b405871cb8fb"), true, "Sierra eléctrica", 2135 },
                    { new Guid("a76977ae-a133-428e-bb50-6420e973151d"), true, "Transporte personal", 2137 },
                    { new Guid("ab3bdac9-d87c-48f1-ab7e-0b6cc00c5751"), true, "Renta de grúa", 2135 },
                    { new Guid("ade6b8d0-5073-415a-a970-e2e8c99969e3"), true, "Excavadora", 2135 },
                    { new Guid("b2eb2d03-b324-4e27-9d70-7a6ece006ee1"), true, "Clavos", 2125 },
                    { new Guid("bbf08a63-a1dc-446d-8e61-e2222d27d5db"), true, "Compactadora", 2135 },
                    { new Guid("c1547ab0-adc6-4e21-ac63-a4965a145917"), true, "Grava", 2131 },
                    { new Guid("c45527cc-daba-45c4-a2c8-daf83fc1ee87"), true, "Agua potable", 2136 },
                    { new Guid("c532d6e6-fbee-48d4-80fd-597adc624ef1"), true, "Yeso", 2126 },
                    { new Guid("c7b49729-daf6-4217-bed6-39b60b1002e0"), true, "Taladro eléctrico", 2135 },
                    { new Guid("c8d00de7-d372-4321-8a6c-2556edf006b7"), true, "Ayudante general", 2136 },
                    { new Guid("d528b30b-4c26-40fa-9afa-7822dfcc50d6"), true, "Madera para cimbra", 2132 },
                    { new Guid("e3bb32e1-cddc-48d0-a3d0-74bd2531e84e"), true, "Ladrillos", 2125 },
                    { new Guid("ec493adb-ab9c-46f8-bf72-7c9ff0c6e879"), true, "Llaves inglesas", 2125 },
                    { new Guid("ee738887-4840-4744-bf0a-6ebfd7a30fde"), true, "Tubos de PVC", 2132 },
                    { new Guid("f14da81f-da45-4687-817f-f4f3f52bd5e7"), true, "Cemento", 2126 },
                    { new Guid("f4c5c60b-6dc9-47f5-b001-b15ed58b263e"), true, "Técnico eléctrico", 2134 },
                    { new Guid("faf2d007-1e64-4f70-bf3b-eb27695b3c0b"), true, "Supervisión de obra externa", 2136 },
                    { new Guid("fcbc3c57-19af-45c2-9ad1-996b3977e535"), true, "Martillos", 2125 }
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
