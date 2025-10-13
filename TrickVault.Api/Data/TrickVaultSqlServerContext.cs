using Microsoft.EntityFrameworkCore;

namespace TrickVault.Api.Data
{
    public class TrickVaultSqlServerContext : TrickVaultDbContextBase
    {
        public TrickVaultSqlServerContext(DbContextOptions<TrickVaultSqlServerContext> options) : base(options) { }
    }
}
