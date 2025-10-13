using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TrickVault.Api.Data
{
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
