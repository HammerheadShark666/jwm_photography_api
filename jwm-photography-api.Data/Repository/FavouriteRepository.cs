using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class FavouriteRepository(JwmPhotographyApiDbContext context) : IFavouriteRepository
{
    public async Task<List<Photo>> GetFavouritePhotosAsync(Guid accountId)
    {
        return await context.Favourites
                               .Include(favourite => favourite.Photo)
                               .ThenInclude(photo => photo.Country)
                               .Where(favourite => favourite.AccountId == accountId)
                               .Select(favourite => favourite.Photo)
                               .ToListAsync();
    }

    public async Task<Favourite?> GetFavouritePhotoAsync(Guid accountId, long photoId)
    {
        return await (from favourite in context.Favourites
                      where favourite.AccountId == accountId
                        && favourite.PhotoId == photoId
                      select favourite).SingleOrDefaultAsync();
    }

    public async Task<Boolean> ExistsAsync(Guid accountId, long photoId)
    {
        return !await context.Favourites
                                .AsNoTracking()
                                .AnyAsync(x => x.AccountId.Equals(accountId) && x.PhotoId.Equals(photoId));
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