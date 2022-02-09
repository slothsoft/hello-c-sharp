using HellCSharp.Persistence;using HelloCSharp.Rest;

var builder = WebApplication.CreateBuilder(args);
new Startup(builder.Configuration).ConfigureServices(builder.Services);

var app = builder.Build();

// Migrate the database to the correct version
app.Services.EnsureDatabaseCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
