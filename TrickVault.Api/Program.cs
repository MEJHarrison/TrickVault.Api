using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Connection String & setup DbContext
var connectionString = builder.Configuration.GetConnectionString("TrickVaultConnectionString");
builder.Services.AddDbContext<TrickVaultDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

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
