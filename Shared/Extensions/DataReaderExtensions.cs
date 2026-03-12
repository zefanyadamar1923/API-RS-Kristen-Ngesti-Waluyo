using System.Data;

namespace Api_RS_Kristen_Ngesti_Waluyo.Shared.Extensions
{
    public static class DataReaderExtensions
    {
        public static string? GetSafeString(this IDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == null || value is DBNull 
                    ? null 
                    : value.ToString()?.Trim();
            }
            catch
            {
                return null;
            }
        }
    }
}
