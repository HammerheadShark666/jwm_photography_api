using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    IPhotoRepository Photos { get; }
    ICountryRepository Countries { get; }
    ICategoryRepository Categories { get; }
    IPaletteRepository Palettes { get; }
    IGalleryRepository Galleries { get; }
    IGalleryPhotoRepository GalleryPhotos { get; }
    IFavouriteRepository Favourites { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IUserGalleryRepository UserGalleries { get; }
    IUserGalleryPhotoRepository UserGalleryPhotos { get; }
    Task<int> Complete();
}