using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._20_ClientOperations.Login
{
    public interface ILoginClientOperations
    {
        Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request);
    }
}
