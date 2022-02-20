namespace HelloCSharp.Rest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddRestServices();

        var app = builder.Build();
        app.AddRestRoutes();

        app.Run();
    }
}