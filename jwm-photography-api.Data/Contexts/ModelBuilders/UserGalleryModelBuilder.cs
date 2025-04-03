using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class UserGalleryModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserGallery>()
            .HasKey(p => new { p.Id });

        modelBuilder.Entity<UserGallery>()
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserGallery>()
            .Property(p => p.AccountId)
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        modelBuilder.Entity<UserGallery>()
          .Property(p => p.Name)
          .IsRequired()
          .HasColumnType("nvarchar(150)")
          .HasMaxLength(150);

        modelBuilder.Entity<UserGallery>()
            .Property(p => p.Description)
            .HasColumnType("nvarchar(1000)")
            .HasMaxLength(1000);

        modelBuilder.Entity<UserGallery>()
            .HasMany(c => c.Photos);
    }
}
