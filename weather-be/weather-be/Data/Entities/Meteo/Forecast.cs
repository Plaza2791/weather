using weather_be.Models.Meteo;

namespace weather_be.Data.Entities.Meteo
{
    public class Forecast
    {
        public Place place { get; set; }
        public string forecastType { get; set; }
        public DateTime forecastCreationTimeUtc { get; set; }
        public IList<ForecastTimestamp> forecastTimestamps { get; set; }

        private IEnumerable<ForecastTimestamp> TwentyFourHourForecastTimestamps()
        {
            var currentTime = DateTime.UtcNow;
            var untilTime = DateTime.UtcNow.AddHours(24);
            var dayForecasts = forecastTimestamps.Where(ft => ft.forecastTimeUtc >= currentTime && ft.forecastTimeUtc <= untilTime);
            return dayForecasts;
        }

        public double MaxTempIn24Hours()
        {
            return TwentyFourHourForecastTimestamps().Max(ft => ft.airTemperature);
        }

        public double MinTempIn24Hours()
        {
            return TwentyFourHourForecastTimestamps().Min(ft => ft.airTemperature);
        }
    }
}
