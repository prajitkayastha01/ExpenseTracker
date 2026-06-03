using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.DTOs;

using Microsoft.Data.SqlClient;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;
        private string sql;
        public TransactionRepository(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        public List<TransactionDto> GetTransactionByUserAccountId(int userAccountId)
        { 
            var transaction = new List<TransactionDto>();
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                conn.Open();

                sql = "select t.TransactionId,tc.TransactionCategory,tc.type,t.note,t.TransactionAmount,t.insertdate from [transaction] t inner join transactionCategory tc on tc.TransactionCategoryId = t.TransactionCategoryId where t.UserAccountId = @userAccountId and t.isDeleted = 0";
                using (var command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@userAccountId", userAccountId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TransactionDto transactions = new TransactionDto();

                        transactions.TransactionId = Convert.ToInt32(reader["TransactionId"]);
                        transactions.TransactionCategory = Convert.ToString(reader["TransactionCategory"]) ?? string.Empty;
                        transactions.Type = Convert.ToString(reader["type"]) ?? string.Empty;
                        transactions.Note = reader["note"] == DBNull.Value ? string.Empty : Convert.ToString(reader["note"]) ?? string.Empty;
                        transactions.TransactionAmount = Convert.ToDecimal(reader["TransactionAmount"]);
                        transactions.InsertDate = Convert.ToDateTime(reader["insertDate"]);

                        transaction.Add(transactions);
                    }

                }
            }
            
            return transaction;
        }

        public async Task<decimal> GetBalance(int userAccountId)
        {
            //var balance = new Task<decimal>();
            using (var conn = new SqlConnection(_connectionString)) 
            { 
                 await conn.OpenAsync();

                sql = "select SUM(Case when tc.Type = 'Income' then t.TransactionAmount else 0 end) - sum(case when tc.Type = 'Expense' then t.TransactionAmount else 0 end) as Balance from [Transaction] as t inner join TransactionCategory as tc on tc.TransactionCategoryId = t.TransactionCategoryId where t.UserAccountId = @userAccountId and t.IsDeleted = 0";
                using (var command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@userAccountId", userAccountId);
                    var result = await command.ExecuteScalarAsync();

                    return result == DBNull.Value ? 0 : Convert.ToDecimal (result);
                }
            }
        }

        public async Task<int> AddTransactionAsync(Transaction transaction)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                sql = "INSERT INTO [Transaction] (UserAccountId, TransactionCategoryId, TransactionAmount, IsDeleted, Note, InsertDate) VALUES (@UserAccountId, @TransactionCategoryId, @TransactionAmount, 0, @Note, GETDATE())";
                using (var command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@UserAccountId", transaction.UserAccountId);
                    command.Parameters.AddWithValue("@TransactionCategoryId", transaction.TransactionCategoryId);
                    command.Parameters.AddWithValue("@TransactionAmount", transaction.TransactionAmount);
                    command.Parameters.AddWithValue("@Note", transaction.Note);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
