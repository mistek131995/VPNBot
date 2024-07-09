using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailNotificationTicketMessage",
                table: "UserSettings",
                newName: "UseEmailNotificationTicketMessage");

            migrationBuilder.RenameColumn(
                name: "EmailNotificationLoginInError",
                table: "UserSettings",
                newName: "UseEmailNotificationLoginInError");

            migrationBuilder.RenameColumn(
                name: "EmailNotificationAboutNews",
                table: "UserSettings",
                newName: "UseEmailNotificationAboutNews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseEmailNotificationTicketMessage",
                table: "UserSettings",
                newName: "EmailNotificationTicketMessage");

            migrationBuilder.RenameColumn(
                name: "UseEmailNotificationLoginInError",
                table: "UserSettings",
                newName: "EmailNotificationLoginInError");

            migrationBuilder.RenameColumn(
                name: "UseEmailNotificationAboutNews",
                table: "UserSettings",
                newName: "EmailNotificationAboutNews");
        }
    }
}
