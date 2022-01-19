using Microsoft.EntityFrameworkCore;
using weather_be.Data.Entities;

namespace weather_be.Data.Repositories;

public class UserPlacesRepository : IUserPlacesRepository
{
    private readonly WeatherContext _weatherContext;

    public UserPlacesRepository(WeatherContext weatherContext)
    {
        _weatherContext = weatherContext;
    }

    public async Task<IEnumerable<UserPlace>> GetAll()
    {
        return _weatherContext.userPlaces;
    }

    public async Task<UserPlace> Get(string code)
    {
        return await _weatherContext.userPlaces.FirstOrDefaultAsync(p => p.code.Equals(code));
    }

    public async Task Insert(UserPlace userPlace)
    {
        _weatherContext.userPlaces.Add(userPlace);
        await _weatherContext.SaveChangesAsync();
    }

    public async Task Update(UserPlace userPlace)
    {
        _weatherContext.userPlaces.Update(userPlace);
        await _weatherContext.SaveChangesAsync();
    }

    public async Task Delete(UserPlace userPlace)
    {
        _weatherContext.userPlaces.Remove(userPlace);
        await _weatherContext.SaveChangesAsync();
    }

    public async Task DeleteAll()
    {
        _weatherContext.userPlaces.RemoveRange(_weatherContext.userPlaces);
        await _weatherContext.SaveChangesAsync();
    }
}