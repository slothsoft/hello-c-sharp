using HelloCSharp.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HelloCSharp.Persistence;

public static class DatabaseStartupExtensions
{
    /// <summary>
    /// Add a database connection to for the implementation defined in this project.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="connectionString">Null for in-memory database, or connection string for anything more permanent</param>
    public static void AddDatabaseService(this IServiceCollection services, string connectionString)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Configure database connection for both development and production
        if (connectionString is null or "")
        {
            services.AddDbContext<Database.Database>(options =>
                options.UseInMemoryDatabase("Filename=TestDatabase.db"));
        }
        else
        {
            // TODO 1) figure out how to start app in production, 2) figure out how to define actual database 3) use both here
            throw new Exception("Actual database connection is not implemented yet! (defaultConnection=" +
                                connectionString + ")");
        }

        services.AddScoped<IDatabase, Database.Database>();
    }

    /// <summary>
    /// Add a database connection to for the implementation defined in this project.
    /// </summary>
    /// <param name="services">The <see cref="IServiceProvider" /> to get services from.</param>
    public static void EnsureDatabaseCreated(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        using (var context = scope.ServiceProvider.GetService<Database.Database>())
        {
            if (context == null)
            {
                throw new Exception("The database context could not be registered correctly!");
            }

            context.Database.EnsureCreated();
        }
    }
}