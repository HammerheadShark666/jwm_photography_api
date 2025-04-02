using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jwm_photography_api.Data.Contexts;
public class JwmPhotographyApiDbContextFactory : IDesignTimeDbContextFactory<JwmPhotographyApiDbContext>
{
    public JwmPhotographyApiDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensure correct directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<JwmPhotographyApiDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new JwmPhotographyApiDbContext(optionsBuilder.Options);
    }
}