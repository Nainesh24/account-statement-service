using StatementService.DTOs;

namespace StatementService.Services.Interface
{
    public interface IStatementServices
    {
        Task<StatementResponse> GetStatement(
        long accountId, DateTime from, DateTime to, int page, int pageSize);
    }
}
