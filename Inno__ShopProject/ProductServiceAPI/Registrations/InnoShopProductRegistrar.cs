using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Infrastructure.Common;
using ProductServiceAPI.Interface;
using ProductServiceAPI.Validation;

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
            services.AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<ProductFluentValidator>();
        }

    }
}
