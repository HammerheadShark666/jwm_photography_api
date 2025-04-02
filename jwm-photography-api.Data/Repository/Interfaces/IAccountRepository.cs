using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetAsync(string email);
    Task<Account?> GetByResetTokenAsync(string resetToken);
    Task<Account?> GetByVerificationTokenAsync(string verificationToken);
    Task<Account?> GetByRefreshTokenAsync(string token);
    Task<bool> ExistsAsync(string email);
    Task<bool> ExistsAsync(string email, Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> AnyAccountExistAsync();
    Task<bool> ValidResetTokenAsync(string token);
    Task<bool> ValidResetTokenEmailCurrentPasswordAsync(string token, string email, string currentPassword);
    Task<Account?> ByIdAsync(int id);
    void Add(Account account);
    void Update(Account account);
    void Delete(Account account);
}