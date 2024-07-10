using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Controller;
using System.Net.Sockets;
using System.Text;

namespace RecommendationEngineServer
{
    public class ClientHandler
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly AuthController _authController;
        private readonly AdminController _adminController;
        private readonly ChefController _chefController;
        private readonly EmployeeController _employeeController;
        private readonly NotificationController _notificationController;
        private IServiceScope _scope;

        public ClientHandler(AuthController authController, AdminController adminController,
                             ChefController chefController, EmployeeController employeeController,
                             NotificationController notificationController)
        {
            _authController = authController;
            _adminController = adminController;
            _chefController = chefController;
            _employeeController = employeeController;
            _notificationController = notificationController;

        }

        #region Public Method
        public void SetClient(TcpClient client, IServiceScope scope)
        {
            _client = client;
            _stream = client.GetStream();
            // Store the scope to dispose it later
            _scope = scope;
        }

        public async void HandleClientAsync(object clientObj)
        {
            try
            {
                byte[] buffer = new byte[2048];
                int bytesRead;

                while ((bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) != 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: {0}", dataReceived);

                    await HandleIncomingDataString(dataReceived).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return;
            }
            finally
            {
                Cleanup();
            }
        }
        #endregion

        #region Private Method
        private async Task HandleIncomingDataString(string data)
        {
            var dataObject = JsonConvert.DeserializeObject<BaseRequestDTO>(data);
            if (dataObject != null)
            {
                await ControllerHandler(dataObject);
            }
        }

        private async Task ControllerHandler(BaseRequestDTO data)
        {
            switch (data.Controller)
            {
                case "Login":
                    await AuthControllerActionHandler(data);
                    break;
                case "Admin":
                    await AdminControllerActionHandler(data);
                    break;
                case "Chef":
                    await ChefControllerActionHandler(data);
                    break;
                case "Employee":
                    await EmployeeControllerActionHandler(data);
                    break;
                case "Notification":
                    await NotificationControllerActionHandler(data);
                    break;
                // Add other controllers here
                default:
                    Console.WriteLine($"Unknown controller: {data.Controller}");
                    break;
            }
        }

        private async Task AuthControllerActionHandler(BaseRequestDTO data)
        {
            switch (data.Action)
            {
                case "Login":
                    var user = JsonConvert.DeserializeObject<UserLoginRequest>(data.Data);
                    var jsonResponse = JsonConvert.SerializeObject(await _authController.Login(user));
                    await SendResponseAsync(jsonResponse);
                    break;
                // Handle other actions here
                default:
                    Console.WriteLine($"Unknown action: {data.Action} for AuthController");
                    break;
            }
        }

        private async Task AdminControllerActionHandler(BaseRequestDTO data)
        {
            switch (data.Action)
            {
                case "AddMenuItem":
                    var menuItem = JsonConvert.DeserializeObject<AddMenuItemRequest>(data.Data);
                    var jsonResponse = JsonConvert.SerializeObject(await _adminController.AddMenuItem(menuItem));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GetMenuList":
                    jsonResponse = JsonConvert.SerializeObject(await _adminController.GetMenuList());
                    await SendResponseAsync(jsonResponse);
                    break;
                case "RemoveMenuItem":
                    var menuId = JsonConvert.DeserializeObject<int>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _adminController.RemoveMenuItem(menuId));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "UpdateMenuItem":
                    var updateMenuItem = JsonConvert.DeserializeObject<UpdateMenuItemRequest>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _adminController.UpdateMenuItem(updateMenuItem));
                    await SendResponseAsync(jsonResponse);
                    break;
                // Handle other actions here
                default:
                    Console.WriteLine($"Unknown action: {data.Action} for AdminController");
                    break;
            }
        }

        private async Task ChefControllerActionHandler(BaseRequestDTO data)
        {
            switch (data.Action)
            {
                case "AddDailyMenuItem":
                    var menuIds = JsonConvert.DeserializeObject<MenuItem>(data.Data);
                    var jsonResponse = JsonConvert.SerializeObject(await _chefController.AddDailyMenuItem(menuIds));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "SendDailyMenuNotification":
                    var currentDate = JsonConvert.DeserializeObject<DateTime>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _chefController.SendDailyMenuNotification(currentDate));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GetMenuListItems":
                    jsonResponse = JsonConvert.SerializeObject(await _chefController.GetMenuListItems());
                    await SendResponseAsync(jsonResponse);
                    break;
                case "DiscardMenu":
                    int menuId = JsonConvert.DeserializeObject<int>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _chefController.DiscardMenuItem(menuId));
                    await SendResponseAsync(jsonResponse);
                    break;
                // Handle other actions here
                default:
                    Console.WriteLine($"Unknown action: {data.Action} for ChefController");
                    break;
            }
        }

        private async Task EmployeeControllerActionHandler(BaseRequestDTO data)
        {
            switch (data.Action)
            {
                case "GetNotification":
                    var notification = JsonConvert.DeserializeObject<NotificationRequest>(data.Data);
                    var jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetNotifcation(notification));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "SelectFoodItemsFromDailyMenu":
                    var order = JsonConvert.DeserializeObject<OrderRequest>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.SelectFoodItemsFromDailyMenu(order));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GiveFeedBack":
                    var feedbacks = JsonConvert.DeserializeObject<List<GiveFeedBackRequest>>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.GiveFeedBack(feedbacks));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GetMenuItemByOrderId":
                    var orderId = JsonConvert.DeserializeObject<int>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetMenuItemByOrderId(orderId));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "AddMenuImprovementFeedbacks":
                    var improvementFeedback = JsonConvert.DeserializeObject<MenuImprovementFeedbackRequest>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.AddMenuImprovementFeedbacks(improvementFeedback));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GetMenuFeedBackQuestions":
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetMenuFeedBackQuestions());
                    await SendResponseAsync(jsonResponse);
                    break;
                case "AddUserPreference":
                    var userFoodPreference = JsonConvert.DeserializeObject<UserPreferenceRequest>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.AddUserPreference(userFoodPreference));
                    await SendResponseAsync(jsonResponse);
                    break;
                case "GetDailyRolledOutMenu": 
                    var rolledOutMenuRequest = JsonConvert.DeserializeObject<DailyRolledOutMenuRequest>(data.Data);
                    jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetDailyRolledOutMenu(rolledOutMenuRequest));
                    await SendResponseAsync(jsonResponse);
                    break;
                // Handle other actions here
                default:
                    Console.WriteLine($"Unknown action: {data.Action} for EmployeeController");
                    break;
            }
        }

        private async Task NotificationControllerActionHandler(BaseRequestDTO data)
        {
            switch (data.Action)
            {
                case "GetMonthlyNotification":
                    {
                        var date = JsonConvert.DeserializeObject<DateTime>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _notificationController.GetMonthlyDiscardedMenuNotification(date));
                        await SendResponseAsync(jsonResponse);
                        break;
                    }   
                case "AddNewNotificationForDiscardedMenuFeedback":
                    {
                        var improveMenu = JsonConvert.DeserializeObject<MenuImprovementNotificationRequest>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _notificationController.AddNewNotificationForDiscardedMenuFeedback(improveMenu));
                        await SendResponseAsync(jsonResponse);
                        break;
                    }  
                // Handle other actions here
                default:
                    Console.WriteLine($"Unknown action: {data.Action} for EmployeeController");
                    break;
            }
        }

        private async Task SendResponseAsync(string response)
        {
            try
            {
                var dataToSend = Encoding.ASCII.GetBytes(response);
                await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending response: {ex.Message}");
                return;
            }
        }

        private void Cleanup()
        {
            try
            {
                _client?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing client: {ex.Message}");
            }

            try
            {
                _scope?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing scope: {ex.Message}");
            }
        }
        #endregion
    }

}
