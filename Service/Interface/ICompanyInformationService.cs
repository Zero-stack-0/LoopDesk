using Service.Dto.CompanyInformation;
using Service.Dto.User;
using Service.Helper;

namespace Service.Interface
{
    public interface ICompanyInformationService
    {
        Task<CommonResponse> CreateCompany(CreateCompanyInformationRequest request, UserResponse requestor);
    }
}