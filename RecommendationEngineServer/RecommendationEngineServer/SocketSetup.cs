using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineServer.Common;
using System.Net;
using System.Net.Sockets;

namespace RecommendationEngineServer
{
    public class SocketSetup
    {
        private readonly IServiceProvider _serviceProvider;

        public SocketSetup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartServer()
        {
            int port = ApplicationConstants.ServerPort;
            Console.WriteLine("Start server...");
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"Server is up and running at port: {port}");
            int requestCount = 0;

            while (true)
            {
                Console.WriteLine("Wait for Request...");
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"Request number : {++requestCount}");

                // Resolve a new scope for each client
                var scope = _serviceProvider.CreateScope();
                var clientHandler = scope.ServiceProvider.GetRequiredService<ClientHandler>();
                clientHandler.SetClient(client, scope);
                Thread clientThread = new Thread(new ParameterizedThreadStart(clientHandler.HandleClientAsync));
                clientThread.Start();
            }
        }
    }


}
