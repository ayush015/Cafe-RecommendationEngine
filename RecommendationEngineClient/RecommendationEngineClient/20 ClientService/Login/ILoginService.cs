using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient._20_ClientService.Login
{
    public interface ILoginService
    {
        Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request);
    }
}
