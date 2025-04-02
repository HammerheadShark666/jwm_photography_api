using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class UserGalleryRepository(JwmPhotographyApiDbContext context) : IUserGalleryRepository
{
    public async Task<List<UserGallery>> AllSortedAsync()
    {
        return await context.UserGalleries.OrderBy(userGallery => userGallery.Name).ToListAsync();
    }

    public async Task<List<UserGallery>> AllSortedAsync(Guid userId)
    {
        return await context.UserGalleries.Where(userGallery => userGallery.UserId == userId).OrderBy(userGallery => userGallery.Name).ToListAsync();
    }

    public async Task<UserGallery?> GetAsync(Guid userId, long galleryId)
    {
        return await context.UserGalleries.Where(userGallery => userGallery.UserId == userId && userGallery.Id == galleryId).SingleOrDefaultAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId, string name)
    {
        return await context.UserGalleries
                                .Where(userGallery => userGallery.UserId == userId && userGallery.Name.Equals(name)).AnyAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId, long id, string name)
    {
        return await context.UserGalleries
                               .Where(userGallery => userGallery.Name.Equals(name)
                                    && !userGallery.Id.Equals(id) && userGallery.UserId == userId)
                               .AnyAsync();
    }

    public async Task<UserGallery?> GetFullGalleryAsync(Guid userId, long id)
    {
        return await context.UserGalleries
                                .Include(userGallery => userGallery.Photos)
                                .ThenInclude(photos => photos.Photo)
                                    .ThenInclude(photo => photo!.Country)
                                .SingleOrDefaultAsync(userGallery => userGallery.UserId == userId && userGallery.Id == id);
    }

    public async Task AddAsync(UserGallery gallery)
    {
        await context.UserGalleries.AddAsync(gallery);
    }

    public void Update(UserGallery userGallery)
    {
        context.UserGalleries.Update(userGallery);
    }

    public void Delete(UserGallery userGallery)
    {
        context.UserGalleries.Remove(userGallery);
    }

    public async Task<UserGallery?> ByIdAsync(long id)
    {
        return await context.UserGalleries.FindAsync(id);
    }
}