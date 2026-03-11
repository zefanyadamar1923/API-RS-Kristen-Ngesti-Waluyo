using Api_Sumber_Pasien.Features.Kota.Services;
using Api_Sumber_Pasien.Shared.Models.Response;

namespace Api_Sumber_Pasien.Features.Kota
{
    public static class KotaEndpoints
    {
        public static void MapKotaEndpoints(this WebApplication app)
        {
            app.MapGet("/api/kota", GetAllKota)
                .WithTags("Kota")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Ambil Semua Daftar Kota",
                    Description = "Klik 'Try it out' lalu 'Execute' untuk melihat seluruh data kota."
                })
                .WithName("GetAllKota");

            app.MapGet("/api/kota/{kode}", GetKotaByKode)
                .WithTags("Kota")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Cari Satu Kota",
                    Description = "Masukkan kode (misal: 1) untuk mencari data spesifik."
                })
                .WithName("GetKotaByKode");
        }

        private static async Task<IResult> GetAllKota(IKotaService kotaService)
        {
            try
            {
                var data = await kotaService.GetAllAsync();
                return Results.Ok(new ApiResponse<List<object>>(
                    success: true,
                    response: data.Cast<object>().ToList(),
                    code: 200,
                    message: "Data kota berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }

        private static async Task<IResult> GetKotaByKode(string kode, IKotaService kotaService)
        {
            try
            {
                var data = await kotaService.GetByKodeAsync(kode);
                if (data is null)
                    return Results.NotFound(new ApiErrorResponse(404, "Kota tidak ditemukan"));
                
                return Results.Ok(new ApiResponse<object>(
                    success: true,
                    response: (object)data,
                    code: 200,
                    message: "Data kota berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }
    }
}
