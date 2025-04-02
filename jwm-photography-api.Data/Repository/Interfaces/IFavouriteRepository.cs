using jwm_photography_api.Domain;
using PhotographySite.Areas.Admin.Dto.Request;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IFavouriteRepository
{
    Task<List<Photo>> GetFavouritePhotosAsync(Guid userId);

    Task<Favourite?> GetFavouritePhotoAsync(Guid userId, long photoId);

    Task<int> ByFilterCountAsync(Guid userId, PhotoFilterRequest photoFilterRequest);

    Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterRequest photoFilterRequest);

    Task<Boolean> ExistsAsync(Guid userId, long photoId);

    void Delete(Favourite favourite);

    Task AddAsync(Favourite favourite);
}