using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services
{
    public class UserInvestmentService : IUserInvestmentService
    {
        private readonly IUserInvestmentRepository _userInvestmentRepository;

        public UserInvestmentService(IUserInvestmentRepository userInvestmentRepository)
        {
            _userInvestmentRepository = userInvestmentRepository;
        }

        public async Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId)
        {
            return await _userInvestmentRepository.GetUserInvestmentsByUserId(userId);
        }

        public async Task<int> AddUserInvestment(UserInvestment userInvestment)
        {
            userInvestment.CurrentPrice = userInvestment.BuyPrice;
            return await _userInvestmentRepository.addUserInvestment(userInvestment);
        }
    }
}
