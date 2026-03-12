using Api_RS_Kristen_Ngesti_Waluyo.Shared.Data;
using Api_RS_Kristen_Ngesti_Waluyo.Shared.Extensions;
using Api_RS_Kristen_Ngesti_Waluyo.Features.Kota.Models;
using Microsoft.Data.SqlClient;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.Kota.Services
{
    public class KotaService : IKotaService
    {
        public async Task<List<Models.Kota>> GetAllAsync()
        {
            var listKota = new List<Models.Kota>();
            
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            string sql = "SELECT vc_kode, vc_kota, vc_propinsi FROM PubKota";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                listKota.Add(new Models.Kota
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_kota = reader.GetSafeString("vc_kota"),
                    vc_propinsi = reader.GetSafeString("vc_propinsi")
                });
            }

            return listKota;
        }

        public async Task<Models.Kota?> GetByKodeAsync(string kode)
        {
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            using var cmd = new SqlCommand("SELECT * FROM PubKota WHERE RTRIM(vc_kode) = @kode", conn);
            cmd.Parameters.AddWithValue("@kode", kode.Trim());
            using var reader = await cmd.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                return new Models.Kota
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_kota = reader.GetSafeString("vc_kota"),
                    vc_propinsi = reader.GetSafeString("vc_propinsi")
                };
            }

            return null;
        }
    }
}
