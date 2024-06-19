using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.DAL;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.Logic.Admin;
using RecommendationEngineServer.Logic.Chef;
using RecommendationEngineServer.Logic.Employee;
using RecommendationEngineServer.Logic.Login;

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

                    // Logic Config
                    services.AddScoped<IAuthLogic, AuthLogic>();
                    services.AddScoped<IAdminLogic, AdminLogic>();
                    services.AddScoped<IChefLogic, ChefLogic>();
                    services.AddScoped<IEmployeeLogic, EmployeeLogic>();
                });
        }
    }


}
