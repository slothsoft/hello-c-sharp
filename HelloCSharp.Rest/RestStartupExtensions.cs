using HelloCSharp.Api.Database;
using HelloCSharp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Rest;

public static class RestStartupExtensions
{
    public static void AddRestServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure database connection
        builder.AddDatabaseServices();
    }

    public static void AddRestRoutes(this WebApplication app)
    {
        // Migrate the database to the correct version
        app.EnsureDatabaseCreated();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
    }
}