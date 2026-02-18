using Microsoft.Data.SqlClient;
using DotNetEnv;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API RS Kristen Ngesti Waluyo",
        Version = "v1",
        Description = "API Terintegrasi - Kota, Pekerjaan & Sumber Pasien"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

string connectionString = $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                         $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                         $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                         $"Password={Environment.GetEnvironmentVariable("DB_PASS")};" +
                         "TrustServerCertificate=True";

// --- ENDPOINT: DATA KOTA ---
app.MapGet("/api/kota", async () =>
{
    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        
        string sql = "SELECT vc_kode, vc_kota, vc_propinsi FROM PubKota";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var listKota = new List<object>();

        while (await reader.ReadAsync())
        {
            listKota.Add(new {
                vc_kode = reader["vc_kode"].ToString()?.Trim(),
                vc_kota = reader["vc_kota"].ToString()?.Trim(),
                vc_propinsi = reader["vc_propinsi"].ToString()?.Trim()
            });
        }
        return Results.Ok(listKota);
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Kota")
.WithOpenApi(operation => new(operation) {
    Summary = "Ambil Semua Daftar Kota",
    Description = "Klik 'Try it out' lalu 'Execute' untuk melihat seluruh data kota."
});

app.MapGet("/api/kota/{kode}", async (string kode) =>
{
    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand("SELECT * FROM PubKota WHERE RTRIM(vc_kode) = @kode", conn);
        cmd.Parameters.AddWithValue("@kode", kode.Trim());
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Results.Ok(new {
                vc_kode = reader["vc_kode"].ToString()?.Trim(),
                vc_kota = reader["vc_kota"].ToString()?.Trim(),
                vc_propinsi = reader["vc_propinsi"].ToString()?.Trim()
            });
        }
        return Results.NotFound(new { message = "Kota tidak ditemukan" });
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Kota")
.WithOpenApi(operation => new(operation)
{
    Summary = "Cari Satu Kota",
    Description = "Masukkan kode (misal: 1) untuk mencari data spesifik."
});

// --- ENDPOINT: DATA PEKERJAAN ---
app.MapGet("/api/pekerjaan", async () =>
{
    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        
        string sql = "SELECT vc_kode, vc_pekerjaan FROM PubKerja";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var listKerja = new List<object>();

        while (await reader.ReadAsync())
        {
            listKerja.Add(new {
                vc_kode = reader["vc_kode"].ToString()?.Trim(),
                vc_pekerjaan = reader["vc_pekerjaan"].ToString()?.Trim()
            });
        }
        return Results.Ok(listKerja);
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Pekerjaan")
.WithOpenApi(operation => new(operation) {
    Summary = "Ambil Semua Daftar Pekerjaan",
    Description = "Klik 'Execute' untuk melihat seluruh daftar pekerjaan."
});

app.MapGet("/api/pekerjaan/{kode}", async (string kode) =>
{
    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        
        using var cmd = new SqlCommand("SELECT vc_kode, vc_pekerjaan FROM PubKerja WHERE RTRIM(vc_kode) = @kode", conn);
        cmd.Parameters.AddWithValue("@kode", kode.Trim());
        
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Results.Ok(new {
                vc_kode = reader["vc_kode"].ToString()?.Trim(),
                vc_pekerjaan = reader["vc_pekerjaan"].ToString()?.Trim()
            });
        }
        return Results.NotFound(new { message = "Pekerjaan tidak ditemukan" });
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Pekerjaan")
.WithOpenApi(operation => new(operation) {
    Summary = "Cari Pekerjaan berdasarkan Kode",
    Description = "Masukkan kode pekerjaan untuk melihat detailnya."
});

// --- ENDPOINT: SUMBER PASIEN ---
app.MapGet("/api/sumber_pasien", async () => 
{
    try 
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand("SELECT vc_kode, vc_sbpasien FROM PubSbpasien", conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var listSumber = new List<object>();

        while (await reader.ReadAsync())
        {
            listSumber.Add(new { 
                vc_kode = reader["vc_kode"].ToString()?.Trim(), 
                vc_sbpasien = reader["vc_sbpasien"].ToString()?.Trim() 
            });
        }
        return Results.Ok(listSumber);
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Sumber Pasien")
.WithOpenApi(operation => new(operation)
{
    Summary = "Ambil Semua Daftar Sumber Pasien",
    Description = "Menampilkan seluruh daftar tanpa filter."
});

app.MapGet("/api/sumber_pasien/{kode}", async (string kode) =>
{
    try 
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand("SELECT vc_kode, vc_sbpasien FROM PubSbpasien WHERE vc_kode = @kode", conn);
        cmd.Parameters.AddWithValue("@kode", kode);
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return Results.Ok(new { 
                vc_kode = reader["vc_kode"].ToString()?.Trim(), 
                vc_sbpasien = reader["vc_sbpasien"].ToString()?.Trim() 
            });
        }
        return Results.NotFound(new { message = "Data tidak ditemukan" });
    }
    catch (Exception ex) { return Results.Problem(ex.Message); }
})
.WithTags("Sumber Pasien")
.WithOpenApi(operation => new(operation)
{
    Summary = "Cari Satu Sumber Pasien",
    Description = "Masukkan kode (misal: 1) untuk mencari data spesifik."
});

app.Run();