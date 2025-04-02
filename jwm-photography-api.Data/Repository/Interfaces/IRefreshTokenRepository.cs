using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IRefreshTokenRepository
{
    Task<bool> ExistsAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    void Update(RefreshToken refreshToken);
    void Delete(RefreshToken refreshToken);
    Task<List<RefreshToken>> ByIdAsync(int accountId);
    Task<RefreshToken?> ByTokenAsync(string token);
    void RemoveExpired(int expireDays, Guid accountId);
}
