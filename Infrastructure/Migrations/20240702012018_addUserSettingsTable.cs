using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UseTelegramNotificationTicketMessage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);


            migrationBuilder.Sql(@$"
                DECLARE @IDs TABLE(Id INT)
                DECLARE @I INT SET @I = 0


                INSERT INTO @IDs
                SELECT
                u.Id
                FROM Users u

                WHILE (SELECT COUNT(*) FROM @IDs) > @I
	                BEGIN

		                INSERT INTO UserSettings(UserId, UseTelegramNotificationTicketMessage)
		                SELECT
		                Id AS UserId,
		                1 AS UseTelegramNotificationTicketMessage
		                FROM @IDs order by Id offset @I rows fetch first 1 rows only

		                SET @I = @I + 1
	                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");
        }
    }
}
