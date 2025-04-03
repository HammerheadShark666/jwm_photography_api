using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_UserGalleryPhoto")]
public class UserGalleryPhoto
{
    public int Id { get; set; }
    public long UserGalleryId { get; set; }
    public UserGallery? UserGallery { get; set; }
    public long PhotoId { get; set; }
    public int Order { get; set; }
    public Photo? Photo { get; set; }
}