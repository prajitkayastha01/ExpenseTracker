using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserInvestmentRepository
    {
        Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId);
        Task<int> addUserInvestment(UserInvestment userInvestment);
    }
}
