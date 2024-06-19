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
        private AuthController _authController;
        private AdminController _adminController;
        private ChefController _chefController;
        private EmployeeController _employeeController;
        private IServiceScope _scope;

        public ClientHandler(AuthController authController, AdminController adminController,ChefController chefController, EmployeeController employeeController)
        {
            _authController = authController;
            _adminController = adminController;
            _chefController = chefController;
            _employeeController = employeeController;   
        }

        public void SetClient(TcpClient client, IServiceScope scope)
        {
            _client = client;
            _stream = client.GetStream();
            // Store the scope to dispose it later
            _scope = scope; 
        }

        public async void HandleClientAsync(object client)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: {0}", dataReceived);

                    await HandleIncomingDataString(dataReceived);
                }
            }
            finally
            {
                _client?.Close();
                _scope?.Dispose();
            }
        }

        private async Task HandleIncomingDataString(string data)
        {
            var dataObject = JsonConvert.DeserializeObject<DataObject>(data);

            await ControllerHandler(dataObject);
        }

        private async Task ControllerHandler(DataObject data)
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
                    // Add other controllers here
            }
        }

        private async Task AuthControllerActionHandler(DataObject data)
        {
            switch (data.Action)
            {
                case "Login":
                    UserLoginRequest user = JsonConvert.DeserializeObject<UserLoginRequest>(data.Data);
                    var jsonResponse = JsonConvert.SerializeObject(await _authController.Login(user));
                    byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                    await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                    break;
                    // Handle other actions here
            }
        }

        private async Task AdminControllerActionHandler(DataObject data)
        {
            switch (data.Action)
            {
                case "AddMenuItem":
                    {
                        AddMenuItemRequest menuItem = JsonConvert.DeserializeObject<AddMenuItemRequest>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _adminController.AddMenuItem(menuItem));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "GetMenuList":
                    {
                        var jsonResponse = JsonConvert.SerializeObject(await _adminController.GetMenuList());
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "RemoveMenuItem":
                    {
                        int menuId = JsonConvert.DeserializeObject<int>(data.Data); ;
                        var jsonResponse = JsonConvert.SerializeObject(await _adminController.RemoveMenuItem(menuId));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "UpdateMenuItem":
                    {
                        UpdateMenuItemRequest menuItem = JsonConvert.DeserializeObject<UpdateMenuItemRequest>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _adminController.UpdateMenuItem(menuItem));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                    // Handle other actions here
            }
        }

        private async Task ChefControllerActionHandler(DataObject data)
        {
            switch (data.Action)
            {
                case "AddDailyMenuItem":
                    {
                        List<int> menuIds = JsonConvert.DeserializeObject<List<int>>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _chefController.AddDailyMenuItem(menuIds));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "SendNotification":
                    {
                        var jsonResponse = JsonConvert.SerializeObject(await _chefController.SendNotification());
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }

                    // Handle other actions here
            }
        }

        private async Task EmployeeControllerActionHandler(DataObject data)
        {
            switch (data.Action)
            {
                case "GetNotification":
                    {
                        int userId = JsonConvert.DeserializeObject<int>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetNotification(userId));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "SelectFoodItemsFromDailyMenu":
                    {
                        OrderRequest order = JsonConvert.DeserializeObject<OrderRequest>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _employeeController.SelectFoodItemsFromDailyMenu(order));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "GiveFeedBack":
                    {
                        var feedbacks = JsonConvert.DeserializeObject<List<GiveFeedBackRequest>>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _employeeController.GiveFeedBack(feedbacks));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }
                case "GetMenuItemByOrderId":
                    {
                        int orderId = JsonConvert.DeserializeObject<int>(data.Data);
                        var jsonResponse = JsonConvert.SerializeObject(await _employeeController.GetMenuItemByOrderId(orderId));
                        byte[] dataToSend = Encoding.ASCII.GetBytes(jsonResponse);
                        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                        break;
                    }

                    // Handle other actions here GetMenuItemByOrderId
            }
        }

    }


}
