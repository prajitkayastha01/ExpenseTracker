using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Data
{
    public class UserInvestmentRepository: IUserInvestmentRepository

    {
        private readonly String _connectioString;

        public UserInvestmentRepository(String connectioString)
        {
            _connectioString = connectioString;
        }

        public async Task<List<UserInvestmentDto>> GetUserInvestmentsByUserId(int userId)
        {
            var investments = new List<UserInvestmentDto>();
            using (SqlConnection conn = new SqlConnection(_connectioString)) 
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
    }
}
