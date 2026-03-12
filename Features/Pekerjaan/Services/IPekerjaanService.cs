using Api_RS_Kristen_Ngesti_Waluyo.Features.Pekerjaan.Models;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.Pekerjaan.Services
{
    public interface IPekerjaanService
    {
        Task<List<Models.Pekerjaan>> GetAllAsync();
        Task<Models.Pekerjaan?> GetByKodeAsync(string kode);
    }
}
