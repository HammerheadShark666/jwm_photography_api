using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class UserGalleryPhotosModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<UserGalleryPhoto>()
        //    .Property(p => p.Id)
        //    .HasColumnType("int")
        //    .IsRequired()
        //    .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserGalleryPhoto>()
            .Property(p => p.UserGalleryId)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<UserGalleryPhoto>()
            .Property(p => p.PhotoId)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<UserGalleryPhoto>()
            .Property(p => p.Order)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<UserGalleryPhoto>()
            .HasOne(g => g.Photo)
            .WithMany(g => g.UserGalleryPhotos)
            .HasForeignKey(g => g.PhotoId);

        modelBuilder.Entity<UserGalleryPhoto>()
           .HasKey(p => new { p.UserGalleryId, p.PhotoId });

    }
}
