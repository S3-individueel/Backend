using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolksmondAPI.Migrations
{
    /// <inheritdoc />
    public partial class solutionCitizen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Solution_CitizenId",
                table: "Solution",
                column: "CitizenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solution_Citizen_CitizenId",
                table: "Solution",
                column: "CitizenId",
                principalTable: "Citizen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solution_Citizen_CitizenId",
                table: "Solution");

            migrationBuilder.DropIndex(
                name: "IX_Solution_CitizenId",
                table: "Solution");
        }
    }
}
