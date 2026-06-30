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

        //public async Task<int> ValidateUserCredentialsAsync(string connectionString, LoginRequestDto loginDto)
        //{
           
        //    string query = "SELECT u.UserId FROM [User] u JOIN UserCredential ua ON u.UserId = ua.UserId WHERE u.Email = @Email AND ua.PasswordHash = @Password";

        //    using (var conn = new SqlConnection(connectionString))
        //    {
        //        using (var cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@Email", loginDto.Email);
        //            cmd.Parameters.AddWithValue("@Password", loginDto.Password);

        //            await conn.OpenAsync();
        //            object result = await cmd.ExecuteScalarAsync();

        //            if (result != null && result != DBNull.Value)
        //            {
        //                return Convert.ToInt32(result); // Returns valid UserId
        //            }
        //            return 0; // Invalid credentials
        //        }
        //    }
        //}

        public async Task<(int UserId, string PasswordHash)> GetUserAuthDetailsByEmailAsync(string email)
        {
            string query = "SELECT u.UserId, ua.PasswordHash FROM [User] u JOIN UserCredential ua ON u.UserId = ua.UserId WHERE u.Email = @Email";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            int userId = Convert.ToInt32(reader["UserId"]);
                            string passwordHash = reader["PasswordHash"].ToString();
                            return (userId, passwordHash);
                        }
                    }
                }
            }
            return (0, null); // User not found
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(1) FROM [User] WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    await conn.OpenAsync();
                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }

        public async Task<int> RegisterUserAsync(UserRegisterDto user, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                // Using a SQL Transaction to ensure both inserts succeed or both fail
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string userSql = @"
                            INSERT INTO [User] (UserName, PhoneNo, Email, InsertDate) 
                            OUTPUT INSERTED.UserId 
                            VALUES (@UserName, @PhoneNo, @Email, GETDATE());";

                        int newUserId;
                        using (SqlCommand cmd = new SqlCommand(userSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserName", user.UserName);
                            cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                            cmd.Parameters.AddWithValue("@Email", user.Email);

                            newUserId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }

                        string authSql = @"
                            INSERT INTO UserCredential (UserId, PasswordHash, InsertDate) 
                            VALUES (@UserId, @PasswordHash, GETDATE());";

                        using (SqlCommand cmd = new SqlCommand(authSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", newUserId);
                            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                        return newUserId;
                    }
                    catch (SqlException)
                    {
                        await transaction.RollbackAsync();
                        return -1;
                    }
                }
            }
        }

    }
}
