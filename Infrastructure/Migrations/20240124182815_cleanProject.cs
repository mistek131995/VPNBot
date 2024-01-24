using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cleanProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accesses_Users_UserId",
                table: "Accesses");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Accesses_VpnServers_VpnServerId",
            //    table: "Accesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accesses",
                table: "Accesses");

            migrationBuilder.DropIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses");

            migrationBuilder.RenameTable(
                name: "Accesses",
                newName: "Access");

            migrationBuilder.RenameIndex(
                name: "IX_Accesses_VpnServerId",
                table: "Access",
                newName: "IX_Access_VpnServerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Access",
                table: "Access",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Access_UserId",
                table: "Access",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Access_Users_UserId",
                table: "Access",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Access_VpnServers_VpnServerId",
                table: "Access",
                column: "VpnServerId",
                principalTable: "VpnServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Access_Users_UserId",
                table: "Access");

            migrationBuilder.DropForeignKey(
                name: "FK_Access_VpnServers_VpnServerId",
                table: "Access");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Access",
                table: "Access");

            migrationBuilder.DropIndex(
                name: "IX_Access_UserId",
                table: "Access");

            migrationBuilder.RenameTable(
                name: "Access",
                newName: "Accesses");

            migrationBuilder.RenameIndex(
                name: "IX_Access_VpnServerId",
                table: "Accesses",
                newName: "IX_Accesses_VpnServerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accesses",
                table: "Accesses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accesses_Users_UserId",
                table: "Accesses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Accesses_VpnServers_VpnServerId",
            //    table: "Accesses",
            //    column: "VpnServerId",
            //    principalTable: "VpnServers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
