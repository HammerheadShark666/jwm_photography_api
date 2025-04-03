using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;
public class RefreshTokenModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RefreshToken>()
           .Property(p => p.AccountId)
           .HasColumnType("uniqueidentifier")
           .IsRequired();

        modelBuilder.Entity<RefreshToken>()
           .HasOne(u => u.Account);

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.Token)
            .IsRequired()
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.Expires)
            .IsRequired()
            .HasColumnType("datetime2(7)");

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.IsExpired)
            .HasColumnType("bit")
            .HasComputedColumnSql("CAST(CASE WHEN GETDATE() >= Expires THEN 1 ELSE 0 END AS bit)")
            .HasConversion<bool>();

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.CreatedByIp)
            .IsRequired()
            .HasColumnType("nvarchar(25)")
            .HasMaxLength(25);
    }
}