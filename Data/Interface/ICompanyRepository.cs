using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface ICompanyRepository
    {
        Task<CompanyInformation> Create(CompanyInformation company);
        Task<CompanyInformation> GetCompanyByOwnerId(ObjectId ownerId);
        Task<CompanyInformation> GetByIdAndOwnerId(ObjectId id, ObjectId ownerId);
    }
}