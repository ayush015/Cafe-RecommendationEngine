using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Login
{
    public class LoginHandler
    {
        private RequestServices _requestServices;
        public LoginHandler(RequestServices requestServices) 
        { 
          _requestServices = requestServices;
        }

        public async Task<LoggedInUserResponse> AttemptLogin()
        {
            UserLoginRequest request = new UserLoginRequest();
            Console.WriteLine("Enter UserName");
            request.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            request.Password = Console.ReadLine();

            var requestObject  = JsonConvert.SerializeObject(request);
            DataObject requestData = new DataObject()
            { 
                Controller = "Login",
                Action = "Login",
                Data = requestObject
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var response = await _requestServices.SendRequestAsync(jsonRequest);
            return JsonConvert.DeserializeObject<LoggedInUserResponse>(response);
        }
    }
}
