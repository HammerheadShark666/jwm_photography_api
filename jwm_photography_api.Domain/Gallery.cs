using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Gallery")]
public class Gallery
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<GalleryPhoto> Photos { get; set; } = [];
}