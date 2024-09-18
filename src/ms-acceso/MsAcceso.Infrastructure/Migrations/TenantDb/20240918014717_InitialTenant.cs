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
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ciudadanos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudadanos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "licencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
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
                name: "pasaportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pasaportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pasaportes_ciudadanos_Id",
                        column: x => x.Id,
                        principalTable: "ciudadanos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "detalle-productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle-productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalle-productos_productos_Id",
                        column: x => x.Id,
                        principalTable: "productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "producto-categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto-categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_producto-categorias_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto-categorias_productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resenias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resenias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resenias_productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "menus_opciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpcionesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    Email = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
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
                        name: "FK_usuario_licencia_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "licencias",
                columns: new[] { "Id", "Activo", "Nombre" },
                values: new object[,]
                {
                    { new Guid("1a9e887b-aa55-49b8-b9bc-4d7ba609d065"), true, "PROFESIONAL" },
                    { new Guid("e88a6456-3941-4136-b172-7a0d5167c7fc"), true, "EDUCACIONAL" },
                    { new Guid("ecbdebff-cb86-4e74-bd12-f7fbfc165dfb"), true, "ENTERPRISE" }
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
                    { 3, null, true, 2, null, 1, "NATURAL", "1" },
                    { 4, null, true, 2, null, 1, "JURIDICA", "2" },
                    { 10, null, true, 9, null, 1, "LICENCIA", "1" },
                    { 11, null, true, 9, null, 1, "ADMINISTRADOR", "2" },
                    { 6, "DNI", true, 3, null, 2, "DOCUMENTO NACIONAL DE IDENTIDAD", "1" },
                    { 7, "RUC", true, 4, null, 2, "REGISTRO UNICO DE CONTRIBUYENTES", "1" },
                    { 8, "CE", true, 3, null, 2, "CARNET DE EXTRANJERIA", "2" }
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
                name: "IX_producto-categorias_CategoriaId",
                table: "producto-categorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_producto-categorias_ProductoId",
                table: "producto-categorias",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_resenias_ProductoId",
                table: "resenias",
                column: "ProductoId");

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
                name: "detalle-productos");

            migrationBuilder.DropTable(
                name: "menus_opciones");

            migrationBuilder.DropTable(
                name: "pasaportes");

            migrationBuilder.DropTable(
                name: "personas_juridicas");

            migrationBuilder.DropTable(
                name: "personas_naturales");

            migrationBuilder.DropTable(
                name: "producto-categorias");

            migrationBuilder.DropTable(
                name: "resenias");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "usuario_licencia");

            migrationBuilder.DropTable(
                name: "ciudadanos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "productos");

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
