using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;
using UserServiceAPI.Application.EmailSet.EmaiGeneration;
using UserServiceAPI.Application.Services.Common;
using UserServiceAPI.Infrastructure.DataBase;
using UserServiceAPI.Application.JwtSet.JwtGeneration;


namespace UserServiceAPI.WebAPI.Registrations
{
    /// <summary>
    /// Класс регистрации компонентов микросервиса пользователя.
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

            services.AddIdentity<AppUsers, AppRoles>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
            })
                .AddEntityFrameworkStores<MutableInnoShopDbContext>()
                .AddDefaultTokenProviders();

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



            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailSender, EmailSender>();





        }
    }
}
