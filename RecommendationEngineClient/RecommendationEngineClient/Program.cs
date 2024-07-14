using RecommendationEngineClient._30_ConsoleHandler;
using RecommendationEngineClient.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Login;
using System.Net.Sockets;

namespace RecommendationEngineClient
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Cafe-Recommendation\n");
            await Start();
            Console.ReadLine();
        }

        private static async Task Start()
        {
            try
            {
                while (true)
                {
                    RequestService requestServices = new RequestService();
                    LoginConsole loginHandler = new LoginConsole(requestServices);
                    AdminConsole adminHandler = new AdminConsole(requestServices);
                    ChefConsole chefHandler = new ChefConsole(requestServices);
                    EmployeeConsole employeeHandler = new EmployeeConsole(requestServices);
                    var userLogin = await loginHandler.AttemptLogin();
                    UserRole userRole = (UserRole)userLogin.UserRoleId;
                    switch (userRole)
                    {
                        case UserRole.Admin:
                            {
                                await adminHandler.AdminConsoleHandler();

                            }
                            break;
                        case UserRole.Chef:
                            {
                                await chefHandler.ChefConsoleHandler();
                            }
                            break;
                        case UserRole.Employee:
                            {
                                await employeeHandler.EmployeeConsoleHandler(userLogin.UserId);
                            }
                            break;
                    }
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine((SocketError)ex.ErrorCode);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                Console.ReadKey();
                return;
            }
        }
    }
}
