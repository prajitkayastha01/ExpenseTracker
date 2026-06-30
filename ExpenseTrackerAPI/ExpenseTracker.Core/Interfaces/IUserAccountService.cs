using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserAccountService
    {
        List<UserAccount>GetAccountsByUserId(int userId);
        // Task<int> ValidateUserCredentialsAsync(LoginRequestDto loginData);

        Task<string> AuthenticateUserAsync(LoginRequestDto loginData);
        Task<(bool Success, string Message)> RegisterAsync(UserRegisterDto data);
    }
}
