using Newtonsoft.Json;
using RecommendationEngineClient.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Login;
using System.Net.Sockets;
using System.Text;

namespace RecommendationEngineClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Start();
            Console.ReadLine();
        }

        public static async Task Start()
        {
            RequestServices requestServices = new RequestServices();
            LoginHandler loginHandler = new LoginHandler(requestServices);
            AdminHandler adminHandler = new AdminHandler(requestServices);
            var userLogin = await loginHandler.AttemptLogin();
            UserRole userRole = (UserRole)userLogin.UserRoleId;
            switch (userRole)
            {
                case UserRole.Admin:
                    {
                        await adminHandler.AdminConsole();
                    }
                    break;
                case UserRole.Chef:
                    {

                    }
                    break;
                case UserRole.Employee:
                    {

                    }
                    break;
            }


            Console.WriteLine($" Username : {userLogin.UserName},\n User Role : {userLogin.UserRoleId}");
           
        }
    }
}
