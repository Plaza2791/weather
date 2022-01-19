namespace weather_be.Data.Entities.Meteo
{
    public class ForecastTimestamp
    {
        public DateTime forecastTimeUtc { get; set; }
        public double airTemperature { get; set; }
        public int windSpeed { get; set; }
        public int windGust { get; set; }
        public int windDirection { get; set; }
        public int cloudCover { get; set; }
        public int seaLevelPressure { get; set; }
        public int relativeHumidity { get; set; }
        public double totalPrecipitation { get; set; }
        public string conditionCode { get; set; }
    }
}
