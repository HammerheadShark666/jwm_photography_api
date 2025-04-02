using System.ComponentModel.DataAnnotations.Schema;
using static jwm_photography_api.Domain.Helper.Enums;

namespace jwm_photography_api.Domain;

[Table("PHOTO_Account")]
public class Account
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool AcceptTerms { get; set; }
    public Role? Role { get; set; }
    public string? VerificationToken { get; set; } = string.Empty;
    public DateTime? Verified { get; set; }
    public bool IsAuthenticated => Verified.HasValue || PasswordReset.HasValue;
    public string? ResetToken { get; set; } = string.Empty;
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public List<Domain.RefreshToken> RefreshTokens { get; set; }

    public Account()
    {
        RefreshTokens = [];
    }
}