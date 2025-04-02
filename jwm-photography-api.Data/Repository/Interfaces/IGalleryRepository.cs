using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IGalleryRepository
{
    Task<List<Gallery>> GetAllGalleriesSortedAsync();
    Task<Gallery> GetGalleryAsync(long id);

    Task<bool> ExistsAsync(long id);
}