using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IUserGalleryRepository
{
    Task<List<UserGallery>> AllSortedAsync();
    Task<List<UserGallery>> AllSortedAsync(Guid userId);
    Task<UserGallery?> GetAsync(Guid userId, long galleryId);
    Task<bool> ExistsAsync(Guid userId, string name);
    Task<bool> ExistsAsync(Guid userId, long id, string name);
    Task<UserGallery?> GetFullGalleryAsync(Guid userId, long id);
    Task AddAsync(UserGallery gallery);
    void Update(UserGallery userGallery);
    void Delete(UserGallery userGallery);
    Task<UserGallery?> ByIdAsync(long id);
}