// Ignore Spelling: Jwm Api

using jwm_photography_api.Data.Contexts.ModelBuilders;
using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts;

public class JwmPhotographyApiDbContext(DbContextOptions<JwmPhotographyApiDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
    public DbSet<Palette> Palettes { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserGallery> UserGalleries { get; set; }
    public DbSet<UserGalleryPhoto> UserGalleryPhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        CountryModelBuilder.Build(modelBuilder);
        CategoryModelBuilder.Build(modelBuilder);
        PaletteModelBuilder.Build(modelBuilder);
        AccountModelBuilder.Build(modelBuilder);
        FavouriteModelBuilder.Build(modelBuilder);
        GalleryModelBuilder.Build(modelBuilder);
        GalleryPhotosModelBuilder.Build(modelBuilder);
        PhotoModelBuilder.Build(modelBuilder);
        RefreshTokenModelBuilder.Build(modelBuilder);
        UserGalleryModelBuilder.Build(modelBuilder);
        UserGalleryPhotosModelBuilder.Build(modelBuilder);
    }
}