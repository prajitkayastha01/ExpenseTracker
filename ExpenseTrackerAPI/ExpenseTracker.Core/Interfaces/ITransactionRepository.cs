using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface ITransactionRepository
    {
        List<TransactionDto> GetTransactionByUserAccountId(int userAccountId);

        Task<decimal> GetBalance(int userAccountId);

        Task<int> AddTransactionAsync(Transaction transaction);
        Task<int> DeleteTransaction(int transactionId);
    }
}
