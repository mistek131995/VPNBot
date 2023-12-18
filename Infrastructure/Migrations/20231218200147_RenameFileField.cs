using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFileField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Files",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "FileByte",
                table: "Files",
                newName: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Files",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Files",
                newName: "FileByte");
        }
    }
}
