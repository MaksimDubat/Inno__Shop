using UserServiceAPI.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using UserServiceAPI.Interface;
using UserServiceAPI.DataBaseAccess;


namespace UserServiceAPI.Registrations
{
    /// <summary>
    /// Класс регистрации компонентов приложения.
    /// </summary>
    public class InnoShopRegistrar
    {
        public static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MutableInnoShopDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddScoped<IUserRepository, UserRepository>();
            
        }
    }
}
