using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwm_photography_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class createjwmphototables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AcceptTerms = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Verified = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    IsAuthenticated = table.Column<bool>(type: "bit", nullable: false, computedColumnSql: "CAST(CASE WHEN Verified IS NOT NULL OR PasswordReset IS NOT NULL THEN 1 ELSE 0 END AS bit)", stored: true),
                    ResetToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    PasswordReset = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "GETDATE()"),
                    Updated = table.Column<DateTime>(type: "datetime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Gallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Palette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Palette", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_UserGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_UserGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false, computedColumnSql: "CAST(CASE WHEN GETDATE() >= Expires THEN 1 ELSE 0 END AS bit)"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_RefreshToken_JWM_PHOTO_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "JWM_PHOTO_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Camera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Lens = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExposureTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApertureValue = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ExposureProgram = table.Column<string>(type: "nvarchar(50)", maxLength: 25, nullable: true),
                    Iso = table.Column<int>(type: "int", nullable: true),
                    DateTaken = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    FocalLength = table.Column<string>(type: "nvarchar(10)", maxLength: 15, nullable: true),
                    Orientation = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    UseInMontage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PaletteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_Photo_JWM_PHOTO_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "JWM_PHOTO_Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_Photo_JWM_PHOTO_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "JWM_PHOTO_Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_Photo_JWM_PHOTO_Palette_PaletteId",
                        column: x => x.PaletteId,
                        principalTable: "JWM_PHOTO_Palette",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_Favourite",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_Favourite", x => new { x.AccountId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_Favourite_JWM_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "JWM_PHOTO_Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_GalleryPhoto",
                columns: table => new
                {
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_GalleryPhoto", x => new { x.GalleryId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_GalleryPhoto_JWM_PHOTO_Gallery_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "JWM_PHOTO_Gallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_GalleryPhoto_JWM_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "JWM_PHOTO_Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JWM_PHOTO_UserGalleryPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGalleryId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWM_PHOTO_UserGalleryPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_UserGalleryPhoto_JWM_PHOTO_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "JWM_PHOTO_Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JWM_PHOTO_UserGalleryPhoto_JWM_PHOTO_UserGallery_UserGalleryId",
                        column: x => x.UserGalleryId,
                        principalTable: "JWM_PHOTO_UserGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_Favourite_PhotoId",
                table: "JWM_PHOTO_Favourite",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_GalleryPhoto_PhotoId",
                table: "JWM_PHOTO_GalleryPhoto",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_Photo_CategoryId",
                table: "JWM_PHOTO_Photo",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_Photo_CountryId",
                table: "JWM_PHOTO_Photo",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_Photo_PaletteId",
                table: "JWM_PHOTO_Photo",
                column: "PaletteId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_RefreshToken_AccountId",
                table: "JWM_PHOTO_RefreshToken",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_UserGalleryPhoto_PhotoId",
                table: "JWM_PHOTO_UserGalleryPhoto",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_JWM_PHOTO_UserGalleryPhoto_UserGalleryId",
                table: "JWM_PHOTO_UserGalleryPhoto",
                column: "UserGalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Favourite");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_GalleryPhoto");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_RefreshToken");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_UserGalleryPhoto");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Gallery");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Account");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Photo");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_UserGallery");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Category");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Country");

            migrationBuilder.DropTable(
                name: "JWM_PHOTO_Palette");
        }
    }
}
