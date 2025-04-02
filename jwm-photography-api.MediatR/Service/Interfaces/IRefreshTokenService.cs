using jwm_photography_api.Domain;

namespace jwm_photography_api.MediatR.Service.Interfaces;

public interface IRefreshTokenService
{
    void RemoveExpiredRefreshTokens(Guid accountId);
    Task AddRefreshToken(RefreshToken refreshToken);
    RefreshToken GenerateRefreshToken(string ipAddress, Account account);
    Task<RefreshToken> GetRefreshTokenAsync(string token);
}