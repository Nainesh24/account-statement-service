namespace StatementService.DTOs
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
