using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class GalleryRepository(JwmPhotographyApiDbContext context) : IGalleryRepository
{
    private readonly JwmPhotographyApiDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Gallery>> GetAllGalleriesSortedAsync()
    {
        return await _context.Galleries
                                .AsNoTracking()
                                .OrderBy(gallery => gallery.Name)
                                .ToListAsync();
    }

    public async Task<Gallery> GetGalleryAsync(long id)
    {
        return await _context.Galleries
                .AsNoTracking()
                .Include(gallery => gallery.Photos.OrderBy(c => c.Order))
                    .ThenInclude(photos => photos.Photo)
                        .ThenInclude(photo => photo.Country)
                .Include(gallery => gallery.Photos)
                    .ThenInclude(photos => photos.Photo)
                        .ThenInclude(photo => photo.Category)
                .Include(gallery => gallery.Photos)
                    .ThenInclude(photos => photos.Photo)
                        .ThenInclude(photo => photo.Palette)
                .SingleAsync(gallery => gallery.Id == id);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Galleries
                               .Where(a => a.Id.Equals(id))
                               .AsNoTracking()
                               .AnyAsync();
    }
}