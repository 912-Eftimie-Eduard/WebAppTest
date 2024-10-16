using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestTestinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articol",
                columns: table => new
                {
                    IdArticol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAutorPrincipal = table.Column<int>(type: "int", nullable: false),
                    IdRevista = table.Column<int>(type: "int", nullable: false),
                    TitluArticol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnPublicare = table.Column<int>(type: "int", nullable: false),
                    NrPagini = table.Column<int>(type: "int", nullable: false),
                    NrCoautori = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articol", x => x.IdArticol);
                });

            migrationBuilder.CreateTable(
                name: "Persoana",
                columns: table => new
                {
                    IdPersoana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasterii = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persoana", x => x.IdPersoana);
                });

            migrationBuilder.CreateTable(
                name: "Revista",
                columns: table => new
                {
                    IdRevista = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipRevista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitluRevista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnAparitie = table.Column<int>(type: "int", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tara = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revista", x => x.IdRevista);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articol");

            migrationBuilder.DropTable(
                name: "Persoana");

            migrationBuilder.DropTable(
                name: "Revista");
        }
    }
}
