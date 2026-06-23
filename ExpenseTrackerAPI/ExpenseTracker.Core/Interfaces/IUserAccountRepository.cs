using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserAccountRepository
    {
        List<UserAccount> GetAll();
        List<UserAccount> GetAccountsByUserId(int userId);
        Task<int> ValidateUserCredentialsAsync(LoginRequestDto request);
    }
}
