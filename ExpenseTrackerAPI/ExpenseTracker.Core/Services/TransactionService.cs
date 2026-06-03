using System.Runtime.CompilerServices;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<TransactionDto> GetTransactionByUserAccountId(int userAccountId)
        {
            return _transactionRepository.GetTransactionByUserAccountId(userAccountId);
        }

        public async Task<decimal> GetBalance(int userAccountId)
        {
            return await _transactionRepository.GetBalance(userAccountId);
        }

        public async Task<int> AddTransactionAsync(Transaction transaction)
        {
            transaction.IsDeleted = false;
            return await _transactionRepository.AddTransactionAsync(transaction);
        }
    }
}
