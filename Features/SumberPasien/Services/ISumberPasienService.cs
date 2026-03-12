using Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien.Models;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien.Services
{
    public interface ISumberPasienService
    {
        Task<List<Models.SumberPasien>> GetAllAsync();
        Task<Models.SumberPasien?> GetByKodeAsync(string kode);
    }
}
