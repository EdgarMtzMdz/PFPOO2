using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    idClientes = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombreClientes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaRegistrio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.idClientes);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    idEmpleados = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombreEmpleados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.idEmpleados);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    idProveedores = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codProveedores = table.Column<int>(type: "int", nullable: false),
                    nombreProveedores = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.idProveedores);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    codBarras = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantProducto = table.Column<int>(type: "int", nullable: false),
                    codProveedor = table.Column<int>(type: "int", nullable: false),
                    costoProducto = table.Column<float>(type: "real", nullable: false),
                    idProveedores = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProveedoresidProveedores = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.codBarras);
                    table.ForeignKey(
                        name: "FK_Inventario_Proveedores_ProveedoresidProveedores",
                        column: x => x.ProveedoresidProveedores,
                        principalTable: "Proveedores",
                        principalColumn: "idProveedores");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ProveedoresidProveedores",
                table: "Inventario",
                column: "ProveedoresidProveedores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
