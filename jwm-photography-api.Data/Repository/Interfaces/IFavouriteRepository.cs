using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IFavouriteRepository
{
    Task<List<Photo>> GetFavouritePhotosAsync(Guid userId);
    Task<Favourite?> GetFavouritePhotoAsync(Guid userId, long photoId);
    Task<Boolean> ExistsAsync(Guid userId, long photoId);
    void Delete(Favourite favourite);
    Task AddAsync(Favourite favourite);
}