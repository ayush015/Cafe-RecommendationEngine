using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.DAL;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.Service.Admin;
using RecommendationEngineServer.Service.Chef;
using RecommendationEngineServer.Service.Employee;
using RecommendationEngineServer.Service.Login;
using RecommendationEngineServer.Service.Notifications;

namespace RecommendationEngineServer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();
            var socket = app.Services.GetRequiredService<SocketSetup>();
            await socket.StartServer();
            await app.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Initial Setup Config
                    services.AddDbContext<RecommendationEngineDBContext>(
                        options => options.UseSqlServer("Server=ITT-AYUSH-SRIV\\SQLEXPRESS;Database=RecommendationEngineDB;Trusted_Connection=True;TrustServerCertificate=True"),
                        ServiceLifetime.Scoped);
                    services.AddScoped<SocketSetup>();
                    services.AddScoped<ClientHandler>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    // Controller Config
                    services.AddScoped<AuthController>();
                    services.AddScoped<AdminController>();
                    services.AddScoped<ChefController>();
                    services.AddScoped<EmployeeController>();
                    services.AddScoped<NotificationController>();

                    // Logic Config
                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<IAdminService, AdminService>();
                    services.AddScoped<IChefService, ChefService>();
                    services.AddScoped<IEmployeeService, EmployeeService>();
                    services.AddScoped<INotificationService, NotificationService>();
                });
        }
    }


}
