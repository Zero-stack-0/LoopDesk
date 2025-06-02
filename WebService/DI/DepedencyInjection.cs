using Service.Interface;
using Data.Interface;
using Data.Repository;
using Service;
using Data;

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

            //repositories
            services.AddScoped<IUserRepository, UsersRepository>();

            services.AddScoped<MongoDbContext>();
        }
    }
}