using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.DAL;
using RecommendationEngineServer.DAL.UnitOfWork;
using RecommendationEngineServer.Logic.Login;

namespace RecommendationEngineServer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var app = (await CreateHostBuilder(args)).Build();
            var socket = app.Services.GetRequiredService<SocketSetup>();
            await socket.StartServer();
            await app.RunAsync();
        }

        public static async Task<IHostBuilder> CreateHostBuilder(string[] args)
        {
            return await Task.FromResult(Host.CreateDefaultBuilder(args)
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

                    // Logic Config
                    services.AddScoped<IAuthLogic, AuthLogic>();
                }));
        }
    }


}
