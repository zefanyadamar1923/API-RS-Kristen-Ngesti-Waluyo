using Api_Sumber_Pasien.Features.Pekerjaan.Models;

namespace Api_Sumber_Pasien.Features.Pekerjaan.Services
{
    public interface IPekerjaanService
    {
        Task<List<Models.Pekerjaan>> GetAllAsync();
        Task<Models.Pekerjaan?> GetByKodeAsync(string kode);
    }
}
