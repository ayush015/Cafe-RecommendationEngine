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
        private IServiceScope _scope;

        public ClientHandler(AuthController authController)
        {
            _authController = authController;
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
            var splitData = data.Split('/');
            var dataObject = new DataObject()
            {
                Controller = splitData[0],
                Action = splitData[1],
                Data = splitData[2]
            };

            await ControllerHandler(dataObject);
        }

        private async Task ControllerHandler(DataObject data)
        {
            switch (data.Controller)
            {
                case "Login":
                    await AuthControllerActionHandler(data);
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
    }


}
