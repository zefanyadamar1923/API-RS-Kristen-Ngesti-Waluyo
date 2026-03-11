using Api_Sumber_Pasien.Features.Pekerjaan.Services;
using Api_Sumber_Pasien.Shared.Models.Response;

namespace Api_Sumber_Pasien.Features.Pekerjaan
{
    public static class PekerjaanEndpoints
    {
        public static void MapPekerjaanEndpoints(this WebApplication app)
        {
            app.MapGet("/api/pekerjaan", GetAllPekerjaan)
                .WithTags("Pekerjaan")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Ambil Semua Daftar Pekerjaan",
                    Description = "Klik 'Execute' untuk melihat seluruh daftar pekerjaan."
                })
                .WithName("GetAllPekerjaan");

            app.MapGet("/api/pekerjaan/{kode}", GetPekerjaanByKode)
                .WithTags("Pekerjaan")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Cari Pekerjaan berdasarkan Kode",
                    Description = "Masukkan kode pekerjaan untuk melihat detailnya."
                })
                .WithName("GetPekerjaanByKode");
        }

        private static async Task<IResult> GetAllPekerjaan(IPekerjaanService pekerjaanService)
        {
            try
            {
                var data = await pekerjaanService.GetAllAsync();
                return Results.Ok(new ApiResponse<List<object>>(
                    success: true,
                    response: data.Cast<object>().ToList(),
                    code: 200,
                    message: "Data pekerjaan berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }

        private static async Task<IResult> GetPekerjaanByKode(string kode, IPekerjaanService pekerjaanService)
        {
            try
            {
                var data = await pekerjaanService.GetByKodeAsync(kode);
                if (data is null)
                    return Results.NotFound(new ApiErrorResponse(404, "Pekerjaan tidak ditemukan"));
                
                return Results.Ok(new ApiResponse<object>(
                    success: true,
                    response: (object)data,
                    code: 200,
                    message: "Data pekerjaan berhasil diambil"
                ));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ApiErrorResponse(500, $"Terjadi kesalahan: {ex.Message}"));
            }
        }
    }
}
