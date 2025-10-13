using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TrickVault.Api.Data
{
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
}
