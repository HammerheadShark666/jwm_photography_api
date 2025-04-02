using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Data.UnitOfWork.Interfaces;

namespace jwm_photography_api.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly JwmPhotographyApiDbContext _context;

    public IPhotoRepository Photos { get; private set; }
    public ICountryRepository Countries { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public IPaletteRepository Palettes { get; private set; }
    public IGalleryRepository Galleries { get; private set; }
    public IGalleryPhotoRepository GalleryPhotos { get; private set; }
    public IFavouriteRepository Favourites { get; private set; }
    public IAccountRepository Accounts { get; private set; }
    public IRefreshTokenRepository RefreshTokens { get; private set; }
    public IUserGalleryRepository UserGalleries { get; private set; }
    public IUserGalleryPhotoRepository UserGalleryPhotos { get; private set; }

    public UnitOfWork(JwmPhotographyApiDbContext context)
    {
        _context = context;
        Accounts = new AccountRepository(_context);
        Photos = new PhotoRepository(_context);
        Countries = new CountryRepository(_context);
        Categories = new CategoryRepository(_context);
        Palettes = new PaletteRepository(_context);
        Galleries = new GalleryRepository(_context);
        GalleryPhotos = new GalleryPhotoRepository(_context);
        Favourites = new FavouriteRepository(_context);
        RefreshTokens = new RefreshTokenRepository(_context);
        UserGalleries = new UserGalleryRepository(_context);
        UserGalleryPhotos = new UserGalleryPhotoRepository(_context);
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}