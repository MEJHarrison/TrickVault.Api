using Microsoft.EntityFrameworkCore;

namespace TrickVault.Api.Data
{
    public class TrickVaultPostgresContext : TrickVaultDbContextBase
    {
        public TrickVaultPostgresContext(DbContextOptions<TrickVaultPostgresContext> options) : base(options) { }
    }
}
