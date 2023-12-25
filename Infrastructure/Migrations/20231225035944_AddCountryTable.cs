using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "VpnServers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnServers_CountryId",
                table: "VpnServers",
                column: "CountryId");

            migrationBuilder.Sql(@"
                INSERT INTO Countries (Name) VALUES ('Нидерладнды')
                INSERT INTO Countries (Name) VALUES ('Франция')
                INSERT INTO Countries (Name) VALUES ('Латвия')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_VpnServers_CountryId",
                table: "VpnServers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "VpnServers");
        }
    }
}
