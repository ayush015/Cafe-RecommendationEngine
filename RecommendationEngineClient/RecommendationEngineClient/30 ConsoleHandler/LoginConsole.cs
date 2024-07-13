using Newtonsoft.Json;
using RecommendationEngineClient._20_ClientOperations.Login;
using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient.Login
{
    public class LoginConsole
    {
        private ILoginClientOperations _loginService;
        public LoginConsole(RequestServices requestServices) 
        { 
            _loginService = new LoginClientOperations(requestServices);
        }

        public async Task<LoggedInUserResponse> AttemptLogin()
        {
            UserLoginRequest request = new UserLoginRequest();
            Console.WriteLine("Login With Username and Password\n");

            Console.WriteLine("Enter UserName");
            request.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            request.Password = Console.ReadLine();

            Console.WriteLine();
            return await _loginService.LoginHandler(request);
        }
    }
}
