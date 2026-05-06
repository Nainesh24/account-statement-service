using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatementService.Services.Interface;
namespace StatementService.Controllers
{
    [ApiController]
    [Route("api/v1/statements")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementServices _service;

        public StatementController(IStatementServices service)
        {
            _service = service;
        }

        [HttpGet("{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetStatement(
            long accountId,
            DateTime from,
            DateTime to,
            int page = 1,
            int pageSize = 20)
        {
            var result = await _service.GetStatement(accountId, from, to, page, pageSize);

            if (!result.Transactions.Any())
                return Ok(new { message = "No transactions found", data = result });

            return Ok(result);
        }
    }
}
