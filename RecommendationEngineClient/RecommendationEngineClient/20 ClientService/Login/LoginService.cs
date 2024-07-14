using RecommendationEngineClient._10_Common;
using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient._20_ClientService.Login
{
    public class LoginService :BaseService, ILoginService
    {
        public LoginService(RequestService requestServices) : base(requestServices) 
        { 
        }

        #region Public Method
        public async Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request)
        {
            var jsonResponse = await SendRequestAsync<LoggedInUserResponse>(ApiEndpoints.LoginController, "Login", request);
          
            Console.WriteLine($"{jsonResponse.Message}\n");

            return jsonResponse; 
        }
        #endregion
    }
}
