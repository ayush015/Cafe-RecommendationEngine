using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._30_Services.Login
{
    public class LoginService : ILoginService
    {
        private RequestServices _requestServices;
        public LoginService(RequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        public async Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request)
        {

            var requestObject = JsonConvert.SerializeObject(request);
            DataObject requestData = new DataObject()
            {
                Controller = "Login",
                Action = "Login",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);
            var response = JsonConvert.DeserializeObject<LoggedInUserResponse>(jsonResponse);

            Console.WriteLine($"{response.Message}\n");

            return response; 
        }
    }
}
