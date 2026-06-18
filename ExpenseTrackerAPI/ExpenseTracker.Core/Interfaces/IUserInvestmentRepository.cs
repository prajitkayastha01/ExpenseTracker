using ExpenseTracker.Core.DTOs;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserInvestmentRepository
    {
        Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId);
    }
}
