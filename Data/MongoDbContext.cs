

using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Entities.Models;

namespace Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;
        private readonly IOptions<MongoDbMappingConfiguration> config;

        public MongoDbContext(IOptions<MongoDbMappingConfiguration> config)
        {
            this.config = config;

            var client = new MongoClient(this.config.Value.ConnectionString);
            this.database = client.GetDatabase(this.config.Value.DatabaseName);
        }

        public IMongoCollection<Users> Users =>
            database.GetCollection<Users>(
                config.Value.Collections.FirstOrDefault(c => c == "users") ?? "users"
            );

        public IMongoCollection<Role> Role =>
            database.GetCollection<Role>(
                config.Value.Collections.FirstOrDefault(c => c == "role") ?? "role"
            );
        public IMongoCollection<Subscription> Subscription =>
            database.GetCollection<Subscription>(
                config.Value.Collections.FirstOrDefault(c => c == "subscription") ?? "subscription"
            );

        public IMongoCollection<Country> Country =>
            database.GetCollection<Country>(
                config.Value.Collections.FirstOrDefault(c => c == "country") ?? "country"
            );

        public IMongoCollection<State> State =>
            database.GetCollection<State>(
                config.Value.Collections.FirstOrDefault(c => c == "state") ?? "state"
            );
        public IMongoCollection<City> City =>
            database.GetCollection<City>(
                config.Value.Collections.FirstOrDefault(c => c == "city") ?? "city"
            );
    }
}
