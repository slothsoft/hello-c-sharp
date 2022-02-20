using HelloCSharp.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace HelloCSharp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddFrontendServices();
        builder.AddDatabaseServices();

        var app = builder.Build();
        app.AddFrontendRoutes();
        app.EnsureDatabaseCreated();

        app.Run();
    }
}