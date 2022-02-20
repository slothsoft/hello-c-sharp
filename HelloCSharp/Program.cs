using HelloCSharp.Persistence;
using HelloCSharp.Rest;
using Microsoft.AspNetCore.Builder;

namespace HelloCSharp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddFrontendServices();
        builder.AddDatabaseServices();
        builder.AddRestServices();

        var app = builder.Build();
        app.AddFrontendRoutes();
        app.EnsureDatabaseCreated();
        app.AddRestRoutes();

        app.Run();
    }
}