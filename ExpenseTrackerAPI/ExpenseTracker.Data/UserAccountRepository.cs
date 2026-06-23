using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Data
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly string _connectionString;

        public UserAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<UserAccount> GetAll()
        {
            var accounts = new List<UserAccount>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM UserAccount", conn);
                SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read()) 
                    {
                        UserAccount account = new UserAccount();

                        account.UserAccountId = (int)reader["UserAccountId"];
                        account.UserId = (int)reader["UserId"];
                        account.AccountTypeId = (int)reader["AccountTypeId"];
                        account.Status = (bool)reader["Status"];
                        account.InsertDate = (DateTime)reader["InsertDate"];

                        accounts.Add(account);
                    }
                }
            }
            return accounts;
        }

        public List<UserAccount> GetAccountsByUserId(int userId)
        {
            var accounts = new List<UserAccount>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM UserAccount WHERE UserId = @UserId";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        UserAccount accountDetail = new UserAccount();

                        accountDetail.UserAccountId = Convert.ToInt32(reader["UserAccountId"]);
                        accountDetail.UserId = Convert.ToInt32(reader["UserId"]);
                        accountDetail.AccountTypeId = Convert.ToInt32(reader["AccountTypeId"]);
                        accountDetail.Status = Convert.ToBoolean(reader["Status"]);
                        accountDetail.InsertDate = Convert.ToDateTime(reader["InsertDate"]);

                        accounts.Add(accountDetail);
                    }

                }
            }

            return accounts;
        }

        public async Task<int> ValidateUserCredentialsAsync(string connectionString, LoginRequestDto loginDto)
        {
           
            string query = "SELECT u.UserId FROM [User] u JOIN UserCredential ua ON u.UserId = ua.UserId WHERE u.Email = @Email AND ua.PasswordHash = @Password";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", loginDto.Email);
                    cmd.Parameters.AddWithValue("@Password", loginDto.Password);

                    await conn.OpenAsync();
                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result); // Returns valid UserId
                    }
                    return 0; // Invalid credentials
                }
            }
        }
    }
}
