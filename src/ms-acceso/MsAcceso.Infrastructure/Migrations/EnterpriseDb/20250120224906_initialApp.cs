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
                    TipoRecursoId = table.Column<int>(type: "int", nullable: false),
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
                    TipoCuadrilla = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "Activo", "Nombre", "TipoRecursoId", "UnidadMedidaId" },
                values: new object[,]
                {
                    { new Guid("1d10c177-79c1-4901-9b7c-b43b3f90a1ad"), true, "Arena", 2125, 2128 },
                    { new Guid("4b08ed27-bf0a-408c-8c30-5ea1fa7ae308"), true, "Clavos", 2125, 2130 },
                    { new Guid("5d46b2fe-75ad-4f81-9354-64e1057afff1"), true, "Cemento", 2125, 2127 },
                    { new Guid("5d5bb9c6-b261-4b6b-bb50-763bc0d99253"), true, "Pintura", 2125, 2131 },
                    { new Guid("81ef5a42-def1-4ff3-bdf9-f06930c5505f"), true, "Ayudante", 2127, 2133 },
                    { new Guid("8f63095c-f4c4-469e-ba7e-53fd91f14546"), true, "Tubería PVC", 2125, 2132 },
                    { new Guid("a3138ec8-d562-41cd-85a8-7cc9cf064719"), true, "Ladrillos", 2125, 2129 },
                    { new Guid("cdc786d0-a000-47d9-abcb-0243e1ee371c"), true, "Obrero", 2126, 2133 },
                    { new Guid("d8e73375-b997-4118-b22c-b126ed63090e"), true, "Capataz", 2127, 2133 },
                    { new Guid("fc9c0c77-6339-4aab-bb75-68abaf7ca289"), true, "Ingeniero Residente", 2127, 2134 }
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
