using Microsoft.EntityFrameworkCore;
using StatementService.Models;

namespace StatementService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
