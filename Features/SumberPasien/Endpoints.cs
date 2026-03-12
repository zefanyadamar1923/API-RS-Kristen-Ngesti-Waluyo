using Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien.Services;
using Api_RS_Kristen_Ngesti_Waluyo.Shared.Models.Response;

namespace Api_RS_Kristen_Ngesti_Waluyo.Features.SumberPasien
{
    public static class SumberPasienEndpoints
    {
        public static void MapSumberPasienEndpoints(this WebApplication app)
        {
            app.MapGet("/api/sumberpasien", GetAllSumberPasien)
                .WithTags("Sumber Pasien")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Ambil Semua Daftar Sumber Pasien",
                    Description = "Menampilkan seluruh daftar tanpa filter."
                })
                .WithName("GetAllSumberPasien");

            app.MapGet("/api/sumberpasien/{kode}", GetSumberPasienByKode)
                .WithTags("Sumber Pasien")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Cari Satu Sumber Pasien",
                    Description = "Masukkan kode (misal: 1) untuk mencari data spesifik."
                })
                .WithName("GetSumberPasienByKode");
        }

        private static async Task<IResult> GetAllSumberPasien(ISumberPasienService sumberPasienService)
        {
            try
            {
                var data = await sumberPasienService.GetAllAsync();
                return Results.Ok(new ApiResponse<List<object>>(
                    success: true,
                    response: data.Cast<object>().ToList(),
                    code: 200,
                    message: "Data sumber pasien berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }

        private static async Task<IResult> GetSumberPasienByKode(string kode, ISumberPasienService sumberPasienService)
        {
            try
            {
                var data = await sumberPasienService.GetByKodeAsync(kode);
                if (data is null)
                    return Results.NotFound(new ApiErrorResponse(404, "Data tidak ditemukan"));
                
                return Results.Ok(new ApiResponse<object>(
                    success: true,
                    response: (object)data,
                    code: 200,
                    message: "Data sumber pasien berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }
    }
}
