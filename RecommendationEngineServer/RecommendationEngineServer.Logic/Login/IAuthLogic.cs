using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Models;

namespace RecommendationEngineServer.Logic.Login
{
    public interface IAuthLogic
    {
        Task<User> Login(UserLoginRequest userLoginRequest);
    }
}
