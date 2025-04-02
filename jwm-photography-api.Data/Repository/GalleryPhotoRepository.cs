using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using jwm_photography_api.Helper;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class GalleryPhotoRepository(JwmPhotographyApiDbContext context) : IGalleryPhotoRepository
{
    public async Task<List<Photo>> GetGalleryPhotosAsync(long galleryId)
    {
        return await (from galleryPhoto in context.GalleryPhotos
                      join photo in context.Photos
                            on galleryPhoto.PhotoId equals photo.Id
                      where galleryPhoto.GalleryId == galleryId
                      orderby (galleryPhoto.Order)
                      select photo).ToListAsync();
    }

    public async Task<GalleryPhoto?> GetGalleryPhotoAsync(long galleryId, long photoId)
    {
        return await (from galleryPhoto in context.GalleryPhotos
                      where galleryPhoto.GalleryId == galleryId
                        && galleryPhoto.PhotoId == photoId
                      select galleryPhoto).SingleOrDefaultAsync();
    }

    public async Task<List<GalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in context.GalleryPhotos
                      where galleryPhotos.GalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order >= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<List<GalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in context.GalleryPhotos
                      where galleryPhotos.GalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order <= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<Photo?> GetRandomGalleryPhotoAsync(long galleryId)
    {
        int gallerySize = await (from galleryPhoto in context.GalleryPhotos
                                 where galleryPhoto.GalleryId == galleryId
                                    && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                                 select galleryPhoto).CountAsync();

        if (gallerySize == 0) return null;

        Random rand = new();
        int toSkip = rand.Next(0, gallerySize);

        return await (from galleryPhoto in context.GalleryPhotos
                      join photo in context.Photos
                            on galleryPhoto.PhotoId equals photo.Id
                      where galleryPhoto.GalleryId == galleryId
                          && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                      orderby (galleryPhoto.Order)
                      select photo).Skip(toSkip).FirstOrDefaultAsync();
    }

    public async Task<GalleryPhoto> AddAsync(GalleryPhoto galleryPhoto)
    {
        await context.GalleryPhotos.AddAsync(galleryPhoto);
        return galleryPhoto;
    }

    public void Update(GalleryPhoto galleryPhoto)
    {
        context.GalleryPhotos.Update(galleryPhoto);
    }

    public void Delete(GalleryPhoto galleryPhoto)
    {
        context.GalleryPhotos.Remove(galleryPhoto);
    }
}