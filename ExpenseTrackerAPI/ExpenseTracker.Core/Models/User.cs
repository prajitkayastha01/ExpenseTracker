using System;

namespace ExpenseTracker.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
