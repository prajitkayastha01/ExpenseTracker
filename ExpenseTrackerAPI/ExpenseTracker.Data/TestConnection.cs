using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Data
{
    public class TestConnection
    {
        public void RunTest()
        {
            var connectionString = "Server=DESKTOP-VH1B1CG;Database=ExpenseTracker;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection SuccessFull");
            }
        }
    }
}
