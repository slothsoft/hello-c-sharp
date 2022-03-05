using HelloCSharp.Api.Database;
using HelloCSharp.Persistence.Database;
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
    /// <param name="connectionString">The database to connect too.</param>
    public static void AddDatabaseServices(this WebApplicationBuilder builder, string? connectionString = null)
    {
        var usedConnectionString = connectionString ?? builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Configure database connection for both development and production
        if (usedConnectionString is null or "")
        {
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase("Filename=TestDatabase.db"));
        }
        else
        {
            if (usedConnectionString.Equals("this value is set by Azure"))
            {
                throw new ArgumentException("Connection string was not set correctly: " + usedConnectionString);
            }
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySQL(usedConnectionString));
        }
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
        builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
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