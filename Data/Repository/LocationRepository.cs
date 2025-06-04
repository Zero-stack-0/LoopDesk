using Data.Interface;
using Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMongoCollection<State> _stateCollection;
        private readonly IMongoCollection<City> _cityCollection;
        private readonly IMongoCollection<Country> _countryCollection;

        public LocationRepository(MongoDbContext context)
        {
            _stateCollection = context.State;
            _cityCollection = context.City;
            _countryCollection = context.Country;
        }

        public async Task<ICollection<Country>> GetAllCountries(ObjectId countryId)
        {
            var filter = Builders<Country>.Filter.Eq(c => c.Id, countryId);
            return await _countryCollection.Find(filter).Sort(Builders<Country>.Sort.Ascending(r => r.Name)).ToListAsync();
        }
        public async Task<ICollection<State>> GetAllStates(ObjectId countryId)
        {
            var filter = Builders<State>.Filter.Eq(s => s.countryId, countryId);
            return await _stateCollection.Find(filter).Sort(Builders<State>.Sort.Ascending(r => r.name)).ToListAsync();
        }
        public async Task<ICollection<City>> GetAllCities(ObjectId stateId)
        {
            var filter = Builders<City>.Filter.Eq(c => c.stateId, stateId);
            return await _cityCollection.Find(filter).Sort(Builders<City>.Sort.Ascending(e => e.name)).ToListAsync();
        }
    }
}