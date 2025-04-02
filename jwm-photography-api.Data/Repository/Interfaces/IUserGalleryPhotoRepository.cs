using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IUserGalleryPhotoRepository
{
    Task<List<Photo>> GetGalleryPhotosAsync(long galleryId);
    Task<UserGalleryPhoto?> GetGalleryPhotoAsync(long galleryId, long photoId);
    Task<Photo?> GetRandomGalleryPhotoAsync(long galleryId);
    Task<List<UserGalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order);
    Task<List<UserGalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order);
    Task<UserGalleryPhoto> AddAsync(UserGalleryPhoto userGalleryPhoto);
    void Delete(UserGalleryPhoto userGalleryPhoto);
}