using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Models;

namespace RecommendationEngineServer.Service.Login
{
    public interface IAuthService
    {
        Task<User> Login(UserLoginRequest userLoginRequest);
    }
}
