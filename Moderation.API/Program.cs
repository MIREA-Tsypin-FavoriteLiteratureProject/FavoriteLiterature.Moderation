using FavoriteLiterature.Moderation.Extensions;
using FavoriteLiterature.Moderation.Extensions.Builder;
using FavoriteLiterature.Moderation.Extensions.Builder.Common;
using FavoriteLiterature.Moderation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

builder.AddPostgresDatabase();
builder.Services.AddControllers();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearerAuthentication(builder.Configuration);

builder.AddRolePolicy();
builder.AddSwagger();
builder.AddRepositories();
builder.AddMediatr();
builder.AddAutoMapper();
builder.AddCustomMiddlewares();
builder.AddNormalizeRoute();
builder.AddRabbitMq();
builder.AddAttachmentStorage();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.SeedDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();