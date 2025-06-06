using AutoMapper;
using Entities.Models;
using Service.Dto.CompanyInformation;
using Service.Dto.Location;
using Service.Dto.Role;
using Service.Dto.Subscription;
using Service.Dto.User;
using Service.Dto.UserSubscription;

namespace Service.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Users, UserResponse>();
            CreateMap<Role, RoleResponse>();
            CreateMap<Subscription, SubscriptionResponse>();
            CreateMap<Country, CountryResponse>();
            CreateMap<State, StateResponse>();
            CreateMap<City, CityResponse>();
            CreateMap<UserSubscription, UserSubscriptionResponse>();
            CreateMap<CompanyInformation, CompanyResponse>();
            CreateMap<UserSubscription, UserSubscriptionResponse>();
        }
    }
}