using System;

namespace ExpenseTracker.Core.Models
{
    public class TransactionCategory
    {
        public int TransactionCategoryId { get; set; }
        public string TransactionCategoryName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; }
    }
}
