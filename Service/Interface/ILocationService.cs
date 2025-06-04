using Entities.Models;
using MongoDB.Bson;
using Service.Dto.Location;
using Service.Helper;

namespace Service.Interface
{
    public interface ILocationService
    {
        Task<CommonResponse> GetAllCountries(ObjectId countryId);
        Task<CommonResponse> GetAllStates(ObjectId countryId);
        Task<CommonResponse> GetAllCities(ObjectId stateId);
    }
}