using AutoMapper;
using Data.Interface;
using Entities;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Service.Dto.User;
using Service.Dto.UserSubscription;
using Service.Helper;
using Service.Interface;

namespace Service
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository userSubscriptionRepository;
        private readonly ICompanyRepository companyInformationRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository, ICompanyRepository companyInformationRepository,
        ISubscriptionRepository subscriptionRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.companyInformationRepository = companyInformationRepository;
            this.subscriptionRepository = subscriptionRepository;
            this.userRepository = userRepository;
            this.userSubscriptionRepository = userSubscriptionRepository;
            this.mapper = mapper;
        }

        public async Task<CommonResponse> Create(CreateUserSubscription request, UserResponse requestor)
        {
            try
            {
                if (requestor is null || requestor.Role.Name != Constants.ROLE.COMPANY_ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                var owner = await userRepository.GetByEmail(requestor.Email);
                if (owner is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.USER_VALIDATIONS.USER_NOT_FOUND);
                }

                var subscription = await subscriptionRepository.GetById(ObjectId.TryParse(request.SubscriptionId, out ObjectId subscriptionId) ? subscriptionId : ObjectId.Empty);
                if (subscription is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.SUBSCRIPTION_VALIDATIONS.SUBSCRIPTION_NOT_FOUND);
                }

                var getCompanyByRequestorId = await companyInformationRepository.GetByIdAndOwnerId(ObjectId.TryParse(request.CompanyId, out ObjectId companyId) ? companyId : ObjectId.Empty, ObjectId.TryParse(requestor.Id, out ObjectId ownerId) ? ownerId : ObjectId.Empty);
                if (getCompanyByRequestorId is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.COMPANY_VALIDATIONS.COMPANY_NOT_FOUND);
                }

                var userSubscriptionByCompanyAndUserId = await userSubscriptionRepository.GetByUserIdAndCompanyId(owner.Id, companyId);
                if (userSubscriptionByCompanyAndUserId is not null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_ALREADY_EXISTS);
                }

                DateTime startDate = DateTime.UtcNow;
                DateTime? endDate = subscription.IsTrial ? startDate.AddDays(30) : null;

                var userSubscription = new UserSubscription(owner.Id, companyId, subscriptionId, startDate, endDate, request.BillingCycle, subscription.IsTrial);

                var createdUserSubscription = await userSubscriptionRepository.Create(userSubscription);
                if (createdUserSubscription is null)
                {
                    return new CommonResponse(StatusCodes.Status500InternalServerError, null, Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_CREATION_FAILED);
                }

                owner.UserProfileSetUpStep = UserProfileSetUpStep.Subscription;

                var updatedUser = await userRepository.Update(owner);

                return new CommonResponse(StatusCodes.Status201Created, createdUserSubscription, Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_CREATED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, $"{Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_CREATION_FAILED}: {ex.Message}");
            }
        }

        public async Task<CommonResponse> GetByCompanyIdAndUserId(GetByCompanyAndUserId request, UserResponse requestor)
        {
            try
            {
                if (requestor is null || requestor.Role.Name != Constants.ROLE.COMPANY_ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                var userSubscription = await userSubscriptionRepository.Get(ObjectId.TryParse(request.UserId, out ObjectId userObjectId) ? userObjectId : ObjectId.Empty, ObjectId.TryParse(request.CompanyId, out ObjectId companyObjectId) ? companyObjectId : ObjectId.Empty);
                if (userSubscription is null)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_NOT_FOUND);
                }

                return new CommonResponse(StatusCodes.Status200OK, mapper.Map<UserSubscriptionResponse>(userSubscription), Constants.USER_SUBSCRIPTION_VALIDATIONS.USER_SUBSCRIPTION_FETCHED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }
    }
}