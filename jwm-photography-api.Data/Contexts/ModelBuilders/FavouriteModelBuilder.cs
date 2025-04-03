using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class FavouriteModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Favourite>()
            .Property(p => p.AccountId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        modelBuilder.Entity<Domain.Favourite>()
            .Property(p => p.PhotoId)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Entity<Domain.Favourite>()
           .HasOne(u => u.Photo);

        modelBuilder.Entity<Favourite>().HasKey(fv => new { fv.AccountId, fv.PhotoId });
    }
}
