using Newtonsoft.Json;
using RecommendationEngineClient.Common;
using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._20_ClientOperations.Login
{
    public class LoginClientOperations :BaseClientOperations, ILoginClientOperations
    {
        public LoginClientOperations(RequestServices requestServices) : base(requestServices) 
        { 
        }

        #region Public Method
        public async Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request)
        {
            var jsonResponse = await SendRequestAsync<LoggedInUserResponse>(ApplicationConstants.LoginController, "Login", request);
          
            Console.WriteLine($"{jsonResponse.Message}\n");

            return jsonResponse; 
        }
        #endregion
    }
}
