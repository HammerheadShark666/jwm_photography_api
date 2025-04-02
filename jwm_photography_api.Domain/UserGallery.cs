using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("PHOTO_UserGallery")]
public class UserGallery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    public long Id { get; set; }

    [Required]
    [Column(TypeName = "UNIQUEIDENTIFIER")]
    public Guid UserId { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<UserGalleryPhoto> Photos { get; set; } = [];

    [Column(TypeName = "nvarchar(1000)")]
    public string? Description { get; set; }
}