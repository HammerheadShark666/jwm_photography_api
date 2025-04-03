using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Country")]

public class Country
{
    public int Id { get; set; }
    public required string Name { get; set; }
}