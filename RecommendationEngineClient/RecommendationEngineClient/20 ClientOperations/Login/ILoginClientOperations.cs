using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient._20_ClientOperations.Login
{
    public interface ILoginClientOperations
    {
        Task<LoggedInUserResponse> LoginHandler(UserLoginRequest request);
    }
}
