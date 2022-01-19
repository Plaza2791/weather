using weather_be.Data.Entities;

namespace weather_be.Data.Repositories;

public interface IUserPlacesRepository
{
    Task<IEnumerable<UserPlace>> GetAll();
    Task<UserPlace> Get(string code);
    Task Insert(UserPlace userPlace);
    Task Update(UserPlace userPlace);
    Task Delete(UserPlace userPlace);
    Task DeleteAll();
}