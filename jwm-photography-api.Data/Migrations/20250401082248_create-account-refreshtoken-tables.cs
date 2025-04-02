using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwm_photography_api.Migrations;

/// <inheritdoc />
public partial class createaccountrefreshtokentables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PHOTO_Account",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AcceptTerms = table.Column<bool>(type: "bit", nullable: false),
                Role = table.Column<int>(type: "int", nullable: true),
                VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Verified = table.Column<DateTime>(type: "datetime2", nullable: true),
                ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                PasswordReset = table.Column<DateTime>(type: "datetime2", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PHOTO_Account", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PHOTO_RefreshToken",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AccountId = table.Column<int>(type: "int", nullable: false),
                Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PHOTO_RefreshToken", x => x.Id);
                table.ForeignKey(
                    name: "FK_PHOTO_RefreshToken_PHOTO_Account_AccountId",
                    column: x => x.AccountId,
                    principalTable: "PHOTO_Account",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PHOTO_RefreshToken_AccountId",
            table: "PHOTO_RefreshToken",
            column: "AccountId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PHOTO_RefreshToken");

        migrationBuilder.DropTable(
            name: "PHOTO_Account");
    }
}
