using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserAccountRepository
    {
        List<UserAccount> GetAll();
        List<UserAccount> GetAccountsByUserId(int userId);
        // Task<int> ValidateUserCredentialsAsync(LoginRequestDto loginData);

        Task<(int UserId, string PasswordHash)> GetUserAuthDetailsByEmailAsync(string email);
        Task<int> RegisterUserAsync(UserRegisterDto user, string passwordHash);
        Task<bool> EmailExistsAsync(string email);
    }
}
