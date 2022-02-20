using HelloCSharp.Frontend;
using HelloCSharp.Rest;
using Microsoft.AspNetCore.Builder;

namespace HelloCSharp;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddFrontendServices();
        builder.AddRestServices();

        var app = builder.Build();
        app.AddFrontendRoutes();
        app.AddRestRoutes();

        app.Run();
    }
}