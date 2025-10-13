using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.MappingProfiles;
using TrickVault.Api.Models;
using TrickVault.Api.Models.Configuration;
using TrickVault.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureDatabaseServices(builder);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
if (string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException("JwtSettings:Key is not configured.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ITricksService, TricksService>();
builder.Services.AddScoped<IUsersService, UsersService>();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDevServer",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
        );
});

var app = builder.Build();

app.MapGroup("api/defaultauth").MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactDevServer");

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureDatabaseServices(WebApplicationBuilder builder)
{
    // Configure Connection String & setup DbContext
    var isMac = OperatingSystem.IsMacOS();

    if (isMac)
    {
        var connectionString = builder.Configuration.GetConnectionString("TrickVaultConnectionStringPostgres");

        builder.Services.AddDbContext<TrickVaultPostgresContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(TrickVaultPostgresContext).Assembly.FullName)));

        builder.Services.AddScoped<TrickVaultDbContextBase>(provider =>
            provider.GetRequiredService<TrickVaultPostgresContext>());

        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TrickVaultPostgresContext>();
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("TrickVaultConnectionStringSqlServer");

        builder.Services.AddDbContext<TrickVaultSqlServerContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(TrickVaultSqlServerContext).Assembly.FullName)));

        builder.Services.AddScoped<TrickVaultDbContextBase>(provider =>
            provider.GetRequiredService<TrickVaultSqlServerContext>());

        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TrickVaultSqlServerContext>();
    }
}