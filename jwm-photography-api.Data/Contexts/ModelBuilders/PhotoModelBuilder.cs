using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class PhotoModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Photo>()
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Photo>()
            .Property(p => p.FileName)
            .IsRequired()
            .HasColumnType("nvarchar(150)")
            .HasMaxLength(150);

        modelBuilder.Entity<Photo>()
            .Property(p => p.Title)
            .HasColumnType("nvarchar(150)")
            .HasMaxLength(150);

        modelBuilder.Entity<Photo>()
          .Property(p => p.Camera)
          .HasColumnType("nvarchar(100)")
          .HasMaxLength(100);

        modelBuilder.Entity<Photo>()
            .Property(p => p.Lens)
            .HasColumnType("nvarchar(150)")
            .HasMaxLength(150);

        modelBuilder.Entity<Photo>()
           .Property(p => p.ExposureProgram)
           .HasColumnType("nvarchar(25)")
           .HasMaxLength(25);

        modelBuilder.Entity<Photo>()
           .Property(p => p.ApertureValue)
           .HasColumnType("nvarchar(25)")
           .HasMaxLength(25);

        modelBuilder.Entity<Photo>()
           .Property(p => p.ExposureProgram)
           .HasColumnType("nvarchar(50)")
           .HasMaxLength(25);

        modelBuilder.Entity<Photo>()
           .Property(p => p.Iso)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .Property(p => p.DateTaken)
           .HasColumnType("datetime2(7)");

        modelBuilder.Entity<Photo>()
           .Property(p => p.FocalLength)
           .HasColumnType("nvarchar(10)")
           .HasMaxLength(15);

        modelBuilder.Entity<Photo>()
           .Property(p => p.Orientation)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .Property(p => p.Height)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .Property(p => p.Width)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .Property(p => p.UseInMontage)
           .HasColumnType("bit")
           .HasDefaultValue(0);

        modelBuilder.Entity<Photo>()
           .Property(p => p.CountryId)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .HasOne(u => u.Country);

        modelBuilder.Entity<Photo>()
           .Property(p => p.CategoryId)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .HasOne(u => u.Category);

        modelBuilder.Entity<Photo>()
           .Property(p => p.PaletteId)
           .HasColumnType("int");

        modelBuilder.Entity<Photo>()
           .HasOne(u => u.Palette);

    }
}
