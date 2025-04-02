using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class RefreshTokenRepository(JwmPhotographyApiDbContext context) : IRefreshTokenRepository
{
    private readonly JwmPhotographyApiDbContext _context = context;

    public async Task<bool> ExistsAsync(string token)
    {
        return await _context.RefreshTokens
                                .AsNoTracking()
                                .AnyAsync(a => a.Token.Equals(token));
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public void Update(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
    }

    public void Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
    }

    public async Task<RefreshToken?> ByTokenAsync(string token)
    {
        return await _context.RefreshTokens
                             .Include(a => a.Account)
                             .Where(x => x.Token.Equals(token))
                             .SingleOrDefaultAsync();
    }

    public async Task<List<RefreshToken>> ByIdAsync(int accountId)
    {
        return await _context.RefreshTokens.Where(a => a.Account.Id.Equals(accountId)).ToListAsync();
    }

    public void RemoveExpired(int expireDays, Guid accountId)
    {
        var refreshTokens = _context.RefreshTokens.Where(a => a.Account.Id.Equals(accountId)
                                                            && DateTime.Now >= a.Expires
                                                                && a.Created.AddDays(expireDays) <= DateTime.Now).ToList();

        _context.RefreshTokens.RemoveRange(refreshTokens);
    }
}