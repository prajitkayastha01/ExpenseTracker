namespace ExpenseTracker.Core.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionCategory { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
