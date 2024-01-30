using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeHCaptchaKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Settings SET CaptchaPrivateKey = 'ES_13f1c893b24845ce9ac239a4fa1be497'
                UPDATE Settings SET CaptchaPublicKey = 'e7bbc5aa-9151-4f39-8ae1-cced70bec276'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
