using weather_be.Data.Entities.Meteo;

namespace weather_be.Models.Meteo;

public class Place
{
    public string code { get; set; }
    public string name { get; set; }
    public string administrativeDivision { get; set; }
    public string country { get; set; }
    public string countryCode { get; set; }
    public Coordinates coordinates { get; set; }
}