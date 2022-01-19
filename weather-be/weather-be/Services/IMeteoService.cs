using weather_be.Data.Entities.Meteo;
using weather_be.Models.Meteo;

namespace weather_be.Services;

public interface IMeteoService
{
    Task<List<Place>> GetPlaces();
    Task<Forecast> GetForecast(string code);
}