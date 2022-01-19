using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using weather_be.Data.Entities.Meteo;
using weather_be.Models.Meteo;

namespace weather_be.Services
{
    public class MeteoService : IMeteoService
    {
        private readonly HttpClient _client = new HttpClient();
        JsonSerializerOptions _options = new JsonSerializerOptions();

        public MeteoService()
        {
            _options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
        }

        public async Task<List<Place>> GetPlaces()
        {
            var res = await _client.GetStringAsync("https://api.meteo.lt/v1/places");

            List<Place> places = JsonSerializer.Deserialize<List<Place>>(res);

            return places;
        }

        public async Task<Forecast> GetForecast(string code)
        {
            var res = await _client.GetStringAsync($"https://api.meteo.lt/v1/places/{code}/forecasts/long-term");
            
            var forecast = JsonSerializer.Deserialize<Forecast>(res, _options);

            return forecast;
        }

        public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Debug.Assert(typeToConvert == typeof(DateTime));
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
