// Ignore Spelling: Jwt

using jwm_photography_api.Domain;
using jwm_photography_api.Helpers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace jwm_photography_api.Helper;

public class AuthenticationHelper
{
    public static string IpAddress(HttpRequest request, HttpContext httpContext)
    {
        if (request.Headers.TryGetValue("X-Forwarded-For", out Microsoft.Extensions.Primitives.StringValues value))
        {
            return value;
        }

        return httpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "Unknown IpAddress";
    }

    public static string CreateRandomToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var randomNumber = new byte[40];
        rng.GetBytes(randomNumber);
        return CleanToken(randomNumber);
    }

    public static string CleanToken(byte[] randomNumber)
    {
        return Convert.ToBase64String(randomNumber).Replace('+', '-')
                                                   .Replace('/', '_')
                                                   .Replace("=", "4")
                                                   .Replace("?", "G")
                                                   .Replace("/", "X");
    }

    public static RefreshToken GenerateRefreshToken(string ipAddress, DateTime expires, Account account)
    {
        return new RefreshToken
        {
            Token = AuthenticationHelper.CreateRandomToken(),
            Expires = expires,
            Created = DateTime.Now,
            CreatedByIp = ipAddress,
            AccountId = account.Id,
            Account = account
        };
    }

    public static string GenerateJwtToken(Account account)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JwtSymmetricSecurityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: EnvironmentVariables.JwtIssuer,
            audience: EnvironmentVariables.JwtAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(EnvironmentVariables.JwtSettingsTokenExpiryMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static Guid GetAccountId(HttpContext httpContext)
    {
        var accountId = (httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value) ?? throw new BadRequestException("Account Id not found.");
        return new Guid(accountId);
    }
}