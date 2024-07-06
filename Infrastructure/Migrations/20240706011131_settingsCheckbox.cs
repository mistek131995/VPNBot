using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class settingsCheckbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailNotificationAboutNews",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailNotificationLoginInError",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailNotificationTicketMessage",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseTelegramNotificationAboutNews",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseTelegramNotificationLoginInError",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailNotificationAboutNews",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "EmailNotificationLoginInError",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "EmailNotificationTicketMessage",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "UseTelegramNotificationAboutNews",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "UseTelegramNotificationLoginInError",
                table: "UserSettings");
        }
    }
}
