using Api_RS_Kristen_Ngesti_Waluyo.Shared.Data;
using Api_RS_Kristen_Ngesti_Waluyo.Shared.Extensions;
using Api_RS_Kristen_Ngesti_Waluyo.Features.Pekerjaan.Models;
using Microsoft.Data.SqlClient;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.Pekerjaan.Services
{
    public class PekerjaanService : IPekerjaanService
    {
        public async Task<List<Models.Pekerjaan>> GetAllAsync()
        {
            var listKerja = new List<Models.Pekerjaan>();
            
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            string sql = "SELECT vc_kode, vc_pekerjaan FROM PubKerja";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                listKerja.Add(new Models.Pekerjaan
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_pekerjaan = reader.GetSafeString("vc_pekerjaan")
                });
            }

            return listKerja;
        }

        public async Task<Models.Pekerjaan?> GetByKodeAsync(string kode)
        {
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            using var cmd = new SqlCommand("SELECT vc_kode, vc_pekerjaan FROM PubKerja WHERE RTRIM(vc_kode) = @kode", conn);
            cmd.Parameters.AddWithValue("@kode", kode.Trim());
            using var reader = await cmd.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                return new Models.Pekerjaan
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_pekerjaan = reader.GetSafeString("vc_pekerjaan")
                };
            }

            return null;
        }
    }
}
