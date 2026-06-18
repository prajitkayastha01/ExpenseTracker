using ExpenseTracker.Core.DTOs;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserInvestmentService
    {
        Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId);
    }
}
