using System;
using HelloCSharp.Api.Database;
using HelloCSharp.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database connection for both development and production
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
if (defaultConnection is null or "")
{
    builder.Services.AddDbContext<Database>(options => options.UseInMemoryDatabase("Filename=TestDatabase.db"));
}
else
{
    // TODO 1) figure out how to start app in production, 2) figure out how to define actual database 3) use both here
    throw new Exception("Actual database connection is not implemented yet! (defaultConnection=" + defaultConnection + ")");
}
builder.Services.AddScoped<IDatabase, Database>();

// Add the views and build the application
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

// Migrate the database to the correct version
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<Database>()) {
    if (context == null)
    {
        throw new Exception("The database context could not be registered correctly! (defaultConnection=" + defaultConnection + ")");
    }
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
