using MauiHybrid;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

public class AlumnoApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;

    public AlumnoApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://www.mauihybrid.somee.com/api";
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Alumno>> GetAlumnosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/alumnos");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Alumno>>(json, _jsonOptions) ?? new List<Alumno>();
        }
        catch (Exception ex)
        {
            // Log del error
            System.Diagnostics.Debug.WriteLine($"Error al obtener alumnos: {ex.Message}");
            return new List<Alumno>();
        }
    }

    public async Task<Alumno?> GetAlumnoByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/alumnos/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Alumno>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al obtener alumno {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddAlumnoAsync(Alumno alumno)
    {
        try
        {
            var json = JsonSerializer.Serialize(alumno, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/alumnos", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al agregar alumno: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateAlumnoAsync(Alumno alumno)
    {
        try
        {
            var json = JsonSerializer.Serialize(alumno, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/alumnos/{alumno.Id}", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al actualizar alumno: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAlumnoAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/alumnos/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al eliminar alumno {id}: {ex.Message}");
            return false;
        }
    }
}