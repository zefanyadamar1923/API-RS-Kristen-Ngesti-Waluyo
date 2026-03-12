using Api_RS_Kristen_Ngesti_Waluyo.Features.Kota.Models;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.Kota.Services
{
    public interface IKotaService
    {
        Task<List<Models.Kota>> GetAllAsync();
        Task<Models.Kota?> GetByKodeAsync(string kode);
    }
}
