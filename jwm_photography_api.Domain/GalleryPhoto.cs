using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_GalleryPhoto")]
public class GalleryPhoto
{
    public long GalleryId { get; set; }
    public long PhotoId { get; set; }
    public int Order { get; set; }
    public required Photo Photo { get; set; }
    public required Gallery Gallery { get; set; }
}