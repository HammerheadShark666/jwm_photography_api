using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("PHOTO_Favourite")]

public class Favourite
{
    [Required]
    [Column(TypeName = "UNIQUEIDENTIFIER")]
    public Guid UserId { get; set; }

    [Required]
    [Column(TypeName = "bigint")]
    public long PhotoId { get; set; }

    public required Photo Photo { get; set; }
}