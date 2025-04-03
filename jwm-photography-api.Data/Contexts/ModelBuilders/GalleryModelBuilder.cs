using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class GalleryModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gallery>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Gallery>()
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Gallery>()
            .Property(p => p.Name)
            .IsRequired()
            .HasColumnType("nvarchar(75)")
            .HasMaxLength(75);

        modelBuilder.Entity<Gallery>()
            .Property(p => p.Description)
            .HasColumnType("nvarchar(1000)")
            .HasMaxLength(1000);

        modelBuilder.Entity<Gallery>()
            .HasMany(c => c.Photos);
    }
}
