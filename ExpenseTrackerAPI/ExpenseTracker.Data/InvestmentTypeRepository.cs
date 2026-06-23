using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Data
{
    public class InvestmentTypeRepository: IInvestmentTypeRepository
    {
        private readonly string _connectionString;

        public InvestmentTypeRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        public List<InvestmentType> GetAllInvestmentTypes()
        {
            var investmentTypes = new List<InvestmentType> ();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT InvestmentTypeId, InvestmentType, InsertDate FROM InvestmentType";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var investmentType = new InvestmentType
                        {
                        InvestmentTypeId = Convert.ToInt32(reader["InvestmentTypeId"]),
                        InvestmentTypeName = Convert.ToString(reader["Investmenttype"]) ?? String.Empty,
                        InsertDate = Convert.ToDateTime(reader["Insertdate"])

                        };

                        investmentTypes.Add(investmentType);
                    }

                }

                return investmentTypes;
            }
        }
    }
}
