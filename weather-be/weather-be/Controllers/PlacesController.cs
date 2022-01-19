using Microsoft.AspNetCore.Mvc;
using weather_be.Data.Dtos.Places;
using weather_be.Services;

namespace weather_be.Controllers;

[ApiController]
[Route("places")]
public class PlacesController : ControllerBase
{
    private readonly IMeteoService _meteoService;

    public PlacesController(IMeteoService meteoService)
    {
        _meteoService = meteoService;
    }

    [HttpGet]
    public async Task<IEnumerable<PlaceDto>> GetAll()
    {
        var places = await _meteoService.GetPlaces();
        var lithuanianPlaces = places.Where(p => p.countryCode.Equals("LT"));
        return lithuanianPlaces.Select(p => new PlaceDto(p.code, p.name));
    }
}