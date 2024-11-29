using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "especialidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "partidas",
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
                    TipoRecursoId = table.Column<int>(type: "int", nullable: false),
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
                name: "ubigeos",
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
                    table.PrimaryKey("PK_ubigeos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ubigeos_ubigeos_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "ubigeos",
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
                name: "presupuestos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UbigeoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plazodias = table.Column<int>(type: "int", nullable: false),
                    JornadaDiariaId = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<int>(type: "int", nullable: false),
                    PresupuestoBaseCD = table.Column<double>(type: "float", nullable: false),
                    PresupuestoBaseDI = table.Column<double>(type: "float", nullable: false),
                    TotalPresupuestoBase = table.Column<double>(type: "float", nullable: false),
                    PresupuestoOfertaCD = table.Column<double>(type: "float", nullable: false),
                    PresupuestoOfertaDI = table.Column<double>(type: "float", nullable: false),
                    TotalPresupuestoOferta = table.Column<double>(type: "float", nullable: false),
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
                        name: "FK_presupuestos_personas_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "personas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_presupuestos_ubigeos_UbigeoId",
                        column: x => x.UbigeoId,
                        principalTable: "ubigeos",
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
                name: "presupuesto_especialidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresupuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EspecialidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Correlativo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuesto_especialidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_presupuesto_especialidad_especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_presupuesto_especialidad_presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_presupuestales_Dependencia",
                table: "carpetas_presupuestales",
                column: "Dependencia");

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
                name: "IX_presupuesto_especialidad_EspecialidadId",
                table: "presupuesto_especialidad",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuesto_especialidad_PresupuestoId",
                table: "presupuesto_especialidad",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_CarpetaPresupuestalId",
                table: "presupuestos",
                column: "CarpetaPresupuestalId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_ClienteId",
                table: "presupuestos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_presupuestos_UbigeoId",
                table: "presupuestos",
                column: "UbigeoId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_RolId",
                table: "rols_permisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_opciones_RolPermisoId",
                table: "rols_permisos_opciones",
                column: "RolPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_ubigeos_Dependencia",
                table: "ubigeos",
                column: "Dependencia");

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
                name: "presupuesto_especialidad");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "titulos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "partidas");

            migrationBuilder.DropTable(
                name: "recursos");

            migrationBuilder.DropTable(
                name: "especialidades");

            migrationBuilder.DropTable(
                name: "presupuestos");

            migrationBuilder.DropTable(
                name: "rols_permisos");

            migrationBuilder.DropTable(
                name: "carpetas_presupuestales");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "ubigeos");

            migrationBuilder.DropTable(
                name: "rols");
        }
    }
}
