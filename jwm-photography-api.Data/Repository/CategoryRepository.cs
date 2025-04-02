using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Repository;

public class CategoryRepository(JwmPhotographyApiDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> AllSortedAsync()
    {
        return await context.Categories.OrderBy(category => category.Name).ToListAsync();
    }
}