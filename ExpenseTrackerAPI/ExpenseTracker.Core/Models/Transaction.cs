using System;

namespace ExpenseTracker.Core.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserAccountId { get; set; }
        public int TransactionCategoryId { get; set; }
        public decimal TransactionAmount { get; set; }
        public bool IsDeleted { get; set; }
        public string? Note { get; set; }
        public DateTime InsertDate { get; set; }

    }
}
