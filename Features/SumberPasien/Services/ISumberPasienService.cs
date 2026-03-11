using Api_Sumber_Pasien.Features.SumberPasien.Models;

namespace Api_Sumber_Pasien.Features.SumberPasien.Services
{
    public interface ISumberPasienService
    {
        Task<List<Models.SumberPasien>> GetAllAsync();
        Task<Models.SumberPasien?> GetByKodeAsync(string kode);
    }
}
