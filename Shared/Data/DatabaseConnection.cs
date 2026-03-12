using Microsoft.Data.SqlClient;

namespace Api_RS_Kristen_Ngesti_Waluyo.Shared.Data
{
    public class DatabaseConnection
    {
        public static string GetConnectionString()
        {
            return $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                   $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                   $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                   $"Password={Environment.GetEnvironmentVariable("DB_PASS")};" +
                   "TrustServerCertificate=True";
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
