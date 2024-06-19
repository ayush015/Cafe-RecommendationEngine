using RecommendationEngineClient._20_ConsoleHandler;
using RecommendationEngineClient.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Login;

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

        public static async Task Start()
        {
            while(true)
            {
                RequestServices requestServices = new RequestServices();
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
    }
}
