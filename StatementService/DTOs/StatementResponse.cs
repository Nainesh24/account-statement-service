namespace StatementService.DTOs
{
    public class StatementResponse
    {
        public long AccountId { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
