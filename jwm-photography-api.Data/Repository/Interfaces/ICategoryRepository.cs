using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> AllSortedAsync();
}