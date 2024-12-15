using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MsAcceso.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class initialApp : Migration
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
                name: "licencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermiteCrearUsuarios = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tooltip = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
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
                name: "sesiones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sistemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dependencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
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
                name: "rols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoRolId = table.Column<int>(type: "int", nullable: true),
                    LicenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rols_licencias_LicenciaId",
                        column: x => x.LicenciaId,
                        principalTable: "licencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rols_parametros_TipoRolId",
                        column: x => x.TipoRolId,
                        principalTable: "parametros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "menus_opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpcionesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TieneUrl = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus_opciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menus_opciones_opciones_OpcionesId",
                        column: x => x.OpcionesId,
                        principalTable: "opciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_menus_opciones_sistemas_MenusId",
                        column: x => x.MenusId,
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
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_users_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
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

            migrationBuilder.CreateTable(
                name: "usuario_licencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LicenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeriodoLicenciaId = table.Column<int>(type: "int", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_licencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_licencia_licencias_LicenciaId",
                        column: x => x.LicenciaId,
                        principalTable: "licencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuario_licencia_parametros_PeriodoLicenciaId",
                        column: x => x.PeriodoLicenciaId,
                        principalTable: "parametros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuario_licencia_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "licencias",
                columns: new[] { "Id", "Activo", "Nombre", "PermiteCrearUsuarios" },
                values: new object[,]
                {
                    { new Guid("1a9e887b-aa55-49b8-b9bc-4d7ba609d065"), true, "PROFESIONAL", false },
                    { new Guid("e88a6456-3941-4136-b172-7a0d5167c7fc"), true, "ESTUDIANTE", false },
                    { new Guid("ecbdebff-cb86-4e74-bd12-f7fbfc165dfb"), true, "ENTERPRISE", true }
                });

            migrationBuilder.InsertData(
                table: "parametros",
                columns: new[] { "Id", "Abreviatura", "Activo", "Dependencia", "Descripcion", "Nivel", "Nombre", "Valor" },
                values: new object[,]
                {
                    { 1, null, true, null, null, 0, "ESTADO DE SOLICITUDES", null },
                    { 2, null, true, null, null, 0, "TIPO DE PERSONA", null },
                    { 5, null, true, null, null, 0, "TIPO DE ASUNTO", null },
                    { 9, null, true, null, null, 0, "TIPO DE ROL", null },
                    { 12, null, true, null, null, 0, "PERIODO DE LICENCIAS", null },
                    { 16, null, true, null, null, 0, "TIPO DE IDENTIFICADOR SGO", null },
                    { 19, null, true, null, null, 0, "TIPO DE RECURSO", null },
                    { 24, null, true, null, null, 0, "UBIGEO", null },
                    { 3, null, true, 2, null, 1, "NATURAL", "1" },
                    { 4, null, true, 2, null, 1, "JURIDICA", "2" },
                    { 10, null, true, 9, null, 1, "LICENCIA", "1" },
                    { 11, null, true, 9, null, 1, "ADMINISTRADOR", "2" },
                    { 13, null, true, 12, null, 1, "1 MES", "1" },
                    { 14, null, true, 12, null, 1, "6 MESES", "6" },
                    { 15, null, true, 12, null, 1, "12 MESES", "12" },
                    { 17, null, true, 16, null, 1, "CLIENTE", "0" },
                    { 18, null, true, 16, null, 1, "PROVEEDOR", "1" },
                    { 20, null, true, 19, null, 1, "MANO DE OBRA", "0" },
                    { 21, null, true, 19, null, 1, "SERVICIO", "1" },
                    { 22, null, true, 19, null, 1, "MATERIALES", "2" },
                    { 23, null, true, 19, null, 1, "CONTRATO", "3" },
                    { 25, null, true, 24, null, 1, "Amazonas", null },
                    { 26, null, true, 24, null, 1, "Áncash", null },
                    { 27, null, true, 24, null, 1, "Apurímac", null },
                    { 28, null, true, 24, null, 1, "Arequipa", null },
                    { 29, null, true, 24, null, 1, "Ayacucho", null },
                    { 30, null, true, 24, null, 1, "Cajamarca", null },
                    { 31, null, true, 24, null, 1, "Callao", null },
                    { 32, null, true, 24, null, 1, "Cusco", null },
                    { 33, null, true, 24, null, 1, "Huancavelica", null },
                    { 34, null, true, 24, null, 1, "Huánuco", null },
                    { 35, null, true, 24, null, 1, "Ica", null },
                    { 36, null, true, 24, null, 1, "Junín", null },
                    { 37, null, true, 24, null, 1, "La Libertad", null },
                    { 38, null, true, 24, null, 1, "Lambayeque", null },
                    { 39, null, true, 24, null, 1, "Lima", null },
                    { 40, null, true, 24, null, 1, "Loreto", null },
                    { 41, null, true, 24, null, 1, "Madre de Dios", null },
                    { 42, null, true, 24, null, 1, "Moquegua", null },
                    { 43, null, true, 24, null, 1, "Pasco", null },
                    { 44, null, true, 24, null, 1, "Piura", null },
                    { 45, null, true, 24, null, 1, "Puno", null },
                    { 46, null, true, 24, null, 1, "San Martín", null },
                    { 47, null, true, 24, null, 1, "Tacna", null },
                    { 48, null, true, 24, null, 1, "Tumbes", null },
                    { 49, null, true, 24, null, 1, "Ucayali", null },
                    { 6, "DNI", true, 3, null, 2, "DOCUMENTO NACIONAL DE IDENTIDAD", "1" },
                    { 7, "RUC", true, 4, null, 2, "REGISTRO UNICO DE CONTRIBUYENTES", "1" },
                    { 8, "CE", true, 3, null, 2, "CARNET DE EXTRANJERIA", "2" },
                    { 50, null, true, 49, null, 2, "Coronel Portillo", null },
                    { 51, null, true, 49, null, 2, "Atalaya", null },
                    { 52, null, true, 49, null, 2, "Padre Abad", null },
                    { 53, null, true, 49, null, 2, "Purús", null },
                    { 54, null, true, 50, null, 3, "Callería", null },
                    { 55, null, true, 50, null, 3, "Campoverde", null },
                    { 56, null, true, 50, null, 3, "Iparía", null },
                    { 57, null, true, 50, null, 3, "Masisea", null },
                    { 58, null, true, 50, null, 3, "Yarinacocha", null },
                    { 59, null, true, 50, null, 3, "Nueva Requena", null },
                    { 60, null, true, 50, null, 3, "Manantay", null },
                    { 61, null, true, 51, null, 3, "Raymondi", null },
                    { 62, null, true, 51, null, 3, "Sepahua", null },
                    { 63, null, true, 51, null, 3, "Tahuanía", null },
                    { 64, null, true, 51, null, 3, "Yurúa", null },
                    { 65, null, true, 52, null, 3, "Padre Abad", null },
                    { 66, null, true, 52, null, 3, "Irázola", null },
                    { 67, null, true, 52, null, 3, "Curimana", null },
                    { 68, null, true, 53, null, 3, "Purús", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_menus_opciones_MenusId",
                table: "menus_opciones",
                column: "MenusId");

            migrationBuilder.CreateIndex(
                name: "IX_menus_opciones_OpcionesId",
                table: "menus_opciones",
                column: "OpcionesId");

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
                name: "IX_rols_LicenciaId",
                table: "rols",
                column: "LicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_rols_TipoRolId",
                table: "rols",
                column: "TipoRolId");

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

            migrationBuilder.CreateIndex(
                name: "IX_users_RolId",
                table: "users",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_licencia_LicenciaId",
                table: "usuario_licencia",
                column: "LicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_licencia_PeriodoLicenciaId",
                table: "usuario_licencia",
                column: "PeriodoLicenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_licencia_UserId",
                table: "usuario_licencia",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auditorias");

            migrationBuilder.DropTable(
                name: "menus_opciones");

            migrationBuilder.DropTable(
                name: "personas_juridicas");

            migrationBuilder.DropTable(
                name: "personas_naturales");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "sesiones");

            migrationBuilder.DropTable(
                name: "usuario_licencia");

            migrationBuilder.DropTable(
                name: "opciones");

            migrationBuilder.DropTable(
                name: "rols_permisos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "sistemas");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "rols");

            migrationBuilder.DropTable(
                name: "licencias");

            migrationBuilder.DropTable(
                name: "parametros");
        }
    }
}
