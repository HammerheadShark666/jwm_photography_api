using jwm_photography_api.Domain;
using Microsoft.EntityFrameworkCore;

namespace jwm_photography_api.Data.Contexts.ModelBuilders;

public class AccountModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Account>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Domain.Account>()
            .Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Domain.Account>()
            .Property(p => p.FirstName)
            .IsRequired()
            .HasColumnType("nvarchar(75)")
            .HasMaxLength(75);

        modelBuilder.Entity<Domain.Account>()
            .Property(p => p.LastName)
            .IsRequired()
            .HasColumnType("nvarchar(75)")
            .HasMaxLength(75);

        modelBuilder.Entity<Domain.Account>()
            .Property(p => p.Email)
            .IsRequired()
            .HasColumnType("nvarchar(75)")
            .HasMaxLength(75);

        modelBuilder.Entity<Domain.Account>()
            .Property(p => p.PasswordHash)
            .IsRequired()
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        modelBuilder.Entity<Account>()
            .Property(p => p.AcceptTerms)
            .HasColumnType("bit");

        modelBuilder.Entity<Account>()
            .Property(p => p.Role)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Entity<Account>()
            .Property(p => p.VerificationToken)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        modelBuilder.Entity<Account>()
            .Property(p => p.Verified)
            .HasColumnType("datetime2(7)");

        modelBuilder.Entity<Account>()
            .Property(p => p.IsAuthenticated)
            .HasColumnType("bit")
            .HasComputedColumnSql("CAST(CASE WHEN Verified IS NOT NULL OR PasswordReset IS NOT NULL THEN 1 ELSE 0 END AS bit)", stored: true)
            .HasConversion<bool>();

        modelBuilder.Entity<Account>()
            .Property(p => p.ResetToken)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        modelBuilder.Entity<Account>()
            .Property(p => p.ResetTokenExpires)
            .HasColumnType("datetime2(7)");

        modelBuilder.Entity<Account>()
            .Property(p => p.PasswordReset)
            .HasColumnType("datetime2(7)");

        modelBuilder.Entity<Account>()
            .Property(p => p.Created)
            .IsRequired()
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Account>()
            .Property(p => p.Updated)
            .HasColumnType("datetime2(7)");

        modelBuilder.Entity<Account>()
            .HasMany(c => c.RefreshTokens)
            .WithOne(a => a.Account);
    }
}
