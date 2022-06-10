using _22SevenFincancialApp.Models.EntitySets;
using Microsoft.EntityFrameworkCore;

namespace _22SevenFincancialApp.Models.Context
{
  public class FinancialContext : DbContext
  {
    public FinancialContext(DbContextOptions<FinancialContext> options) : base(options) { }

    public DbSet<CustomerData> Customer { get; set; } = null!;
    public DbSet<CustomerAccountData> CustomerAccounts { get; set; } = null!;
    public DbSet<HistoryData> History { get; set; } = null!;
  }
}