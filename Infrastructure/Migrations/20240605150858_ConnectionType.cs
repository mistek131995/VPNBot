using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConnectionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionType",
                table: "UserConnections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE UserConnections SET ConnectionType = 1
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "UserConnections");
        }
    }
}
