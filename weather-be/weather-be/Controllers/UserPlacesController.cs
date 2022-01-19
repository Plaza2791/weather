using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using weather_be.Data.Dtos.UserPlaces;
using weather_be.Data.Entities;
using weather_be.Data.Repositories;
using weather_be.Services;

namespace weather_be.Controllers;

[ApiController]
[Route("userPlaces")]
public class UserPlacesController : ControllerBase
{
    private readonly IMeteoService _meteoService;
    private readonly IUserPlacesRepository _userPlacesRepository;

    public UserPlacesController(IUserPlacesRepository userPlacesRepository, IMeteoService meteoService)
    {
        _userPlacesRepository = userPlacesRepository;
        _meteoService = meteoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserPlace>>> GetAll()
    {
        var places = _userPlacesRepository.GetAll().Result;
        var placesWithTemps = places.Select(p =>
            new UserPlaceWithTemps(
                p.code,
                p.name,
                p.description,
                _meteoService.GetForecast(p.code).Result.MaxTempIn24Hours(),
                _meteoService.GetForecast(p.code).Result.MinTempIn24Hours()));
        return Ok(placesWithTemps);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<UserPlace>> Get(string code)
    {
        var userPlace = await _userPlacesRepository.Get(code);

        if (userPlace == null) return NotFound($"Saved place with code {code} not found");

        var userPlaceWithTemps = new UserPlaceWithTemps(
            userPlace.code,
            userPlace.name,
            userPlace.description,
            _meteoService.GetForecast(userPlace.code).Result.MaxTempIn24Hours(),
            _meteoService.GetForecast(userPlace.code).Result.MinTempIn24Hours());

        return Ok(userPlaceWithTemps);
    }

    [HttpPost]
    public async Task<ActionResult<UserPlace>> Post(UserPlace userPlace)
    {
        var savedPlace = await _userPlacesRepository.Get(userPlace.code);

        if (savedPlace != null) return BadRequest(new {error = "Vietovė su jau yra."});

        await _userPlacesRepository.Insert(userPlace);

        return Created($"userPlaces/{userPlace.code}", userPlace);
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<UserPlace>> Put(string code, UpdateUserPlaceDto userPlaceDto)
    {
        var userPlace = await _userPlacesRepository.Get(code);

        if (userPlace == null) return NotFound($"Saved place with code {code} not found");

        userPlace.description = userPlaceDto.description;

        try
        {
            var validationContext = new ValidationContext(userPlace);
            Validator.ValidateObject(userPlace, validationContext, true);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.ValidationResult);
        }

        await _userPlacesRepository.Update(userPlace);

        return Ok(userPlace);
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult> Delete(string code)
    {
        var userPlace = await _userPlacesRepository.Get(code);

        if (userPlace == null) return NotFound($"Saved place with code {code} not found");

        await _userPlacesRepository.Delete(userPlace);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete()
    {
        await _userPlacesRepository.DeleteAll();

        return NoContent();
    }
}