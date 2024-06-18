using RecommendationEngineClient.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Login;

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
            AdminConsole adminHandler = new AdminConsole(requestServices);
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
