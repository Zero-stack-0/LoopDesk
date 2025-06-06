using AutoMapper;
using Data.Interface;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Service.Dto.Subscription;
using Service.Dto.User;
using Service.Helper;
using Service.Interface;

namespace Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IMapper mapper;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.mapper = mapper;
        }

        public async Task<CommonResponse> Create(CreateSubscriptionRequest request, UserResponse requestor)
        {
            try
            {
                if (requestor is null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.USER_VALIDATIONS.REQUESTOR_NOT_FOUND);
                }

                if (requestor.Role is null || requestor.Role.Name != Constants.ROLE.ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                var validations = new List<(bool Condition, string Error)>
                {
                    (string.IsNullOrWhiteSpace(request.Name), Constants.SUBSCRIPTION_VALIDATIONS.NAME_REQUIRED),
                    (!request.IsTrial && request.Price <= 0, Constants.SUBSCRIPTION_VALIDATIONS.PRICE_MUST_BE_POSITIVE),
                    (request.ProjectsAllowed <= 0, Constants.SUBSCRIPTION_VALIDATIONS.PROJECTS_ALLOWED_MUST_BE_POSITIVE),
                    (request.UsersAllowed <= 0, Constants.SUBSCRIPTION_VALIDATIONS.USERS_ALLOWED_MUST_BE_POSITIVE),
                    (request.Name.Length > 100, Constants.SUBSCRIPTION_VALIDATIONS.NAME_TOO_LONG),
                    (request.Description?.Length > 500, Constants.SUBSCRIPTION_VALIDATIONS.DESCRIPTION_TOO_LONG),
                    (request.Price > 1000000, Constants.SUBSCRIPTION_VALIDATIONS.PRICE_TOO_HIGH)
                };

                foreach (var (condition, error) in validations)
                {
                    if (condition)
                    {
                        return new CommonResponse(StatusCodes.Status400BadRequest, null, error);
                    }
                }

                var doesSubscriptionExist = await subscriptionRepository.GetByName(request.Name);
                if (doesSubscriptionExist != null)
                {
                    return new CommonResponse(StatusCodes.Status409Conflict, null, Constants.SUBSCRIPTION_VALIDATIONS.SUBSCRIPTION_WITH_NAME_ALREADY_EXISTS);
                }

                var subscription = new Subscription(request.Name, ObjectId.TryParse(requestor.Id, out var objectId) ? objectId : ObjectId.Empty, request.Description, request.ProjectsAllowed, request.UsersAllowed, request.Price, request.IsTrial);

                var createdSubscription = await subscriptionRepository.CreateAsync(subscription);

                return new CommonResponse(StatusCodes.Status201Created, mapper.Map<SubscriptionResponse>(createdSubscription), Constants.SUBSCRIPTION_VALIDATIONS.CREATED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        public async Task<CommonResponse> GetAll(UserResponse requestor)
        {
            try
            {
                if (requestor is null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.USER_VALIDATIONS.REQUESTOR_NOT_FOUND);
                }

                if (requestor.Role is null || requestor.Role.Name != Constants.ROLE.COMPANY_ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                return new CommonResponse(StatusCodes.Status200OK, mapper.Map<ICollection<SubscriptionResponse>>(await subscriptionRepository.GetAllAsync()), Constants.SUBSCRIPTION_VALIDATIONS.FETCHED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }

        //Need to add check that no user has this subscription
        public async Task<CommonResponse> Delete(string id, UserResponse requestor)
        {
            try
            {
                if (requestor is null)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.USER_VALIDATIONS.REQUESTOR_NOT_FOUND);
                }

                if (requestor.Role is null || requestor.Role.Name != Constants.ROLE.ADMIN)
                {
                    return new CommonResponse(StatusCodes.Status403Forbidden, null, Constants.USER_VALIDATIONS.USER_NOT_AUTHORIZED);
                }

                var subscriptionId = ObjectId.TryParse(id, out var objectId) ? objectId : ObjectId.Empty;
                if (subscriptionId == ObjectId.Empty)
                {
                    return new CommonResponse(StatusCodes.Status400BadRequest, null, Constants.SUBSCRIPTION_VALIDATIONS.INVALID_ID);
                }

                var isDeleted = await subscriptionRepository.Delete(subscriptionId);
                if (!isDeleted)
                {
                    return new CommonResponse(StatusCodes.Status404NotFound, null, Constants.SUBSCRIPTION_VALIDATIONS.SUBSCRIPTION_NOT_FOUND);
                }

                return new CommonResponse(StatusCodes.Status200OK, null, Constants.SUBSCRIPTION_VALIDATIONS.DELETED_SUCCESSFULLY);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return new CommonResponse(StatusCodes.Status500InternalServerError, null, ex.Message);
            }
        }
    }
}