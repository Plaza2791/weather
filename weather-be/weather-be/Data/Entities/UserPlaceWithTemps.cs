namespace weather_be.Data.Entities
{
    public class UserPlaceWithTemps : UserPlace
    {
        public double maxTemp { get; }
        public double minTemp { get; }

        public UserPlaceWithTemps(string code, string name, string? description, double maxTemp, double minTemp) : base(code, name, description)
        {
            this.maxTemp = maxTemp;
            this.minTemp = minTemp;
        }
    }
}
