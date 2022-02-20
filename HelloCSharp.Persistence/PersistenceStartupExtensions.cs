using HelloCSharp.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloCSharp.Persistence;

public static class PersistenceStartupExtensions
{
    /// <summary>
    /// Add a database connection to for the implementation defined in this project.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder" /> to add services to.</param>
    public static void AddDatabaseServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Configure database connection for both development and production
        if (connectionString is null or "")
        {
            builder.Services.AddDbContext<Database.DatabaseContext>(options =>
                options.UseInMemoryDatabase("Filename=TestDatabase.db"));
        }
        else
        {
            // TODO 1) figure out how to start app in production, 2) figure out how to define actual database 3) use both here
            throw new Exception("Actual database connection is not implemented yet! (defaultConnection=" +
                                connectionString + ")");
        }

        builder.Services.AddScoped<IDatabaseContext, Database.DatabaseContext>();
    }

    /// <summary>
    /// Add a database connection to for the implementation defined in this project.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication" /> to get services from.</param>
    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetService<Database.DatabaseContext>();
        if (context == null)
        {
            throw new Exception("The database context could not be registered correctly!");
        }

        context.Database.EnsureCreated();
    }
}