using Service.Interface;
using Data.Interface;
using Data.Repository;
using Service;
using Data;
using WebService.Auth;

namespace WebService.DI
{
    public class DepedencyInjection
    {
        public DepedencyInjection()
        {

        }
        public static void InjectDependencies(IServiceCollection services)
        {
            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            //repositories
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            services.AddScoped<MongoDbContext>();
            services.AddScoped<GenerateJwtToken>();
            services.AddScoped<UserProfile>();
            services.AddAutoMapper(typeof(Service.Helper.Mapper).Assembly);

        }
    }
}