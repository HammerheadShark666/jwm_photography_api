using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Favourite")]

public class Favourite
{
    public Guid AccountId { get; set; }
    public long PhotoId { get; set; }
    public required Photo Photo { get; set; }
}