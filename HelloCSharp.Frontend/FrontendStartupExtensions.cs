using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloCSharp.Frontend;

public static class FrontendStartupExtensions
{
    public static void AddFrontendServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        // Add the views and build the application
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        
        builder.Services.AddLocalization();
    }
    
    public static void AddFrontendRoutes(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/App/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        
        // configure Localization
        var supportedCultures = new[] { "en", "de" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        app.UseRequestLocalization(localizationOptions);

        app.MapControllerRoute(
            "default",
            "{controller=App}/{action=Index}/{id?}"
        );
    }
}