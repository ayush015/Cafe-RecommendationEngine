using Microsoft.EntityFrameworkCore;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.UnitOfWork;

namespace RecommendationEngineServer.Logic.Login
{
    public class AuthLogic : IAuthLogic
    {
        private IUnitOfWork _unitOfWork;
        public AuthLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        public async Task<User> Login(UserLoginRequest userLoginRequest)
        {
            if(userLoginRequest.UserName == null || userLoginRequest.Password == null)
            {
                throw new Exception(ApplicationConstants.UserNamePasswordIsNull);
            }

            var user = (await _unitOfWork.User.GetAll())
                                             .FirstOrDefault(u =>
                                             u.Username.Equals(userLoginRequest.UserName) && 
                                             u.Password.Equals(userLoginRequest.Password)
                                             );

            if(user == null)
            {
                throw new Exception(ApplicationConstants.UserNameAndPasswordDidNotMatch);
            }

            return user;
        }
    }
}
