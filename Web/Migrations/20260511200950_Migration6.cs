using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceMatrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatrixName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    MinUnits = table.Column<int>(type: "int", nullable: false),
                    Markup = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxColors = table.Column<int>(type: "int", nullable: false),
                    SalesGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceMatrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceMatrices_SalesGroups_SalesGroupID",
                        column: x => x.SalesGroupID,
                        principalTable: "SalesGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceMatrixProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesGroupID = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowerThreshold = table.Column<int>(type: "int", nullable: false),
                    UpperThreshold = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Descript = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceMatrixProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceMatrixPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<int>(type: "int", nullable: false),
                    LevelMaxCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MatrixId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceMatrixPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceMatrixPrices_PriceMatrices_MatrixId",
                        column: x => x.MatrixId,
                        principalTable: "PriceMatrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceMatrices_SalesGroupID",
                table: "PriceMatrices",
                column: "SalesGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceMatrixPrices_MatrixId",
                table: "PriceMatrixPrices",
                column: "MatrixId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceMatrixPrices");

            migrationBuilder.DropTable(
                name: "PriceMatrixProperties");

            migrationBuilder.DropTable(
                name: "PriceMatrices");
        }
    }
}
