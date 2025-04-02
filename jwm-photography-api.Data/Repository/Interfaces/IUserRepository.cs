namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IUserRepository
{
    Guid GetUserId(string email);
}