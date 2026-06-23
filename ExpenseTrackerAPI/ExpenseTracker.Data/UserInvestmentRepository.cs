using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Data
{
    public class UserInvestmentRepository: IUserInvestmentRepository

    {
        private readonly String _connectionString;

        public UserInvestmentRepository(String connectioString)
        {
            _connectionString = connectioString;
        }

        public async Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId)
        {
            var investments = new List<UserInvestmentDto>();
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            { 
                conn.Open();

                string sql = "Select (ui.CurrentPrice - ui.BuyPrice) * ui.Quantity as PL,it.investmentType, ui.Symbol, ui.UserInvestmentId, ui.BuyPrice,ui.CurrentPrice,ui.PurchaseDate,ui.Quantity from UserInvestment as ui Inner join InvestmentType as it on it.InvestmentTypeId = ui.InvestmentTypeId where ui.UserId = @userId order by PL desc";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (reader.Read())
                        {
                            UserInvestmentDto investment = new UserInvestmentDto();

                            investment.UserInvestmentId = Convert.ToInt32(reader["UserInvestmentId"]);
                            investment.PL = Convert.ToDecimal(reader["Pl"]);
                            investment.Quantity = Convert.ToDecimal(reader["Quantity"]);
                            investment.BuyPrice = Convert.ToDecimal(reader["BuyPrice"]);
                            investment.InvestmentTypeName = Convert.ToString(reader["investmentType"]) ?? string.Empty;
                            investment.CurrentPrice = Convert.ToDecimal(reader["CurrentPrice"]);
                            investment.Symbol = Convert.ToString(reader["Symbol"]) ?? string.Empty;
                            investment.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);

                            investments.Add(investment);
                        }
                    }

                    return investments;

                }
            }
        }

        public async Task<int>  addUserInvestment(UserInvestment userInvestment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO UserInvestment (UserId,InvestmentTypeId,Symbol,Quantity,BuyPrice,CurrentPrice,PurchaseDate) VALUES (@UserId,@InvestmentTypeId,@Symbol,@Quantity,@BuyPrice,@CurrentPrice,@PurchaseDate)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userInvestment.UserId);
                        cmd.Parameters.AddWithValue("@InvestmentTypeId", userInvestment.InvestmentTypeId);
                        cmd.Parameters.AddWithValue("@Symbol", userInvestment.Symbol);
                        cmd.Parameters.AddWithValue("@Quantity", userInvestment.Quantity);
                        cmd.Parameters.AddWithValue("@BuyPrice", userInvestment.BuyPrice);
                        cmd.Parameters.AddWithValue("@CurrentPrice", userInvestment.CurrentPrice);
                        cmd.Parameters.AddWithValue("@PurchaseDate",userInvestment.PurchaseDate);

                        return await cmd.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error in addUserInvestment" + ex);
                return -1;
            }
        }
    }
}
