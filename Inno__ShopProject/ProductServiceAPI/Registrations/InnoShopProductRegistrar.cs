using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Infrastructure.Common;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Registrations
{
    /// <summary>
    /// Класс регистрации компонентов микросервиса продуктов.
    /// </summary>
    public class InnoShopProductRegistrar
    {
        public static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MutableInnoShopProductDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
