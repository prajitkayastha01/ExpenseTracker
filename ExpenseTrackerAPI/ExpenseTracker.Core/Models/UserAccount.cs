using System;


namespace ExpenseTracker.Core.Models
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public int UserId { get; set; }
        public int AccountTypeId { get; set; }
        public bool Status { get; set; }
        public DateTime InsertDate { get; set; }

    }
}
