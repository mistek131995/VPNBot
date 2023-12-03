using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLinkFromAccessToVpnServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Test",
                table: "Accesses",
                newName: "VpnServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses",
                column: "VpnServerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Accesses");

            migrationBuilder.RenameColumn(
                name: "VpnServerId",
                table: "Accesses",
                newName: "Test");
        }
    }
}
