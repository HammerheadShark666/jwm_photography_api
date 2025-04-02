using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IGalleryPhotoRepository
{
    Task<List<Photo>> GetGalleryPhotosAsync(long galleryId);
    Task<GalleryPhoto?> GetGalleryPhotoAsync(long galleryId, long photoId);
    Task<Photo?> GetRandomGalleryPhotoAsync(long galleryId);
    Task<List<GalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order);
    Task<List<GalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order);
    Task<GalleryPhoto> AddAsync(GalleryPhoto galleryPhoto);
    void Update(GalleryPhoto galleryPhoto);
    void Delete(GalleryPhoto galleryPhoto);
}