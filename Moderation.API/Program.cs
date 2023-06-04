using FavoriteLiterature.Moderation.Data;
using FavoriteLiterature.Moderation.Extensions;
using FavoriteLiterature.Moderation.Extensions.Builder;
using FavoriteLiterature.Moderation.Extensions.Builder.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new Exception("Connection string is missing.");
}

builder.Services.AddControllers();
builder.Services.AddDbContext<FavoriteLiteratureModerationDbContext>(options => options.UseNpgsql(connectionString));
builder.AddSwagger();
builder.AddRepositories();
builder.AddMediatr();
builder.AddAutoMapper();
builder.AddNormalizeRoute();
builder.AddRabbitMq();
builder.AddAttachmentStorage();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandlingMiddleware();
app.MapControllers();
app.Run();