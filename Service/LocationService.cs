using AutoMapper;
using Data.Interface;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Service.Dto.Location;
using Service.Helper;
using Service.Interface;

namespace Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;
        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }
        public async Task<CommonResponse> GetAllCountries(ObjectId countryId)
        {
            return new CommonResponse(StatusCodes.Status200OK, mapper.Map<List<CountryResponse>>(await locationRepository.GetAllCountries(countryId)));
        }
        public async Task<CommonResponse> GetAllStates(ObjectId countryId)
        {
            return new CommonResponse(StatusCodes.Status200OK, mapper.Map<ICollection<StateResponse>>(await locationRepository.GetAllStates(countryId)));
        }
        public async Task<CommonResponse> GetAllCities(ObjectId stateId)
        {
            return new CommonResponse(StatusCodes.Status200OK, mapper.Map<ICollection<CityResponse>>(await locationRepository.GetAllCities(stateId)));
        }
    }
}