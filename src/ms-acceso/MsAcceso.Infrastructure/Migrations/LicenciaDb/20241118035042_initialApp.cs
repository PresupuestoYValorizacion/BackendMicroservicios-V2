using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MsAcceso.Infrastructure.Migrations.LicenciaDb
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
                name: "presupuestos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pruebas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pruebas", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_ubigeos_Dependencia",
                table: "ubigeos",
                column: "Dependencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "especialidades");

            migrationBuilder.DropTable(
                name: "presupuestos");

            migrationBuilder.DropTable(
                name: "pruebas");

            migrationBuilder.DropTable(
                name: "ubigeos");
        }
    }
}
