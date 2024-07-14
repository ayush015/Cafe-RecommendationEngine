﻿using RecommendationEngineClient._20_ClientService.Login;
using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient.Login
{
    public class LoginConsole
    {
        private ILoginService _loginService;
        public LoginConsole(RequestService requestServices)
        {
            _loginService = new LoginService(requestServices);
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
