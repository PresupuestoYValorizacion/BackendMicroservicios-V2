using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MsAcceso.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class InitialTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auditorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tabla = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Campo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Accion = table.Column<int>(type: "int", nullable: false),
                    ValorAnterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorActual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Dependencia = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parametros_parametros_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "parametros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sistemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dependencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sistemas_sistemas_Dependencia",
                        column: x => x.Dependencia,
                        principalTable: "sistemas",
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
                    table.ForeignKey(
                        name: "FK_personas_parametros_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "parametros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_personas_parametros_TipoId",
                        column: x => x.TipoId,
                        principalTable: "parametros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "menus_opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpcionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus_opciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menus_opciones_opciones_OpcionId",
                        column: x => x.OpcionId,
                        principalTable: "opciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_menus_opciones_sistemas_MenuId",
                        column: x => x.MenuId,
                        principalTable: "sistemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SistemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_sistemas_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "sistemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "empresas_sistemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SistemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas_sistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empresas_sistemas_personas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "personas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_empresas_sistemas_sistemas_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "sistemas",
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
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_personas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rols_permisos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_rols_permisos_sistemas_MenuId",
                        column: x => x.MenuId,
                        principalTable: "sistemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rols_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_usuarios_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rols_usuarios_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rols_permisos_opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolPermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpcionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols_permisos_opciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_permisos_opciones_opciones_OpcionId",
                        column: x => x.OpcionId,
                        principalTable: "opciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rols_permisos_opciones_rols_permisos_RolPermisoId",
                        column: x => x.RolPermisoId,
                        principalTable: "rols_permisos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "parametros",
                columns: new[] { "Id", "Abreviatura", "Activo", "Dependencia", "Descripcion", "Nivel", "Nombre", "Valor" },
                values: new object[,]
                {
                    { 1, null, true, null, null, 0, "ESTADO DE SOLICITUDES", null },
                    { 2, null, true, null, null, 0, "TIPO DE PERSONA", null },
                    { 5, null, true, null, null, 0, "TIPO DE ASUNTO", null },
                    { 3, null, true, 2, null, 1, "NATURAL", "1" },
                    { 4, null, true, 2, null, 1, "JURIDICA", "2" },
                    { 6, "DNI", true, 3, null, 2, "DOCUMENTO NACIONAL DE IDENTIDAD", "1" },
                    { 7, "RUC", true, 4, null, 2, "REGISTRO UNICO DE CONTRIBUYENTES", "1" },
                    { 8, "CE", true, 3, null, 2, "CARNET DE EXTRANJERIA", "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_empresas_sistemas_EmpresaId",
                table: "empresas_sistemas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_sistemas_SistemaId",
                table: "empresas_sistemas",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_menus_opciones_MenuId",
                table: "menus_opciones",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_menus_opciones_OpcionId",
                table: "menus_opciones",
                column: "OpcionId");

            migrationBuilder.CreateIndex(
                name: "IX_parametros_Dependencia",
                table: "parametros",
                column: "Dependencia");

            migrationBuilder.CreateIndex(
                name: "IX_personas_TipoDocumentoId",
                table: "personas",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_personas_TipoId",
                table: "personas",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_SistemaId",
                table: "rols",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_MenuId",
                table: "rols_permisos",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_RolId",
                table: "rols_permisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_opciones_OpcionId",
                table: "rols_permisos_opciones",
                column: "OpcionId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_permisos_opciones_RolPermisoId",
                table: "rols_permisos_opciones",
                column: "RolPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_usuarios_RolId",
                table: "rols_usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_usuarios_UserId",
                table: "rols_usuarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_sistemas_Dependencia",
                table: "sistemas",
                column: "Dependencia");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_EmpresaId",
                table: "users",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auditorias");

            migrationBuilder.DropTable(
                name: "empresas_sistemas");

            migrationBuilder.DropTable(
                name: "menus_opciones");

            migrationBuilder.DropTable(
                name: "personas_juridicas");

            migrationBuilder.DropTable(
                name: "personas_naturales");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "rols_usuarios");

            migrationBuilder.DropTable(
                name: "opciones");

            migrationBuilder.DropTable(
                name: "rols_permisos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rols");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "sistemas");

            migrationBuilder.DropTable(
                name: "parametros");
        }
    }
}
