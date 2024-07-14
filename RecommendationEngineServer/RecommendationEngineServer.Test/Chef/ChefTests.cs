using Moq;
using Newtonsoft.Json;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.Service.Chef;
using Xunit;

namespace RecommendationEngineServer.Test.Chef
{
    public class ChefTests : TestBase
    {
        private readonly ChefController _chefController;
        private readonly Mock<IChefService> _chefServiceMock;

        public ChefTests()
        {
            _chefServiceMock = new Mock<IChefService>();
            _chefController = new ChefController(_chefServiceMock.Object);
        }

        [Fact]
        public async Task AddDailyMenuItem_ReturnsExpectedResult()
        {
            // Arrange
            var menuItem = JsonConvert.DeserializeObject<MenuItem>(DataDictionary["MenuItem"].ToString());
            var expectedResponse = new BaseResponseDTO
            {
                Status = ApplicationConstants.StatusSuccess,
                Message = ApplicationConstants.DailyMenuAddedSuccessfully
            };

            _chefServiceMock.Setup(s => s.AddDailyMenuItem(It.IsAny<MenuItem>())).ReturnsAsync(1);

            // Act
            var result = await _chefController.AddDailyMenuItem(menuItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Status, result.Status);
            Assert.Equal(expectedResponse.Message, result.Message);
        }

        [Fact]
        public async Task SendNotification_ReturnsExpectedResult()
        {
            // Arrange
            var currentDate = DateTime.Now;
            var expectedResponse = new BaseResponseDTO
            {
                Status = ApplicationConstants.StatusSuccess,
                Message = ApplicationConstants.SentNotificationSuccessfully
            };

            _chefServiceMock.Setup(s => s.SendDailyMenuNotification(It.IsAny<DateTime>())).Returns(Task.CompletedTask);

            // Act
            var result = await _chefController.SendDailyMenuNotification(currentDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Status, result.Status);
            Assert.Equal(expectedResponse.Message, result.Message);
        }

        [Fact]
        public async Task GetMenuListItems_ReturnsExpectedResult()
        {
            // Arrange
            var recommendedMenuList = JsonConvert.DeserializeObject<List<RecommendedMenuModel>>(DataDictionary["MenuListResponse"].ToString());
            var expectedResponse = new RecommendedMenuResponse
            {
                RecommendedMenus = recommendedMenuList,
                Status = ApplicationConstants.StatusSuccess
            };

            _chefServiceMock.Setup(s => s.GetMenuListItems()).ReturnsAsync(recommendedMenuList);

            // Act
            var result = await _chefController.GetMenuListItems();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Status, result.Status);
            Assert.Equal(expectedResponse.RecommendedMenus.Count, result.RecommendedMenus.Count);
        }

        [Fact]
        public async Task DiscardMenuItem_ReturnsExpectedResult()
        {
            // Arrange
            var menuId = int.Parse(DataDictionary["OrderId"].ToString());
            var expectedResponse = new BaseResponseDTO { Status = ApplicationConstants.StatusSuccess };

            _chefServiceMock.Setup(s => s.DiscardMenuItem(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _chefController.DiscardMenuItem(menuId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Status, result.Status);
        }
    }
}
