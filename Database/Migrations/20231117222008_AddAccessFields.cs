using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses");

            migrationBuilder.AddColumn<string>(
                name: "Fp",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Accesses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pbk",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Security",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sid",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sni",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Spx",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Accesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Fp",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Pbk",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Security",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Sid",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Sni",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Spx",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Accesses");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_UserId",
                table: "Accesses",
                column: "UserId");
        }
    }
}
