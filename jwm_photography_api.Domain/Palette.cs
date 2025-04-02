using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("PHOTO_Palette")]
public class Palette
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    [Required]
    public required string Name { get; set; }
}