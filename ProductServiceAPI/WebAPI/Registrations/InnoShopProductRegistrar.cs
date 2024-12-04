using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.FiltrationSet;
using ProductServiceAPI.Infrastructure.Common;
using ProductServiceAPI.Interface;
using ProductServiceAPI.JwtSet;
using ProductServiceAPI.Validation;
using System.Text;

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
            services.AddScoped<IProductFiltration, Filtration>();

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });

        }

    }
}
