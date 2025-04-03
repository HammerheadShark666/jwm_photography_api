using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_UserGallery")]
public class UserGallery
{
    public long Id { get; set; }
    public Guid AccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<UserGalleryPhoto> Photos { get; set; } = [];
}