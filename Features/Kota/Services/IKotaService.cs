using Api_Sumber_Pasien.Features.Kota.Models;

namespace Api_Sumber_Pasien.Features.Kota.Services
{
    public interface IKotaService
    {
        Task<List<Models.Kota>> GetAllAsync();
        Task<Models.Kota?> GetByKodeAsync(string kode);
    }
}
