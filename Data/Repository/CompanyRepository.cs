using MongoDB.Driver;
using Entities.Models;
using Data.Interface;
using MongoDB.Bson;
namespace Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IMongoCollection<CompanyInformation> _companyCollection;
        public CompanyRepository(MongoDbContext context)
        {
            _companyCollection = context.Language;
        }

        public async Task<CompanyInformation> Create(CompanyInformation company)
        {
            await _companyCollection.InsertOneAsync(company);
            return company;
        }

        public async Task<CompanyInformation> GetCompanyByOwnerId(ObjectId ownerId)
        {
            var filter = Builders<CompanyInformation>.Filter.Eq(c => c.OwnerId, ownerId);
            return await _companyCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<CompanyInformation?> GetByIdAndOwnerId(ObjectId id, ObjectId ownerId)
        {
            var filter = Builders<CompanyInformation>.Filter.And(
                Builders<CompanyInformation>.Filter.Eq(c => c.Id, id),
                Builders<CompanyInformation>.Filter.Eq(c => c.OwnerId, ownerId),
                Builders<CompanyInformation>.Filter.Eq(c => c.IsActive, true)
            );
            return await _companyCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}