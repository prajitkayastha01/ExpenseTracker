using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserInvestmentService
    {
        Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId);
        Task<int> AddUserInvestment(UserInvestment userInvestment);
    }
}
