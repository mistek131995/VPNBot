using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class messageCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "TicketMessages");

            migrationBuilder.AddColumn<int>(
                name: "Condition",
                table: "TicketMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "TicketMessages");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "TicketMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
