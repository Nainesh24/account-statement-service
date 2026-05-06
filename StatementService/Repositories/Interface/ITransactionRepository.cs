using StatementService.Models;

namespace StatementService.Repositories.Interface
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetTransactions(
        long accountId, DateTime from, DateTime to, int skip, int take);

        Task<(decimal credit, decimal debit)> GetSummary(
            long accountId, DateTime from, DateTime to);
    }
}
