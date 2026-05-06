using Microsoft.EntityFrameworkCore;
using StatementService.Data;
using StatementService.Models;
using StatementService.Repositories.Interface;

namespace StatementService.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactions(
            long accountId, DateTime from, DateTime to, int skip, int take)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId &&
                            t.CreatedAt >= ToUtc(from) &&
                            t.CreatedAt <= ToUtc(to))
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(take)
                .AsTracking()
                .ToListAsync();
        }

        public async Task<(decimal credit, decimal debit)> GetSummary(
            long accountId, DateTime from, DateTime to)
        {
            var data = await _context.Transactions
                .Where(t => t.AccountId == accountId &&
                            t.CreatedAt >= ToUtc(from) &&
                            t.CreatedAt <= ToUtc(to))
                .GroupBy(t => 1)
                .Select(g => new
                {
                    Credit = g.Where(x => x.Type == "credit").Sum(x => x.Amount),
                    Debit = g.Where(x => x.Type == "debit").Sum(x => x.Amount)
                })
                .FirstOrDefaultAsync();

            return (data?.Credit ?? 0, data?.Debit ?? 0);
        }

        private DateTime ToUtc(DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
    }
}
