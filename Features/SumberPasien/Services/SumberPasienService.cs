using Api_RS_Kristen_Ngesti_Waluyo.Shared.Data;
using Api_RS_Kristen_Ngesti_Waluyo.Shared.Extensions;
using Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien.Models;
using Microsoft.Data.SqlClient;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien.Services
{
    public class SumberPasienService : ISumberPasienService
    {
        public async Task<List<Models.SumberPasien>> GetAllAsync()
        {
            var listSumber = new List<Models.SumberPasien>();
            
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            using var cmd = new SqlCommand("SELECT vc_kode, vc_sbpasien FROM PubSbpasien", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                listSumber.Add(new Models.SumberPasien
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_sbpasien = reader.GetSafeString("vc_sbpasien")
                });
            }

            return listSumber;
        }

        public async Task<Models.SumberPasien?> GetByKodeAsync(string kode)
        {
            using var conn = DatabaseConnection.CreateConnection();
            await conn.OpenAsync();
            
            using var cmd = new SqlCommand("SELECT vc_kode, vc_sbpasien FROM PubSbpasien WHERE vc_kode = @kode", conn);
            cmd.Parameters.AddWithValue("@kode", kode);
            using var reader = await cmd.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                return new Models.SumberPasien
                {
                    vc_kode = reader.GetSafeString("vc_kode"),
                    vc_sbpasien = reader.GetSafeString("vc_sbpasien")
                };
            }

            return null;
        }
    }
}
