using Moq;
using Newtonsoft.Json;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.Service.Chef;
using RecommendationEngineServer.Service.Notifications;
using Xunit;

namespace RecommendationEngineServer.Test.Notification
{
    public class NotificationControllerTests : TestBase
    {
        private readonly NotificationController _controller;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly Mock<IChefService> _chefServiceMock;

        public NotificationControllerTests()
        {
            _notificationServiceMock = new Mock<INotificationService>();
            _chefServiceMock = new Mock<IChefService>();
            _controller = new NotificationController(_notificationServiceMock.Object, _chefServiceMock.Object);
        }

        [Fact]
        public async Task GetNotification_ShouldReturnSuccessResponse_WhenNotificationIsReceived()
        {
            // Arrange
            var notificationRequest = JsonConvert.DeserializeObject<NotificationRequest>(DataDictionary["NotificationRequest"].ToString());
            var expectedResponse = JsonConvert.DeserializeObject<NotificationResponse>(DataDictionary["NotificationResponse"].ToString());

            _notificationServiceMock.Setup(service => service.GetNotification(notificationRequest))
                                    .ReturnsAsync(expectedResponse);

            // Act
            var response = await _controller.GetNotifcation(notificationRequest);

            // Assert
            Assert.Equal(expectedResponse.Status, response.Status);
            Assert.Equal(expectedResponse.Message, response.Message);
            Assert.Equal(expectedResponse.IsNewNotification, response.IsNewNotification);
        }

        [Fact]
        public async Task GetMonthlyDiscardedMenuNotification_ShouldReturnSuccessResponse_WhenDiscardedMenusAreRetrieved()
        {
            // Arrange
            var currentDate = DateTime.Now;
            var recommendedMenuList = JsonConvert.DeserializeObject<List<RecommendedMenuModel>>(DataDictionary["MenuListResponse"].ToString());
            var expectedResponse = JsonConvert.DeserializeObject<DiscardedMenuResponse>(DataDictionary["DiscardedMenuResponse"].ToString());

            _chefServiceMock.Setup(service => service.GetMenuListItems())
                            .ReturnsAsync(recommendedMenuList);

            _notificationServiceMock.Setup(service => service.GetMonthlyDiscardedMenuNotification(currentDate, recommendedMenuList))
                                    .ReturnsAsync(expectedResponse.DiscardedMenus);

            // Act
            var response = await _controller.GetMonthlyDiscardedMenuNotification(currentDate);

            // Assert
            Assert.Equal(expectedResponse.Status, response.Status);
            Assert.NotNull(response.DiscardedMenus);
            Assert.Equal(expectedResponse.DiscardedMenus.Count, response.DiscardedMenus.Count);
        }

        [Fact]
        public async Task AddNewNotificationForDiscardedMenuFeedback_ShouldReturnSuccessResponse_WhenNotificationIsAdded()
        {
            // Arrange
            var menuImprovementRequest = JsonConvert.DeserializeObject<MenuImprovementNotificationRequest>(DataDictionary["MenuImprovementNotificationRequest"].ToString());

            _notificationServiceMock.Setup(service => service.AddNewNotificationForDiscardedMenuFeedback(menuImprovementRequest))
                                    .Returns(Task.CompletedTask);

            // Act
            var response = await _controller.AddNewNotificationForDiscardedMenuFeedback(menuImprovementRequest);

            // Assert
            Assert.Equal(ApplicationConstants.StatusSuccess, response.Status);
        }
    }

}
