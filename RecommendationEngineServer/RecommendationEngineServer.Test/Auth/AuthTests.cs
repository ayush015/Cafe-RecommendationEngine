using Moq;
using Xunit;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Service.Login;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.Common;
using Newtonsoft.Json;

namespace RecommendationEngineServer.Test.Auth
{
    public class AuthControllerTests : TestBase
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_SuccessfulLogin_ReturnsLoggedInUserResponse()
        {
            // Arrange
            var userLoginRequest = JsonConvert.DeserializeObject<UserLoginRequest>(DataDictionary["User"].ToString()); 
            var loginResult = new User { Id = 1, Username = "Emp1", UserRoleId = 2 };

            _authServiceMock.Setup(auth => auth.Login(It.IsAny<UserLoginRequest>()))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _authController.Login(userLoginRequest);

            // Assert
            Assert.Equal(ApplicationConstants.StatusSuccess, result.Status);
            Assert.Equal(loginResult.Id, result.UserId);
            Assert.Equal(loginResult.Username, result.UserName);
            Assert.Equal(loginResult.UserRoleId, result.UserRoleId);
        }

        [Fact]
        public async Task Login_LoginFailed_ReturnsFailedResponse()
        {
            // Arrange
            var userLoginRequest = new UserLoginRequest { UserName = "testuser", Password = "password" };

            _authServiceMock.Setup(auth => auth.Login(It.IsAny<UserLoginRequest>()));

            // Act
            var result = await _authController.Login(userLoginRequest);

            // Assert
            Assert.Equal(ApplicationConstants.StatusFailed, result.Status);
        }
    }

}

