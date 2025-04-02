using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class CountryRepository(JwmPhotographyApiDbContext context) : ICountryRepository
{
    public async Task<List<Country>> AllSortedAsync()
    {
        return await context.Countries.OrderBy(country => country.Name).ToListAsync();
    }
}