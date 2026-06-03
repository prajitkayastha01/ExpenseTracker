using System;

namespace ExpenseTracker.Core.Models
{
    public class UserInvestment
    {
        public int UserInvestmentId { get; set; }
        public int UserId { get; set; }
        public int InvestmentTypeId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
