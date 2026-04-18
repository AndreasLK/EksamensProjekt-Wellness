using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{

    /// <summary>
    /// Provides a design-time factory for the <see cref="WellnessDbContext"/>.
    /// </summary>
    /// <remarks>
    /// This factory is strictly necessary for Entity Framework Core Tools to discover and 
    /// instantiate the <see cref="WellnessDbContext"/> when the startup project (UI)
    /// and the context project (Infrastructure) are separate
    /// </remarks>
    public class WellnessContextFactory : IDesignTimeDbContextFactory<WellnessDbContext>
    {

        /// <summary>
        /// Creates a new instance of the database context during development and migration tasks.
        /// </summary>
        /// <param name="args">Command-line arguments passed by the EF Core tools.</param>
        /// <returns>A configured instance of <see cref="WellnessDbContext"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the 'DefaultConnection' string is missing from the configuration or user secrets.
        /// </exception>
        public WellnessDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Server/Server"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<WellnessContextFactory>()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<WellnessDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new WellnessDbContext(optionsBuilder.Options);
        }
    }
}
