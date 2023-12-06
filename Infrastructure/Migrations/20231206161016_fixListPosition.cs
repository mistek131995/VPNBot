using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixListPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_AccessPositionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccessPositionId",
                table: "Payments",
                column: "AccessPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses",
                column: "VpnServerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_AccessPositionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AccessPositionId",
                table: "Payments",
                column: "AccessPositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses",
                column: "VpnServerId",
                unique: true);
        }
    }
}
