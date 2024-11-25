
using Microsoft.EntityFrameworkCore;
using UserServiceAPI.CustomExceptionsFilter;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Registrations;

namespace UserServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            InnoShopRegistrar.RegisterRepositories(builder.Services, builder.Configuration);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            



            app.MapControllers();

            app.Run();
        }
    }
}
