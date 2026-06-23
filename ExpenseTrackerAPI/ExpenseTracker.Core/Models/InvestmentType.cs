using System;

namespace ExpenseTracker.Core.Models
{
    public class InvestmentType
    {
        public int InvestmentTypeId { get; set; }
        public required string InvestmentTypeName { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
