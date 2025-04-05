using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwm_photography_api.Data.Migrations;

/// <inheritdoc />
public partial class removeidfromusergalleryphotos : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "JWM_PHOTO_UserGalleryPhoto",
            columns: table => new
            {
                UserGalleryId = table.Column<int>(type: "int", nullable: false),
                PhotoId = table.Column<int>(type: "int", nullable: false),
                Order = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JWM_PHOTO_UserGalleryPhoto", x => new { x.UserGalleryId, x.PhotoId });
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
            name: "IX_JWM_PHOTO_UserGalleryPhoto_PhotoId",
            table: "JWM_PHOTO_UserGalleryPhoto",
            column: "PhotoId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "JWM_PHOTO_UserGalleryPhoto");
    }
}
