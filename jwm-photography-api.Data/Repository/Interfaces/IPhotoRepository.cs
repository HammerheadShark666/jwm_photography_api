using jwm_photography_api.Domain;

namespace jwm_photography_api.Data.Repository.Interfaces;

public interface IPhotoRepository
{
    Task<List<Photo>> GetRandomPhotosAsync(int numberOfLandscape, int numberOfPortrait, int numberOfSquare);
    //Task<List<Photo>> AllAsync();
    //Task<int> CountAsync();
    //List<Photo> MontagePhotos(Enums.PhotoOrientation orientation, int numberOfPhotos, Guid userId);
    ////Task<List<Photo>> ByPagingAsync(PhotoFilterRequest photoFilterRequest);
    //Task<int> ByFilterCountAsync(PhotoFilterRequest photoFilterRequest);
    Task<bool> ExistsAsync(int id);
    //bool Exists(string Filename);
    //Task<Photo?> FindByFilenameAsync(string filename);
    //Task<List<Photo>> GetLatestPhotos(int numberOfPhotos);
    //Task AddAsync(Photo photo);
    //void Update(Photo photo);
    //Task<Photo?> ByIdAsync(long id);
}