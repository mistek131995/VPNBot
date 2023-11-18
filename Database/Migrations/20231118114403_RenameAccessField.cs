using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameAccessField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Accesses",
                newName: "ShortId");

            migrationBuilder.RenameColumn(
                name: "Spx",
                table: "Accesses",
                newName: "ServerName");

            migrationBuilder.RenameColumn(
                name: "Sni",
                table: "Accesses",
                newName: "PublicKey");

            migrationBuilder.RenameColumn(
                name: "Sid",
                table: "Accesses",
                newName: "Network");

            migrationBuilder.RenameColumn(
                name: "Pbk",
                table: "Accesses",
                newName: "Fingerprint");

            migrationBuilder.RenameColumn(
                name: "Fp",
                table: "Accesses",
                newName: "AccessName");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Accesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "Accesses");

            migrationBuilder.RenameColumn(
                name: "ShortId",
                table: "Accesses",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ServerName",
                table: "Accesses",
                newName: "Spx");

            migrationBuilder.RenameColumn(
                name: "PublicKey",
                table: "Accesses",
                newName: "Sni");

            migrationBuilder.RenameColumn(
                name: "Network",
                table: "Accesses",
                newName: "Sid");

            migrationBuilder.RenameColumn(
                name: "Fingerprint",
                table: "Accesses",
                newName: "Pbk");

            migrationBuilder.RenameColumn(
                name: "AccessName",
                table: "Accesses",
                newName: "Fp");
        }
    }
}
