
using Core.Models;
using DAL;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

namespace Visas
{
    //TODO
    // DTO Page  для отправки на клиент 
    // DTO Page для получения от клиента
    // Политика CORS ()
    // Подключение кеширования
    // Подключение логирования
    // Подумать о сборе почт и т д для рассылки

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddScoped<IUserPageService, UserPageService>();
            builder.Services.AddScoped<IAdminPageService, AdminPageService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<User>(builder.Configuration.GetSection("AdminCredentials"));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "testCookie";
                    options.LoginPath = "/admin/login";
                });

            builder.Services.AddCors();

            builder.Services.AddAuthorization();

            var app = builder.Build();


            app.UseCors(
                options =>
                {
                    options.AllowAnyOrigin();
                });

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
