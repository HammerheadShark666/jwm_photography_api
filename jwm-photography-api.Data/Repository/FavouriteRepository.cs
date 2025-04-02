using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Dto.Request;

namespace jwm_photography_api.Data.Repository;

public class FavouriteRepository(JwmPhotographyApiDbContext context) : IFavouriteRepository
{
    public async Task<List<Photo>> GetFavouritePhotosAsync(Guid userId)
    {
        return await context.Favourites
                               .Include(favourite => favourite.Photo)
                               .ThenInclude(photo => photo.Country)
                               .Where(favourite => favourite.UserId == userId)
                               .Select(favourite => favourite.Photo)
                               .ToListAsync();

        //return await (from favourite in context.Favourites
        //              join photo in context.Photos
        //                    on favourite.PhotoId equals photo.Id
        //              join country in context.Countries
        //                    on photo.CountryId equals country.Id
        //              where favourite.UserId == userId
        //              select new Photo
        //              {
        //                  Id = photo.Id,
        //                  Title = photo.Title,
        //                  FileName = photo.FileName,
        //                  Orientation = photo.Orientation,
        //                  Country = new Country
        //                  {
        //                      Id = country.Id,
        //                      Name = country.Name
        //                  }
        //              }).ToListAsync();
    }

    public async Task<Favourite?> GetFavouritePhotoAsync(Guid userId, long photoId)
    {
        return await (from favourite in context.Favourites
                      where favourite.UserId == userId
                        && favourite.PhotoId == photoId
                      select favourite).SingleOrDefaultAsync();
    }

    public async Task<Boolean> ExistsAsync(Guid userId, long photoId)
    {
        return !await context.Favourites
                                .AsNoTracking()
                                .AnyAsync(x => x.UserId.Equals(userId) && x.PhotoId.Equals(photoId));
    }

    public async Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        return await context.Favourites
            .Include(favourite => favourite.Photo)
            .ThenInclude(photo => photo.Country)
            .Include(photo => photo.Photo.Category)
            .Include(photo => photo.Photo.Palette)
            .Where(GetPredicateWhereClause(userId, photoFilterRequest))
            .OrderBy(x => x.Photo.Title)
            .Skip((photoFilterRequest.PageIndex - 1) * photoFilterRequest.PageSize).Take(photoFilterRequest.PageSize)
            .Select(favourite => favourite.Photo)
            .ToListAsync();
    }

    public async Task<int> ByFilterCountAsync(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        return await context.Favourites
           .Where(GetPredicateWhereClause(userId, photoFilterRequest))
           .CountAsync();
    }

    private static ExpressionStarter<Favourite> GetPredicateWhereClause(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        var predicate = PredicateBuilder.New<Favourite>(true);

        predicate = predicate.And(favourite => favourite.UserId.Equals(userId));

        if (photoFilterRequest.Id > 0)
            predicate = predicate.And(favourite => favourite.Photo.Id.Equals(photoFilterRequest.Id));

        if (photoFilterRequest.Iso > 0)
            predicate = predicate.And(favourite => favourite.Photo.Iso.Equals(photoFilterRequest.Iso));

        if (photoFilterRequest.PaletteId > 0)
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Palette != null && favourite.Photo.Palette.Id.Equals(photoFilterRequest.PaletteId));

        if (photoFilterRequest.CategoryId > 0)
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Category != null && favourite.Photo.Category.Id.Equals(photoFilterRequest.CategoryId));

        if (photoFilterRequest.CountryId > 0)
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Country != null && favourite.Photo.Country.Id.Equals(photoFilterRequest.CountryId));

        if (photoFilterRequest.DateTaken != null)
            predicate = predicate.And(favourite => favourite.Photo.DateTaken.Equals(photoFilterRequest.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterRequest.FileName))
            predicate = predicate.And(favourite => favourite.Photo.FileName.Contains(photoFilterRequest.FileName));

        if (!string.IsNullOrEmpty(photoFilterRequest.FocalLength))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.FocalLength != null && favourite.Photo.FocalLength.Contains(photoFilterRequest.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterRequest.Lens))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Lens != null && favourite.Photo.Lens.Contains(photoFilterRequest.Lens));

        if (!string.IsNullOrEmpty(photoFilterRequest.ApertureValue))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.ApertureValue != null && favourite.Photo.ApertureValue.Contains(photoFilterRequest.ApertureValue));

        if (!string.IsNullOrEmpty(photoFilterRequest.Camera))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Camera != null && favourite.Photo.Camera.Contains(photoFilterRequest.Camera));

        if (!string.IsNullOrEmpty(photoFilterRequest.Title))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.Title != null && favourite.Photo.Title.Contains(photoFilterRequest.Title));

        if (!string.IsNullOrEmpty(photoFilterRequest.ExposureTime))
            predicate = predicate.And(favourite => favourite.Photo != null && favourite.Photo.ExposureTime != null && favourite.Photo.ExposureTime.Contains(photoFilterRequest.ExposureTime));

        return predicate;
    }
    public void Delete(Favourite favourite)
    {
        context.Favourites.Remove(favourite);
    }

    public async Task AddAsync(Favourite favourite)
    {
        await context.Favourites.AddAsync(favourite);
    }
}