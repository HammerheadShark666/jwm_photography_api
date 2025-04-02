using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("PHOTO_Gallery")]
public class Gallery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required]
    public required string Name { get; set; }

    public ICollection<GalleryPhoto> Photos { get; set; } = [];

    [Column(TypeName = "nvarchar(1000)")]
    public string? Description { get; set; }
}