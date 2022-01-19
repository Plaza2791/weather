using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using weather_be.Data;
using weather_be.Data.Repositories;
using weather_be.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<WeatherContext>();
    services.AddTransient<IMeteoService, MeteoService>();
    services.AddTransient<IUserPlacesRepository, UserPlacesRepository>();
    services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
}