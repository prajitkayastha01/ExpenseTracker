using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs
{
    public class UserInvestmentDto
    {
        public int UserInvestmentId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PL { get; set; }
        public string? InvestmentTypeName { get; set; }  // ← from the JOIN
    }
}
