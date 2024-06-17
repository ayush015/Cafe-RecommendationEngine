using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineServer.Common;
using RecommendationEngineServer.Controller;
using System.Net;
using System.Net.Sockets;

namespace RecommendationEngineServer
{
    //public class SocketSetup
    //{
    //    private AuthController _userController;

    //    public SocketSetup(AuthController userController)
    //    {
    //        _userController = userController;
    //    }

    //    public async Task StartServer()
    //    {
    //        int port = ApplicationConstants.ServerPort;
    //        Console.WriteLine("Start server...");
    //        TcpListener server = new TcpListener(IPAddress.Any, port);
    //        server.Start();
    //        Console.WriteLine($"Server is up and running at port: {port}");
    //        int clientCounter = 0;

    //        while (true)
    //        {
    //            Console.WriteLine("Connecting new Clients");
    //            TcpClient client = await server.AcceptTcpClientAsync();
    //            Console.WriteLine($"{++clientCounter} Clients Connected");
    //            ClientHandler clientHandler = new ClientHandler(client,_userController);
    //            Thread clientThread = new Thread(new ParameterizedThreadStart(clientHandler.HandleClientAsync));
    //            clientThread.Start();
    //            //await Task.Run(() => clientHandler.HandleClientAsync());
    //        }
    //    }
    //}
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
            int clientCounter = 0;

            while (true)
            {
                Console.WriteLine("Connecting new Clients");
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"{++clientCounter} Clients Connected");

                // Resolve a new scope for each client
                var scope = _serviceProvider.CreateScope();
                var clientHandler = scope.ServiceProvider.GetRequiredService<ClientHandler>();
                clientHandler.SetClient(client, scope); // Pass the scope to the client handler
                Thread clientThread = new Thread(new ParameterizedThreadStart(clientHandler.HandleClientAsync));
                clientThread.Start();
            }
        }
    }


}
