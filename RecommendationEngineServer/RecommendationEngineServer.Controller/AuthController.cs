using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Logic.Login;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.Common;

namespace RecommendationEngineServer.Controller
{
    public class AuthController
    {
        private IAuthLogic _authLogic; 
        public AuthController(IAuthLogic authLogic) 
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
