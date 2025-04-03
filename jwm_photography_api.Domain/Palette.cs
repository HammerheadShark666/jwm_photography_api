using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Palette")]
public class Palette
{
    public int Id { get; set; }
    public required string Name { get; set; }
}