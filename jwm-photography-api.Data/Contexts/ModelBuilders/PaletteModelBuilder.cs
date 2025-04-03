using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class PaletteModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Palette>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Palette>()
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Palette>()
            .Property(p => p.Name)
            .IsRequired()
            .HasColumnType("nvarchar(75)")
            .HasMaxLength(75);
    }
}