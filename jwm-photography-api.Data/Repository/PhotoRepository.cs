using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using jwm_photography_api.Helper;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class PhotoRepository(JwmPhotographyApiDbContext context) : IPhotoRepository
{
    public async Task<List<Photo>> GetRandomPhotosAsync(int numberOfLandscape, int numberOfPortrait, int numberOfSquare)
    {
        List<Photo> photos = [];

        photos = await GetListOfPhotos(numberOfLandscape, Enums.PhotoOrientation.landscape, photos);
        photos = await GetListOfPhotos(numberOfPortrait, Enums.PhotoOrientation.portrait, photos);
        photos = await GetListOfPhotos(numberOfSquare, Enums.PhotoOrientation.square, photos);

        return photos;
    }

    private async Task<List<Photo>> GetListOfPhotos(int numberOfPhotos, Enums.PhotoOrientation orientation, List<Photo> photos)
    {
        if (numberOfPhotos > 0)
        {
            Random rand = new();
            int skipper = rand.Next(0, await GetNumberOfPhotos(orientation));
            photos.AddRange(await GetPhotos(orientation, skipper, numberOfPhotos));
        }

        return photos;
    }

    private async Task<List<Photo>> GetPhotos(Enums.PhotoOrientation orientation, int skipper, int numberOfPhotos)
    {
        return await context.Photos
                                .Where(p => p.Orientation == (int)orientation)
                                .Include(photo => photo.Country)
                                .OrderBy(photo => Guid.NewGuid())
                                .Skip(skipper)
                                .Take(numberOfPhotos)
                                .ToListAsync();
    }

    private async Task<int> GetNumberOfPhotos(Enums.PhotoOrientation orientation)
    {
        return await (from galleryPhoto in context.GalleryPhotos
                      where galleryPhoto.Photo.Orientation == (int)orientation
                      select galleryPhoto).CountAsync();
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await context.Photos.AnyAsync(photo => photo.Id == id);
    }
}