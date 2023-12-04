using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLinkFromAccessToVpnServer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              UPDATE a
              SET a.VpnServerId = vs.Id
              FROM Accesses a
              JOIN VpnServers vs ON a.Ip = vs.Ip
            ");

            migrationBuilder.AddColumn<string>(
                name: "Passsword",
                table: "VpnServers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "VpnServers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passsword",
                table: "VpnServers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "VpnServers");
        }
    }
}
