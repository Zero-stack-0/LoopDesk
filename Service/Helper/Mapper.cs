using AutoMapper;
using Entities.Models;
using Service.Dto.Role;
using Service.Dto.Subscription;
using Service.Dto.User;

namespace Service.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Users, UserResponse>();
            CreateMap<Role, RoleResponse>();
            CreateMap<Subscription, SubscriptionResponse>();
        }
    }
}