using StatementService.DTOs;
using StatementService.Repositories.Interface;
using StatementService.Services.Interface;

namespace StatementService.Services
{
    public class StatementServices: IStatementServices
    {
        private readonly ITransactionRepository _repo;
        private readonly ILogger<StatementServices> _logger;

        public StatementServices(ITransactionRepository repo, ILogger<StatementServices> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<StatementResponse> GetStatement(
            long accountId, DateTime from, DateTime to, int page, int pageSize)
        {
            if ((to - from).TotalDays > 30)
                throw new Exception("Max 30 days allowed");

            var skip = (page - 1) * pageSize;

            var transactions = await _repo.GetTransactions(accountId, from, to, skip, pageSize);
            var summary = await _repo.GetSummary(accountId, from, to);

            _logger.LogInformation("Fetched {Count} transactions for account {AccountId}",
                transactions.Count, accountId);

            return new StatementResponse
            {
                AccountId = accountId,
                TotalCredit = summary.credit,
                TotalDebit = summary.debit,
                Transactions = transactions.Select(t => new TransactionDto
                {
                    Amount = t.Amount,
                    Type = t.Type,
                    Date = t.CreatedAt,
                    Description = t.Description
                }).ToList(),
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
