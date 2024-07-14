using Moq;
using Newtonsoft.Json;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.Controller;
using RecommendationEngineServer.Service.Admin;
using Xunit;

namespace RecommendationEngineServer.Test.Admin
{

    public class AdminTests : TestBase
    {
        private Mock<IAdminService> _adminServiceMock;
        private AdminController _adminController;
        public AdminTests()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _adminController = new AdminController(_adminServiceMock.Object);
        }

        [Fact]
        public async Task AddMenuItem_ShouldReturnSuccess_WhenItemIsAdded()
        {
            //Arrange
            var request = JsonConvert.DeserializeObject<AddMenuItemRequest>(DataDictionary["AddMenuItemRequests"].ToString());
            _adminServiceMock.Setup(s => s.AddMenuItem(request)).ReturnsAsync(1);

            var response = await _adminController.AddMenuItem(request);

            Assert.Equal(ApplicationConstants.StatusSuccess, response.Status);
            Assert.Equal(ApplicationConstants.MenuItemAddedSuccessfully, response.Message);
        }

        [Fact]
        public async Task AddMenuItem_ShouldReturnFailed_WhenItemAlreadyExists()
        {
            //Arrange
            var request = JsonConvert.DeserializeObject<AddMenuItemRequest>(DataDictionary["AddMenuItemRequests"].ToString());

            _adminServiceMock.Setup(s => s.AddMenuItem(request)).Throws(new MenuItemAlreadyPresentException());

            //Act
            var response = await _adminController.AddMenuItem(request);

            //Assert
            Assert.Equal(ApplicationConstants.StatusFailed, response.Status);
        }

        [Fact]
        public async Task GetMenuList_ShouldReturnMenuList_WhenItemsExist()
        {
            //Arrange
            var menuList = JsonConvert.DeserializeObject<List<MenuListModel>>(DataDictionary["menuList"].ToString());
            //var menuList = new List<MenuListModel> { new MenuListModel { MenuId = 1, FoodItemId = 1, FoodItemName = "Pizza", MealTypeId = 1, MealTypeName = "Lunch" } };

            _adminServiceMock.Setup(s => s.GetMenuList()).ReturnsAsync(menuList);

            //Act
            var response = await _adminController.GetMenuList();

            //Assert
            Assert.Equal(ApplicationConstants.StatusSuccess, response.Status);
            Assert.NotNull(response.MenuList);
            Assert.Equal(2, response.MenuList.Count);
        }

        [Fact]
        public async Task GetMenuList_ShouldReturnFailed_WhenNoItemsExist()
        {
            //Arrange
            _adminServiceMock.Setup(s => s.GetMenuList()).ReturnsAsync(new List<MenuListModel>());

            //Act
            var response = await _adminController.GetMenuList();

            //Assert
            Assert.Null(response.MenuList);
        }

        [Fact]
        public async Task RemoveMenuItem_ShouldReturnSuccess_WhenItemIsRemoved()
        {
            //Arrange
            int menuId = 1;
            _adminServiceMock.Setup(s => s.RemoveMenuItem(menuId)).Returns(Task.CompletedTask);

            //Act
            var response = await _adminController.RemoveMenuItem(menuId);

            //Assert
            Assert.Equal(ApplicationConstants.StatusSuccess, response.Status);
            Assert.Equal(ApplicationConstants.MenuItemRemovedSuccessfully, response.Message);
        }

        [Fact]
        public async Task RemoveMenuItem_ShouldReturnFailed_WhenItemNotFound()
        {
            //Arrange
            int menuId = 1;
            _adminServiceMock.Setup(s => s.RemoveMenuItem(menuId)).Throws(new MenuItemNotFoundException());

            //Act
            var response = await _adminController.RemoveMenuItem(menuId);

            //Assert
            Assert.Equal(ApplicationConstants.StatusFailed, response.Status);
        }

        [Fact]
        public async Task UpdateMenuItem_ShouldReturnSuccess_WhenItemIsUpdated()
        {
            //Arrange
            var request = new UpdateMenuItemRequest { MenuId = 1, FoodItemName = "Pasta", MealTypeId = 2 };
            _adminServiceMock.Setup(s => s.UpdateMenuItem(request)).Returns(Task.CompletedTask);

            //Act
            var response = await _adminController.UpdateMenuItem(request);

            //Assert
            Assert.Equal(ApplicationConstants.StatusSuccess, response.Status);
        }

        [Fact]
        public async Task UpdateMenuItem_ShouldReturnFailed_WhenItemNotFound()
        {
            //Arrange
            var request = new UpdateMenuItemRequest { MenuId = 1, FoodItemName = "Pasta", MealTypeId = 2 };
            _adminServiceMock.Setup(s => s.UpdateMenuItem(request)).Throws(new MenuItemNotFoundException());

            //Act
            var response = await _adminController.UpdateMenuItem(request);

            //Assert
            Assert.Equal(ApplicationConstants.StatusFailed, response.Status);
        }
    }

}
