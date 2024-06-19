using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._30_Services.Login
{
    public interface ILoginService
    {
        Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request);
    }
}
