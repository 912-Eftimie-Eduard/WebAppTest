using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class FK_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articol_IdAutorPrincipal",
                table: "Articol",
                column: "IdAutorPrincipal");

            migrationBuilder.CreateIndex(
                name: "IX_Articol_IdRevista",
                table: "Articol",
                column: "IdRevista");

            migrationBuilder.AddForeignKey(
                name: "FK_Articol_Persoana_IdAutorPrincipal",
                table: "Articol",
                column: "IdAutorPrincipal",
                principalTable: "Persoana",
                principalColumn: "IdPersoana",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articol_Revista_IdRevista",
                table: "Articol",
                column: "IdRevista",
                principalTable: "Revista",
                principalColumn: "IdRevista",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articol_Persoana_IdAutorPrincipal",
                table: "Articol");

            migrationBuilder.DropForeignKey(
                name: "FK_Articol_Revista_IdRevista",
                table: "Articol");

            migrationBuilder.DropIndex(
                name: "IX_Articol_IdAutorPrincipal",
                table: "Articol");

            migrationBuilder.DropIndex(
                name: "IX_Articol_IdRevista",
                table: "Articol");
        }
    }
}
