using DotNetEnv;
using Microsoft.OpenApi.Models;
using Api_Sumber_Pasien.Features.Kota.Services;
using Api_Sumber_Pasien.Features.Pekerjaan.Services;
using Api_Sumber_Pasien.Features.SumberPasien.Services;
using Api_Sumber_Pasien.Features.Kota;
using Api_Sumber_Pasien.Features.Pekerjaan;
using Api_Sumber_Pasien.Features.SumberPasien;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Register Feature Services
builder.Services.AddScoped<IKotaService, KotaService>();
builder.Services.AddScoped<IPekerjaanService, PekerjaanService>();
builder.Services.AddScoped<ISumberPasienService, SumberPasienService>();

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

// Map Feature Endpoints
app.MapKotaEndpoints();
app.MapPekerjaanEndpoints();
app.MapSumberPasienEndpoints();

app.Run();