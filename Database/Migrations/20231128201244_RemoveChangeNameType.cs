using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChangeNameType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccessPositions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql(@"

                INSERT INTO AccessPositions (Name, MonthCount, Description, Price) VALUES ('1 месяц', 1, '', 0)
                INSERT INTO AccessPositions (Name, MonthCount, Description, Price) VALUES ('1 месяц', 1, '', 100)
                INSERT INTO AccessPositions (Name, MonthCount, Description, Price) VALUES ('3 месяца', 3, '', 180)
                INSERT INTO AccessPositions (Name, MonthCount, Description, Price) VALUES ('6 месяцев', 6, '', 260)

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "AccessPositions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
