using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using TrickVault.Api.Constants;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data
{
    public abstract class TrickVaultDbContextBase : IdentityDbContext<ApplicationUser>
    {
        public TrickVaultDbContextBase(DbContextOptions options) : base(options) { }

        public static bool UseTestData { get; set; } = true;

        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TrickCategory> TrickCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public class TrickVaultSqlServerContext: TrickVaultDbContextBase
    {
        public TrickVaultSqlServerContext(DbContextOptions<TrickVaultSqlServerContext> options) : base(options) { }
    }

    public class TrickVaultPostgresContext : TrickVaultDbContextBase
    {
        public TrickVaultPostgresContext(DbContextOptions<TrickVaultPostgresContext> options) : base(options) { }
    }

    public class TrickVaultSqlServerContextFactory : IDesignTimeDbContextFactory<TrickVaultSqlServerContext>
    {
        public TrickVaultSqlServerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TrickVaultConnectionStringSqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<TrickVaultSqlServerContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(TrickVaultSqlServerContext).Assembly.FullName));

            return new TrickVaultSqlServerContext(optionsBuilder.Options);
        }
    }

    public class TrickVaultPostgresContextFactory : IDesignTimeDbContextFactory<TrickVaultPostgresContext>
    {
        public TrickVaultPostgresContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TrickVaultConnectionStringPostgres");

            var optionsBuilder = new DbContextOptionsBuilder<TrickVaultPostgresContext>();
            optionsBuilder.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(TrickVaultPostgresContext).Assembly.FullName));

            return new TrickVaultPostgresContext(optionsBuilder.Options);
        }
    }
}
