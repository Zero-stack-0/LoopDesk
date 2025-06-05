
using System.Text.RegularExpressions;
using Data.Interface;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Service.Dto.CompanyInformation;
using Service.Dto.User;
using Service.Helper;
using Service.Interface;


namespace Service
{
    public class CompanyInformationService : ICompanyInformationService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IUserRepository userRepository;
        public CompanyInformationService(ICompanyRepository companyRepository, ILocationRepository locationRepository, IUserRepository userRepository)
        {
            this.companyRepository = companyRepository;
            this.locationRepository = locationRepository;
            this.userRepository = userRepository;
        }

        public async Task<CommonResponse> CreateCompany(CreateCompanyInformationRequest request, UserResponse requestor)
        {
            try
            {
                if (requestor is null || requestor.Role.Name != Constants.ROLE.COMPANY_ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                var getCompanyByOwner = await companyRepository.GetCompanyByOwnerId(ObjectId.TryParse(requestor.Id, out var parsedOwnerId) ? parsedOwnerId : ObjectId.Empty);
                if (getCompanyByOwner is not null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.COMPANY_VALIDATIONS.COMAPNY_ALREADY_EXISTS);
                }

                var validationError = ValidateCompanyRequest(request);
                if (!string.IsNullOrEmpty(validationError))
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, validationError);
                }

                var country = await locationRepository.GetCountryById(ObjectId.TryParse(request.CountryId, out var parsedCountryId) ? parsedCountryId : ObjectId.Empty);
                if (country is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.COMPANY_VALIDATIONS.COUNTRY_NOT_FOUND);
                }

                var state = await locationRepository.GetStateById(ObjectId.TryParse(request.StateId, out var parsedStateId) ? parsedStateId : ObjectId.Empty);
                if (state is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.COMPANY_VALIDATIONS.STATE_NOT_FOUND);
                }

                var city = await locationRepository.GetCityById(ObjectId.TryParse(request.CityId, out var parsedCityId) ? parsedCityId : ObjectId.Empty);
                if (city is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.COMPANY_VALIDATIONS.CITY_NOT_FOUND);
                }

                var company = new CompanyInformation(request.Name, request.Domain, request.Address, request.Phone, request.LogoUrl, request.Description,
                    parsedCountryId, parsedStateId, parsedCityId, parsedOwnerId);

                var createdCompany = await companyRepository.Create(company);

                var owner = await userRepository.GetById(parsedOwnerId);
                if (owner is not null)
                {
                    owner.UserProfileSetUpStep = Entities.Enums.UserProfileSetUpStep.CompanyInfo;
                    await userRepository.Update(owner);
                }

                return new CommonResponse(StatusCodes.Status201Created, createdCompany, Constants.COMPANY_VALIDATIONS.CREATED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        private bool checkDomain(string domain)
        {
            var regex = new Regex(@"^(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$");
            return regex.IsMatch(domain);
        }

        private bool IsValidPhone(string phone)
        {
            var regex = new Regex(@"^(\+91[\-\s]?)?[6-9]\d{9}$");
            return regex.IsMatch(phone);
        }

        private string? ValidateCompanyRequest(CreateCompanyInformationRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return Constants.COMPANY_VALIDATIONS.NAME_REQUIRED;
            if (string.IsNullOrEmpty(request.Domain))
                return Constants.COMPANY_VALIDATIONS.DOMAIN_REQUIRED;
            if (string.IsNullOrEmpty(request.Address))
                return Constants.COMPANY_VALIDATIONS.ADDRESS_REQUIRED;
            if (string.IsNullOrEmpty(request.Phone))
                return Constants.COMPANY_VALIDATIONS.PHONE_REQUIRED;
            if (string.IsNullOrEmpty(request.LogoUrl))
                return Constants.COMPANY_VALIDATIONS.LOGOURL_REQUIRED;
            if (string.IsNullOrEmpty(request.Description))
                return Constants.COMPANY_VALIDATIONS.DESCRIPTION_REQUIRED;
            if (request.CountryId == null)
                return Constants.COMPANY_VALIDATIONS.COUNTRYID_REQUIRED;
            if (request.StateId == null)
                return Constants.COMPANY_VALIDATIONS.STATEID_REQUIRED;
            if (request.CityId == null)
                return Constants.COMPANY_VALIDATIONS.CITYID_REQUIRED;
            if (!checkDomain(request.Domain))
                return Constants.COMPANY_VALIDATIONS.DOMAIN_INVALID;
            if (!IsValidPhone(request.Phone))
                return Constants.COMPANY_VALIDATIONS.PHONE_INVALID;
            if (request.Name.Length > 50 || request.Name.Length < 3)
                return Constants.COMPANY_VALIDATIONS.NAME_REQUIREMENTS;
            if (request.Domain.Length > 100 || request.Domain.Length < 3)
                return Constants.COMPANY_VALIDATIONS.DOMAIN_REQUIREMENTS;
            if (request.Address.Length > 200 || request.Address.Length < 5)
                return Constants.COMPANY_VALIDATIONS.ADDRESS_REQUIREMENTS;
            if (request.Phone.Length > 15 || request.Phone.Length < 10)
                return Constants.COMPANY_VALIDATIONS.PHONE_REQUIREMENTS;

            return null;
        }
    }
}