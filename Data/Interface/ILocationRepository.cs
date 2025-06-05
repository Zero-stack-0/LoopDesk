using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface ILocationRepository
    {
        Task<ICollection<Country>> GetAllCountries(ObjectId countryId);
        Task<ICollection<State>> GetAllStates(ObjectId countryId);
        Task<ICollection<City>> GetAllCities(ObjectId stateId);
        Task<Country> GetCountryById(ObjectId countryId);
        Task<State> GetStateById(ObjectId stateId);
        Task<City> GetCityById(ObjectId cityId);
    }
}