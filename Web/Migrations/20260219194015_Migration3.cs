using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_fkCompany",
                table: "Clients",
                column: "fkCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Companies_fkCompany",
                table: "Clients",
                column: "fkCompany",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Companies_fkCompany",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_fkCompany",
                table: "Clients");
        }
    }
}
