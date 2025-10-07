using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.MappingProfiles;
using TrickVault.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Connection String & setup DbContext
var connectionString = builder.Configuration.GetConnectionString("TrickVaultConnectionString");
builder.Services.AddDbContext<TrickVaultDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ITricksService, TricksService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoryMappingProfile>();
    config.AddProfile<TrickMappingProfile>();
});

// Setup controllers to handle cyclical references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
