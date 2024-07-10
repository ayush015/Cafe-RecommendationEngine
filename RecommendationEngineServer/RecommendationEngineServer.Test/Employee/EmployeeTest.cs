using Moq;
using Newtonsoft.Json;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.Service.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RecommendationEngineServer.Test.Employee
{
    public class EmployeeTest : TestBase
    {
        private readonly EmployeeController _employeeController;
        private readonly Mock<IEmployeeService> _employeeServiceMock;

        public EmployeeTest()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeServiceMock.Object);
        }


        [Fact]
        public async Task SelectFoodItemsFromDailyMenu_ReturnsExpectedResult()
        {
            // Arrange
            var orderRequest = JsonConvert.DeserializeObject<OrderRequest>(DataDictionary["OrderRequest"].ToString());
            var expectedResponse = JsonConvert.DeserializeObject<OrderResponse>(DataDictionary["OrderResponse"].ToString());

            _employeeServiceMock.Setup(s => s.SelectFoodItemsFromDailyMenu(It.IsAny<OrderRequest>()))
                .ReturnsAsync(expectedResponse.OrderId);

            // Act
            var result = await _employeeController.SelectFoodItemsFromDailyMenu(orderRequest);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GiveFeedBack_ReturnsExpectedResult()
        {
            // Arrange
            var feedbackRequest = JsonConvert.DeserializeObject<List<GiveFeedBackRequest>>(DataDictionary["FeedbackRequest"].ToString());
            var expectedResponse = new BaseResponseDTO
            {
                Status = ApplicationConstants.StatusSuccess,
                Message = ApplicationConstants.FeedbackSuccess
            };

            _employeeServiceMock.Setup(s => s.GiveFeedBack(It.IsAny<List<GiveFeedBackRequest>>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.GiveFeedBack(feedbackRequest);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMenuItemByOrderId_ReturnsExpectedResult()
        {
            // Arrange
            var orderId = int.Parse(DataDictionary["OrderId"].ToString());
            var expectedMenuItems = JsonConvert.DeserializeObject<List<UserOrderMenuModel>>(DataDictionary["MenuItems"].ToString());
            var expectedResponse = new UserOrderMenuListResponse
            {
                Status = ApplicationConstants.StatusSuccess,
                UserOrderMenus = expectedMenuItems
            };

            _employeeServiceMock.Setup(s => s.GetMenuItemsByOrderId(It.IsAny<int>()))
                .ReturnsAsync(expectedMenuItems);

            // Act
            var result = await _employeeController.GetMenuItemByOrderId(orderId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddMenuImprovementFeedbacks_ReturnsExpectedResult()
        {
            // Arrange
            var feedbackRequest = JsonConvert.DeserializeObject<MenuImprovementFeedbackRequest>(DataDictionary["MenuImprovementFeedbackRequest"].ToString());
            var expectedResponse = new BaseResponseDTO { Status = ApplicationConstants.StatusSuccess, Message = "Added Successfully" };

            _employeeServiceMock.Setup(s => s.AddUserMenuImprovementFeedback(It.IsAny<MenuImprovementFeedbackRequest>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.AddMenuImprovementFeedbacks(feedbackRequest);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMenuFeedBackQuestions_ReturnsExpectedResult()
        {
            // Arrange
            var expectedQuestions = JsonConvert.DeserializeObject<List<FeedbackQuestion>>(DataDictionary["FeedbackQuestions"].ToString());
            var expectedResponse = new FeedbackQuestionResponse
            {
                FeedbackQuestions = expectedQuestions,
                Status = ApplicationConstants.StatusSuccess
            };

            _employeeServiceMock.Setup(s => s.GetMenuFeedBackQuestions())
                .ReturnsAsync(expectedQuestions);

            // Act
            var result = await _employeeController.GetMenuFeedBackQuestions();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.FeedbackQuestions.Count, result.FeedbackQuestions.Count);
        }
    }
}
