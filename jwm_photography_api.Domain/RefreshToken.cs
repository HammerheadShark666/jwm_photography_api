using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_RefreshToken")]
public class RefreshToken
{
    public int Id { get; set; }
    public required Account Account { get; set; }
    public required Guid AccountId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsExpired { get; private set; }
    public DateTime Created { get; set; }
    public string CreatedByIp { get; set; } = string.Empty;
}