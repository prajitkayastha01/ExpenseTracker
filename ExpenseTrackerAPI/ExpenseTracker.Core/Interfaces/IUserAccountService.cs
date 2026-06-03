using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserAccountService
    {
        List<UserAccount>GetAccountsByUserId(int userId);
    }
}
