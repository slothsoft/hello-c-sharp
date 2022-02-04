using HellCSharp.Persistence.Database;
using HelloCSharp.Api.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
