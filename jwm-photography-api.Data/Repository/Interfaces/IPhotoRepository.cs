using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IPhotoRepository
{
    Task<List<Photo>> GetRandomPhotosAsync(int numberOfLandscape, int numberOfPortrait, int numberOfSquare);
    Task<bool> ExistsAsync(int id);
}