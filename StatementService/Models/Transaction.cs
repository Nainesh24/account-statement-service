using System.ComponentModel.DataAnnotations.Schema;

namespace StatementService.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("account_id")]
        public long AccountId { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("type")]
        public string Type { get; set; } // credit/debit

        [Column("description")]
        public string Description { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
