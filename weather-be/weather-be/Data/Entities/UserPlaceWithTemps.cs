namespace weather_be.Data.Entities;

public class UserPlaceWithTemps : UserPlace
{
    public UserPlaceWithTemps(string code, string name, string? description, double maxTemp, double minTemp) : base(
        code, name, description)
    {
        this.maxTemp = maxTemp;
        this.minTemp = minTemp;
    }

    public double maxTemp { get; }
    public double minTemp { get; }
}