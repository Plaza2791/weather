using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather_be.Data.Dtos.UserPlaces;
using weather_be.Data.Entities;
using weather_be.Data.Repositories;

namespace weather_be.Controllers
{
    [ApiController]
    [Route("userPlaces")]
    public class UserPlacesController : ControllerBase
    {
        private readonly IUserPlacesRepository _userPlacesRepository;

        public UserPlacesController(IUserPlacesRepository userPlacesRepository)
        {
            _userPlacesRepository = userPlacesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPlace>>> GetAll()
        {
            return Ok(_userPlacesRepository.GetAll().Result);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<UserPlace>> Get(string code)
        {
            var userPlace = await _userPlacesRepository.Get(code);

            if (userPlace == null)
            {
                return NotFound($"Saved place with code {code} not found");
            }

            return Ok(userPlace);
        }

        [HttpPost]
        public async Task<ActionResult<UserPlace>> Post(UserPlace userPlace)
        {
            var savedPlace = await _userPlacesRepository.Get(userPlace.code);

            if (savedPlace != null)
            {
                return BadRequest($"Place with code {userPlace.code} already exists");
            }

            await _userPlacesRepository.Insert(userPlace);

            return Created($"userPlaces/{userPlace.code}", userPlace);
        }

        [HttpPut("{code}")]
        public async Task<ActionResult<UserPlace>> Put(string code, UpdateUserPlaceDto userPlaceDto)
        {
            var userPlace = await _userPlacesRepository.Get(code);

            if (userPlace == null)
            {
                return NotFound($"Saved place with code {code} not found");
            }

            userPlace.description = userPlaceDto.description;

            try
            {
                var validationContext = new ValidationContext(userPlace);
                Validator.ValidateObject(userPlace, validationContext, validateAllProperties: true);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationResult);
            }

            _userPlacesRepository.Update(userPlace);

            return Ok(userPlace);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(string code)
        {
            var userPlace = await _userPlacesRepository.Get(code);

            if (userPlace == null)
            {
                return NotFound($"Saved place with code {code} not found");
            }

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
}
