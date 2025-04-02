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


    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Photos.AnyAsync(photo => photo.Id == id);
    }


    //public async Task<List<Photo>> AllAsync()
    //{
    //    return await context.Photos
    //        .Include(photo => photo.Country)
    //        .ToListAsync();
    //}

    //public async Task<int> CountAsync()
    //{
    //    return await context.Photos.CountAsync();
    //}

    //public async Task<List<Photo>> ByPagingAsync(PhotoFilterRequest photoFilterRequest)
    //{
    //    return await context.Photos
    //        .Include(photo => photo.Country)
    //        .Include(photo => photo.Category)
    //        .Include(photo => photo.Palette)
    //        .Where(GetPredicateWhereClause(photoFilterRequest))
    //        .OrderBy(GetOrderByStatement(photoFilterRequest))
    //        .Skip((photoFilterRequest.PageIndex - 1) * photoFilterRequest.PageSize).Take(photoFilterRequest.PageSize)
    //        .ToListAsync();
    //}

    //public async Task<int> ByFilterCountAsync(PhotoFilterRequest photoFilterRequest)
    //{
    //    return await context.Photos
    //       .Where(GetPredicateWhereClause(photoFilterRequest))
    //       .CountAsync();
    //}

    //public List<Photo> MontagePhotos(Helpers.Enums.PhotoOrientation orientation, int numberOfPhotos, Guid userId)
    //{
    //    return [.. context.Photos
    //        .Include(photo => photo.Country)
    //        .Include(photo => photo.Favourites.Where(favourite => favourite.UserId == userId))
    //        .Where(p => p.UseInMontage == true && (int)orientation == p.Orientation)
    //        .Distinct()
    //        .OrderBy(x => Guid.NewGuid())
    //        .Take(numberOfPhotos)];
    //}

    //public bool Exists(string filename)
    //{
    //    return context.Photos.Any(photo => photo.FileName == filename);
    //}

    //public async Task<Photo?> FindByFilenameAsync(string filename)
    //{
    //    return await context.Photos.FirstOrDefaultAsync(photo => photo.FileName == filename);
    //}

    //public async Task<List<Photo>> GetLatestPhotos(int numberOfPhotos)
    //{
    //    return await (from photo in context.Photos
    //                  orderby photo.Id descending
    //                  select photo)
    //                  .Take(numberOfPhotos)
    //                  .ToListAsync();
    //}

    //private static string GetOrderByStatement(PhotoFilterRequest photoFilterRequest)
    //{
    //    string orderByStatement = "FileName asc";

    //    if (!string.IsNullOrEmpty(photoFilterRequest.SortField) && !string.IsNullOrEmpty(photoFilterRequest.SortOrder))
    //        orderByStatement = photoFilterRequest.SortField + " " + photoFilterRequest.SortOrder;

    //    return orderByStatement;
    //}

    //private static ExpressionStarter<Photo> GetPredicateWhereClause(PhotoFilterRequest photoFilterRequest)
    //{
    //    var predicate = PredicateBuilder.New<Photo>(true);

    //    if (photoFilterRequest.Id > 0)
    //        predicate = predicate.And(photo => photo.Id.Equals(photoFilterRequest.Id));

    //    if (photoFilterRequest.Iso > 0)
    //        predicate = predicate.And(photo => photo.Iso.Equals(photoFilterRequest.Iso));

    //    if (photoFilterRequest.PaletteId > 0)
    //        predicate = predicate.And(photo => photo.Palette != null && photo.Palette.Id.Equals(photoFilterRequest.PaletteId));

    //    if (photoFilterRequest.CategoryId > 0)
    //        predicate = predicate.And(photo => photo.Category != null && photo.Category.Id.Equals(photoFilterRequest.CategoryId));

    //    if (photoFilterRequest.CountryId > 0)
    //        predicate = predicate.And(photo => photo.Country != null && photo.Country.Id.Equals(photoFilterRequest.CountryId));

    //    if (photoFilterRequest.DateTaken != null)
    //        predicate = predicate.And(photo => photo.DateTaken.Equals(photoFilterRequest.DateTaken));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.FileName))
    //        predicate = predicate.And(photo => photo.FileName.Contains(photoFilterRequest.FileName));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.FocalLength))
    //        predicate = predicate.And(photo => photo.FocalLength != null && photo.FocalLength.Contains(photoFilterRequest.FocalLength));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.Lens))
    //        predicate = predicate.And(photo => photo.Lens != null && photo.Lens.Contains(photoFilterRequest.Lens));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.ApertureValue))
    //        predicate = predicate.And(photo => photo.ApertureValue != null && photo.ApertureValue.Contains(photoFilterRequest.ApertureValue));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.Camera))
    //        predicate = predicate.And(photo => photo.Camera != null && photo.Camera.Contains(photoFilterRequest.Camera));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.Title))
    //        predicate = predicate.And(photo => photo.Title != null && photo.Title.Contains(photoFilterRequest.Title));

    //    if (!string.IsNullOrEmpty(photoFilterRequest.ExposureTime))
    //        predicate = predicate.And(photo => photo.ExposureTime != null && photo.ExposureTime.Contains(photoFilterRequest.ExposureTime));

    //    return predicate;
    //}

    //public async Task AddAsync(Photo photo)
    //{
    //    await context.Photos.AddAsync(photo);
    //}

    //public void Update(Photo photo)
    //{
    //    context.Photos.Update(photo);
    //}

    //public async Task<Photo?> ByIdAsync(long id)
    //{
    //    return await context.Photos.FindAsync(id);
    //}
}