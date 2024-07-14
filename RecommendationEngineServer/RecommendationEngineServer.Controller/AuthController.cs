using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Service.Login;

namespace RecommendationEngineServer.Controller
{
    public class AuthController
    {
        private IAuthService _authLogic;
        public AuthController(IAuthService authLogic)
        {
            _authLogic = authLogic;
        }

        public async Task<LoggedInUserResponse> Login(UserLoginRequest userLoginRequest)
        {
            try
            {
                var result = await _authLogic.Login(userLoginRequest);
                return new LoggedInUserResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.UserLoginSuccessfull,
                    UserId = result.Id,
                    UserRoleId = result.UserRoleId,
                    UserName = result.Username,
                };
            }
            catch (Exception ex)
            {
                return new LoggedInUserResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message,
                };

            }
        }
    }
}
