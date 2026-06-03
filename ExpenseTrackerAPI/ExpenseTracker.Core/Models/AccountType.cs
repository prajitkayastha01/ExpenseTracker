using System;

namespace ExpenseTracker.Core.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public required string AccountTypeName { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
