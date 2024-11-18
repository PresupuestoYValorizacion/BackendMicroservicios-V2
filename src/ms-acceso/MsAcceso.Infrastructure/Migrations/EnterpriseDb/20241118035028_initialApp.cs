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
                name: "especialidades");

            migrationBuilder.DropTable(
                name: "personas_juridicas");

            migrationBuilder.DropTable(
                name: "personas_naturales");

            migrationBuilder.DropTable(
                name: "rols_permisos_opciones");

            migrationBuilder.DropTable(
                name: "titulos");

            migrationBuilder.DropTable(
                name: "ubigeos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rols_permisos");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "rols");
        }
    }
}
