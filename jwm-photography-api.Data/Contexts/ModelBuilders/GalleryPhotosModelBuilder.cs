using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class GalleryPhotosModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GalleryPhoto>()
            .HasKey(p => new { p.GalleryId, p.PhotoId });

        modelBuilder.Entity<GalleryPhoto>()
            .Property(p => p.GalleryId)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<GalleryPhoto>()
            .Property(p => p.PhotoId)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<GalleryPhoto>()
            .Property(p => p.Order)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<GalleryPhoto>()
            .HasOne(g => g.Gallery)
            .WithMany(g => g.Photos)
            .HasForeignKey(g => g.GalleryId);
    }
}
