using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Category")]
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
}