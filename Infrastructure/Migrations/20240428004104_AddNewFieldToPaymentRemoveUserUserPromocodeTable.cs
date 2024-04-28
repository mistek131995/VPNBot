using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToPaymentRemoveUserUserPromocodeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUsedPromoCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PromoCodeId",
                table: "Payments",
                column: "PromoCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_Payments_PromoCodeId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Payments");

            migrationBuilder.CreateTable(
                name: "UserUsedPromoCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoCodeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUsedPromoCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUsedPromoCodes_PromoCodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "PromoCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUsedPromoCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUsedPromoCodes_PromoCodeId",
                table: "UserUsedPromoCodes",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUsedPromoCodes_UserId",
                table: "UserUsedPromoCodes",
                column: "UserId");
        }
    }
}
