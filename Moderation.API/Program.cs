using FavoriteLiterature.Moderation.Data;
using FavoriteLiterature.Moderation.Extensions.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new Exception("Connection string is missing.");
}

builder.Services.AddControllers();
builder.Services
    .AddDbContext<FavoriteLiteratureModerationDbContext>(options => options.UseNpgsql(connectionString));
builder.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();
app.Run();