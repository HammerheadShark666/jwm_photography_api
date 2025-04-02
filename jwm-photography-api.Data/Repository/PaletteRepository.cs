using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class PaletteRepository(JwmPhotographyApiDbContext context) : IPaletteRepository
{
    public async Task<List<Palette>> AllSortedAsync()
    {
        return await context.Palettes.OrderBy(palette => palette.Name).ToListAsync();
    }
}