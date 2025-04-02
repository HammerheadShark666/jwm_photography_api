using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using jwm_photography_api.Helpers.Exceptions;

namespace PhotographySite.Data.Repository;

public class UserRepository(JwmPhotographyApiDbContext context) : IUserRepository
{
    public Guid GetUserId(string email)
    {
        Account? account = context.Accounts.FirstOrDefault(user => user.Email == email);

        if ((account == null))
            throw new NotFoundException("Account Not Found.");

        return account.Id;
    }
}